using System;

namespace Nt.Data.Utils
{
    /// <summary>
    /// Helper class for mathimatical conversions
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">decimal value which should be rounded</param>
        /// <param name="rounding">rounding (0.05 or 0.1 or 0.01 or 1.00)</param>
        /// <returns></returns>
         public static decimal Round(decimal value, decimal rounding)
        {
            if (rounding.Equals(decimal.Zero))
                rounding = 0.01m;

            var value1 = decimal.Divide(value, rounding);
            var value2 = decimal.Round(value1, MidpointRounding.AwayFromZero);
            var result = decimal.Multiply(value2, rounding);
            return result;
        }
    }
}