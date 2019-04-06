using System;
using System.Collections.Generic;
using System.Text;

namespace Novum.Data.Utils
{
    /// <summary>
    /// DataString contains a string Value which can be be splitted by a comma, semicolon, pipe, double pipes or character 96.
    /// </summary>
    public class DataString
    {
        private const char Comma = (char)44;
        private const char Semicolon = (char)59;
        public const char Char96 = (char)96;
        private const char SinglePipe = (char)124;
        private const string DoublePipes = "||";
        private const char SingleQuote = (char)39;
        private const char DoubleQuotes = (char)34;
        private const string CRLF = "\r\n";


        private string _string;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value"></param>
        public DataString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                _string = string.Empty;
            }
            else
            {
                _string = value;
            }
        }

        /// <summary>
        /// Split the current string value by comma (,)
        /// </summary>
        /// <returns>String Array</returns>
        public string[] SplitByComma()
        {
            if (string.IsNullOrEmpty(_string))
                return new string[] { };
            return _string.Split(Comma);
        }

        /// <summary>
        /// Split the current string value by semicolon (;)
        /// </summary>
        /// <returns>String Array</returns>
        public string[] SplitBySemicolon()
        {
            if (string.IsNullOrEmpty(_string))
                return new string[] { };
            return _string.Split(Semicolon);
        }

        /// <summary>
        /// Split the current string value by pipe (|)
        /// </summary>
        /// <returns>String Array</returns>
        public string[] SplitByPipe()
        {
            if (string.IsNullOrEmpty(_string))
                return new string[] { };
            return _string.Split(SinglePipe);
        }

        /// <summary>
        /// Split the current string value by double pipes (||)
        /// </summary>
        /// <returns>String Array</returns>
        public string[] SplitByDoublePipes()
        {
            if (string.IsNullOrEmpty(_string))
                return new string[] { };
            return _string.Split(new string[] { DoublePipes }, StringSplitOptions.None);
        }

        /// <summary>
        /// Split the current string value by char 96 (`)
        /// </summary>
        /// <returns>String Array</returns>
        public string[] SplitByChar96()
        {
            if (string.IsNullOrEmpty(_string))
                return new string[] { };
            return _string.Split(Char96);
        }


        /// <summary>
        /// Split the current string value by carriage return line feed (\r\n)
        /// </summary>
        /// <returns>String Array</returns>
        public string[] SplitByCRLF()
        {
            if (string.IsNullOrEmpty(_string))
                return new string[] { };
            return _string.Split(new string[] { CRLF }, StringSplitOptions.None);
        }
    }
}