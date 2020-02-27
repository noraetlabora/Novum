using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Nt.Printer.Transmission
{
    public class HttpTransmission : ITransmission
    {

        private Uri _printerUrl;

        #region properties

        public HttpResponseMessage Response { get; private set; }
        public Task RequestTask { get; private set; }

        #endregion

        #region constructor

        public HttpTransmission(string printerUrl)
        {
            Uri resultUri;
            if (Uri.TryCreate(printerUrl, UriKind.Absolute, out resultUri))
                _printerUrl = resultUri;
            else
                //return null;
                throw new ArgumentException("Printer URL could not be parsed. printerUrl was " + printerUrl);
        }

        public HttpTransmission(Uri printerUrl)
        {
            _printerUrl = printerUrl;
        }

        #endregion

        public void Send(byte[] data)
        {
            RequestTask = Task.Run(async () =>
            {
                Thread.CurrentThread.Name = "Nt.Print";

                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = new TimeSpan(0, 0, 30);
                try
                {
                    HttpContent content = new ByteArrayContent(data);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    Response = await httpClient.PostAsync(_printerUrl.ToString(), content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("warning - Failed to send print job; url=" + _printerUrl + "; ex=" + ex.Message);
                }
            });
        }
    }
}