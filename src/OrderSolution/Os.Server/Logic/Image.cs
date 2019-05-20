using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Os.Server.Logic
{
    /// <summary>
    /// .
    /// </summary>
    public class Image
    {

        private static Dictionary<string, Nt.Data.Image> _images = new Dictionary<string, Nt.Data.Image>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageId"></param>
        public static Stream GetImageStream(string imageId)
        {
            if (!_images.ContainsKey(imageId))
                return null;
            return new MemoryStream(_images[imageId].Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageId"></param>
        public static Nt.Data.Image GetImage(string imageId)
        {
            if (!_images.ContainsKey(imageId))
                return null;
            return _images[imageId];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="stream"></param>
        public static string SaveNewImage(Nt.Data.Session session, Stream stream)
        {
            var imageId = GetImagePrefix(session) + DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
            SaveImage(imageId, stream);
            return imageId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static void SaveImage(string imageId, Stream stream) 
        {
            var ntImage = new Nt.Data.Image();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                    stream.CopyTo(memoryStream);
                    ntImage.Data = memoryStream.ToArray();
            }

            using(System.Drawing.Image _image = System.Drawing.Image.FromStream(stream))
            {
                ntImage.Width = _image.Width;
                ntImage.Height = _image.Height;
            }
            
            _images.Add(imageId, ntImage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public static void RemoveImages(Nt.Data.Session session)
        {
           var prefix = GetImagePrefix(session);

            foreach(var key in _images.Keys.ToList()) 
            {
                if (key.StartsWith(prefix))
                    _images.Remove(key);
            }
        }

        private static string GetImagePrefix(Nt.Data.Session session)
        {
             return session.Id + "_";
        }
    }
}