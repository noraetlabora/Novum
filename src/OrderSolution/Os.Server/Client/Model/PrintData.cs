using System.Collections.Generic;

namespace Os.Server.Client.Model
{
    /// <summary>
    /// http://www.bixolon.com/upload/download/manual_spp-r200iii_command_english_rev_1_00.pdf
    /// </summary>
    public class PrintData
    {
        /// <summary>
        /// buffer contains a list of bytes to be printed
        /// </summary>
        private List<byte> _buffer;

        #region private enums
        private enum OnOff
        {
            ON = 0x01,
		    OFF = 0x00
        }

        private enum Alignment
        {
            LEFT = 0x00,
            CENTER = 0x01,
            RIGHT = 0x02
        }

        private enum Font
        {
            FONT_A = 65,
            FONT_B = 66,
            FONT_C = 67
        }

        private enum CharacterSet
        {
            USA = 0,
            FRANCE = 1,
            GERMANY = 2
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public PrintData(List<string> printLines)
        {
            _buffer = new List<byte>();
            Initialize(CharacterSet.GERMANY);

            PaperFeed(1);
            foreach(var printLine in printLines)
            {
                if (printLine.StartsWith("<FC>"))
                    continue;
                if (printLine.StartsWith("<QR>"))
                {
                    SetAlignment(Alignment.CENTER);
                    SetQr(printLine.Substring(4));
                    SetAlignment(Alignment.LEFT);
                    continue;
                }
                
                if (printLine.StartsWith("<FONT"))
                    GetStartFont(printLine);

                Text(printLine);
                if (printLine.LastIndexOf("<FONT") > 0)
                    GetEndFont(printLine);
            }

            SetAlignment(Alignment.CENTER);
            SetQr("_R1-AT1_fiskaltrust5_ft972#2197_2019-07-02T10:31:07_3,90_0,00_0,00_0,00_0,00_vD3mQZM=_3a22119b_kPeJtyNlSCo=_U2ljaGVyaGVpdHNlaW5yaWNodHVuZyBhdXNnZWZhbGxlbg==");
            SetAlignment(Alignment.LEFT);
            PaperFeed(1);
        }
        #endregion

        #region public methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            return _buffer.ToArray();
        }

        #endregion

        #region private methods

        private void Initialize(CharacterSet characterSet)
		{
			Send(0x1B, 0x40);
            Send(0x1B, 0x52, (byte)characterSet);
		}

        private void Send(params byte[] values) {
			_buffer.AddRange(values);
		}

        private void Text(string line) 
        {
            line = line.Replace("<FONT1>", "");
            line = line.Replace("<FONT2>", "");
            line = line.Replace("<FONT3>", "");
            Send(System.Text.Encoding.Default.GetBytes(line + "\n"));
        }

        private void PaperFeed(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Text("\n");
            }
        }

        private void SetBold(OnOff state)
		{
			Send(0x1B, 0x45, (byte)state);
		}

        private void SetUnderline(OnOff state)
        {
            Send(0x1B, 0x2D, (byte)state);
        }
        private void SetInverse(OnOff state)
		{
			Send(0x1D, 0x42, (byte)state);
		}

        private void SetAlignment(Alignment alignment)
        {
            Send(0x1B, 0x61, (byte)alignment);
        }

        private void SetFont(Font font)
        {
            Send (0x08, 0x4D, 0x00, (byte)font);
        }

        private void SetMode(Font font, bool bold, bool underlined, bool doubleSize)
        {
            byte mode = 0x00;
			if (font == Font.FONT_B)
				mode += 0x01;

			if (bold)
				mode += 0x08;

            if (underlined)
				mode += 0x80;

			if (doubleSize)
				mode += 0x30;

			this.Send (0x1B, 0x21, mode);
        }

        private void GetStartFont(string printLine)
        {
            if (printLine.StartsWith("<FONT1>")) 
            {
                SetFont(Font.FONT_C);
            }
            else if (printLine.StartsWith("<FONT3>"))
            {
                SetMode(Font.FONT_A, false, false, true);
            }
            else
            {
                SetMode(Font.FONT_A, false, false, false);
            }
        }
         private void GetEndFont(string printLine)
        {
            if (printLine.EndsWith("<FONT1>")) 
            {
                SetFont(Font.FONT_C);
            }
            else if (printLine.EndsWith("<FONT3>"))
            {
                SetMode(Font.FONT_A, false, false, true);
            }
            else
            {
                SetMode(Font.FONT_A, false, false, false);
            }
        }

        private void SetQr(string data)
        {
            // set size
            var size = 3;
            Send(0x1d, 0x28, 0x6b, 0x03, 0x00, 0x31, 0x43, (byte)size); // size between 0 and 9 
            // set model
            var model = 50;
            Send(0x1d, 0x28, 0x6b, 0x04, 0x00, 0x31, 0x41, (byte)model, 0x00); //model1=49, model2 = 50
            // set error correction level
            var level = 51;
            Send(0x1d, 0x28, 0x6b, 0x03, 0x00, 0x31, 0x45, (byte)level); // Level L(7%)=48, Level M(15%)=49, Level Q(25%)=50, Level H(30%)=51
            // store data
            var dataBytes = System.Text.Encoding.ASCII.GetBytes(data);
            var dataLength = dataBytes.Length + 3;
            var highByte = (byte)(dataLength / 0xFF);
            var lowByte = (byte)(dataLength % 0xFF);
            Send(0x1d, 0x28, 0x6b, lowByte, highByte, 0x31, 0x50, 0x30);
            Send(dataBytes);
            // print QR
            Send(0x1d, 0x28, 0x6b, 0x03, 0x00, 0x31, 0x51, 0x30);
        }

        #endregion
    }
}