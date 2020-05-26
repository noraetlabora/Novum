namespace Nt.Print.Device
{
    public abstract class Printer
    {

        #region fields

        protected System.Collections.Generic.List<byte> _buffer;

        /// <summary>
        /// Horizontal Tab
        /// </summary>
		public const byte HT = 0x09;
        /// <summary>
        /// Line Feed
        /// </summary>
		public const byte LF = 0x0A;
        /// <summary>
        /// Carriage Return
        /// </summary>
		public const byte CR = 0x0D;
        /// <summary>
        /// Form Feed
        /// </summary>
		public const byte FF = 0x0C;
        /// <summary>
        /// Escape
        /// </summary>
		public const byte ESC = 0x1B;

        #endregion

        #region Properties

        public uint CharactersPerLine { get; set; }

        public System.Text.Encoding SystemEncoding { get; set; }

        public System.Text.Encoding TargetEncoding { get; set; }

        #endregion

        public Printer(System.Text.Encoding targetEncoding, uint charactersPerLine = 32)
        {
            this.TargetEncoding = targetEncoding;
            this.SystemEncoding = System.Text.Encoding.Default;
            this.CharactersPerLine = charactersPerLine;
        }

        public void SetBuffer(ref System.Collections.Generic.List<byte> buffer)
        {
            this._buffer = buffer;
        }

        public void SetControlCode(byte controlCode)
        {
            this._buffer.Add(controlCode);
        }
        public void SetControlCodes(byte[] controlCodes)
        {
            this._buffer.AddRange(controlCodes);
        }

        public void Print(string text)
        {
            byte[] originatingData = SystemEncoding.GetBytes(text);
            byte[] targetData = System.Text.Encoding.Convert(SystemEncoding, TargetEncoding, originatingData);
            this._buffer.AddRange(targetData);
        }

        public void PrintLine(string text)
        {
            this.Print(text + "\n");
        }

        public abstract void Initialize();
        public abstract void SelectDeviceFont(Enums.Font font);
        public abstract void SetPositionAlignment(Enums.Alignment align);
        public abstract void Bold();
        public abstract void Boldff();
        public abstract void UnderlineOn();
        public abstract void UnderlineOff();
        public abstract void InverseOn();
        public abstract void InverseOff();
    }
}

