using System;
using System.IO;
using Nt.Database.Api;

namespace Nt.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Image : IDbImage
    {
        /// <summary>
        /// 
        /// </summary>
        public Image() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Nt.Data.Image GetImage(Nt.Data.Session session, string imageId) 
        {
            // var dbString = Interaction.CallClassMethod("cmNT.OmPrint", "GetFaxNachricht", session.ClientId, imageId);
            // var dataString = new DataString(dbString);
            // var dataList = new DataList(dataString.SplitByChar96());

            // var image = new Nt.Data.Image();
            // image.Width = dataList.GetInt(0);
            // image.Height = dataList.GetInt(1);
            // image.Data = null; //dataList.GetString(2);

            // return image;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SetImage(Nt.Data.Session session, Nt.Data.Image image) 
        {
            throw new NotImplementedException();
        }

        public static string GetDataString(Nt.Data.Image image) 
        {
            var dataString = new System.Text.StringBuilder();
            dataString.Append("FAX").Append(DataString.Char96);
            dataString.Append(image.Width).Append(DataString.Char96);
            dataString.Append(image.Height).Append(DataString.Char96);
            System.Drawing.Bitmap bitmap;

            using (var ms = new System.IO.MemoryStream(image.Data))
            {
                bitmap = new System.Drawing.Bitmap(ms);
            }
            bitmap.Save(@"C:\Temp\" + image.Id + ".bmp");

            for (int y = 0; y < bitmap.Height-7; y=y+8)
            {
                for(int x = 0; x < bitmap.Width; x++)
                {
                    var pixel0 = bitmap.GetPixel(x, y + 0);
                    var bit0 = (pixel0.Name.Equals("ff000000")) ? 1 : 0;
                    var pixel1 = bitmap.GetPixel(x, y + 1);
                    var bit1 = (pixel1.Name.Equals("ff000000")) ? 1 : 0;
                    var pixel2 = bitmap.GetPixel(x, y + 2);
                    var bit2 = (pixel2.Name.Equals("ff000000")) ? 1 : 0;
                    var pixel3 = bitmap.GetPixel(x, y + 3);
                    var bit3 = (pixel3.Name.Equals("ff000000")) ? 1 : 0;
                    var pixel4 = bitmap.GetPixel(x, y + 4);
                    var bit4 = (pixel4.Name.Equals("ff000000")) ? 1 : 0;
                    var pixel5 = bitmap.GetPixel(x, y + 5);
                    var bit5 = (pixel5.Name.Equals("ff000000")) ? 1 : 0;
                    var pixel6 = bitmap.GetPixel(x, y + 6);
                    var bit6 = (pixel6.Name.Equals("ff000000")) ? 1 : 0;
                    var pixel7 = bitmap.GetPixel(x, y + 7);
                    var bit7 = (pixel7.Name.Equals("ff000000")) ? 1 : 0;
                    var _byte = (bit0 * 1) + (bit1 * 2) + (bit2 * 4) + (bit3 * 8) + (bit4 * 16) + (bit5 * 32) + (bit6 * 64) + (bit7 * 128);
                    dataString.Append(DataString.SinglePipe).Append(_byte.ToString());
                }
            }

            return dataString.ToString();
        }

    }
}