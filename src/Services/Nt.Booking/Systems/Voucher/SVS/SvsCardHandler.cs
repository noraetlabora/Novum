using System;

namespace Nt.Booking.Systems.Voucher.SVS
{
    /// <summary>
    /// This class handles the properties of a SVS card. It extracts additional
    /// arguments and give access to all card information.
    /// </summary>
    public class SvsCardHandler
    {
        /// <summary>Min and max range value.</summary>
        private Tuple<ulong, ulong> _binRange;
        /// <summary>Card number without SSC and PIN.</summary>
        private int _digitsCardNum = 0;
        /// <summary>User defined PIN (SSC/PIN) pattern, e.g. SSSSPPPP.</summary>
        private string _pinPattern = "";
        /// <summary>Type of the card.</summary>
        public string Type { get; set; } = "";
        /// <summary>Maximum allowed charging amount.</summary>
        public decimal MaxCharge { get; set; } = 0;
        /// <summary>Flag to define cards that can only by redeemed by it full value.</summary>
        public bool OnlyFullRedemption { get; set; } = false;
        /// <summary>User defined PIN (SSC/PIN) pattern, e.g. SSSSPPPP.</summary>
        public string PinPattern { get { return _pinPattern; } set { _pinPattern = value.ToUpper(); } }

        /// <summary>
        /// Create a new object defined by its range.
        /// </summary>
        /// <param name="range">Number range separated by a '-', e.g. "10-20" or "10 - 20".</param>
        public SvsCardHandler(in string range)
        {
            SetBinRange(range);
        }

        /// <summary>
        /// Checks if range values are valid.
        /// </summary>
        /// <param name="range">Range (min-max), e.g. "10 - 20"</param>
        /// <returns></returns>
        public bool IsRangeValid(in string range)
        {
            var xRange = range.Split("-");

            if (xRange.Length != 2)
                return false;

            if (xRange[0].Length != xRange[1].Length)
                return false;

            return true;
        }

        /// <summary>
        /// Set the bin range of the card.
        /// </summary>
        /// <param name="range">Range (min-max), e.g. "10 - 20"</param>
        public void SetBinRange(in string range)
        {
            if (!IsRangeValid(range))
                throw new FormatException("Format of range values is not valid.");

            _binRange = StrToTuple<ulong>(range);
            _digitsCardNum = NumberOfDigits(range);
        }

        /// <summary>
        /// Get the number of digits.
        /// </summary>
        /// <param name="range">Range (min-max), e.g. "10 - 20"</param>
        /// <returns>Number of digits.</returns>
        private int NumberOfDigits(in string range)
        {
            var xRange = range.Split('-');
            return xRange[0].Trim().Length;
        }

        /// <summary>
        /// Card number without SSC and PIN.
        /// </summary>
        /// <param name="mediumId">Medium identification number.</param>
        /// <returns>Card number without pin.</returns>
        public string GetCardNumber(in string mediumId)
        {
            if (mediumId?.Length >= _digitsCardNum)
                return mediumId.Substring(0, _digitsCardNum);

            return mediumId;
        }

        /// <summary>
        /// Check if a medium identification number is in the current range, e.g.
        /// Secondary Security Code(SSC) is encoded in the QR-Code like xxxxxxxxxxxxxxxxxxxSSSS
        /// Pin from the manual input should be encoded like            xxxxxxxxxxxxxxxxxxx????PPPP
        /// The PinNumber is either SSSS0000 or 0000PPPP.
        /// </summary>
        /// <param name="mediumId">Medium identification number.</param>
        /// <returns>True, if in range, otherwise false.</returns>
        public bool IsInRange(in string mediumId)
        {
            if (ulong.TryParse(GetCardNumber(mediumId), out ulong mediumNum))
            {
                if (mediumNum >= _binRange.Item1 && mediumNum <= _binRange.Item2)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get the pin number defined by its pattern.
        /// </summary>
        /// <param name="mediumId">Medium identification number.</param>
        /// <returns>Pin number as defined by it's card pattern.</returns>
        public string GetAmount(string mediumId)
        {
            string mediumFullPin = mediumId.Substring(_digitsCardNum);
            string amount = "";

            for (int i = 0, iLength = mediumFullPin.Length; i < iLength; i++)
            {
                if (i < _pinPattern.Length)
                {
                    if (_pinPattern[i] == 'V')
                    {
                        amount += mediumFullPin[i];
                    }
                }
            }
            return amount;
        }

        /// <summary>
        /// Get the pin number defined by its pattern.
        /// </summary>
        /// <param name="mediumId">Medium identification number.</param>
        /// <returns>Pin number as defined by it's card pattern.</returns>
        public string GetPinNumber(string mediumId)
        {
            // no SSC or PIN
            if (OnlyFullRedemption)
                return string.Empty;

            if (_pinPattern.Length != 0)
            {
                string mediumFullPin = mediumId.Substring(_digitsCardNum);
                char[] extractedPin = "".PadRight(_pinPattern.Length, '0').ToCharArray();

                for (int i = 0, iLength = mediumFullPin.Length; i < iLength; i++)
                {
                    if (i < _pinPattern.Length)
                    {
                        if (_pinPattern[i] == 'P')
                        {
                            extractedPin[i] = mediumFullPin[i];
                        }
                    }
                }
                return new string(extractedPin);
            }
            Nt.Logging.Log.Server.Info("SVS - pin number not detachable from mediumId " + mediumId);
            return "";
        }

        /// <summary>
        /// Convert string range to a numeric tuple.
        /// </summary>
        /// <typeparam name="T">Numeric type.</typeparam>
        /// <param name="range">Range in the form of minVal - maxVal, e.g. '10-20' or '10 - 20'.</param>
        /// <returns></returns>
        private Tuple<T, T> StrToTuple<T>(in string range, in string separator = "-", in Tuple<T, T> defaultVal = default(Tuple<T, T>))
        {
            var xRange = range.Split(separator);
            try
            {
                if (xRange.Length != 2)
                    throw new NotSupportedException();

                var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));

                if (converter == null)
                    return defaultVal;

                var minVal = (T)converter.ConvertFromString(xRange[0].Trim());
                var maxVal = (T)converter.ConvertFromString(xRange[1].Trim());

                return new Tuple<T, T>(minVal, maxVal);
            }
            catch (NotSupportedException)
            {
                return defaultVal;
            }
        }
    }
}
