using Os.Server.Client.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Os.Server.Client.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPrintingApi
    {
        /// <summary>
        /// Get Job information Get a list of print jobs for this printer with &#x60;id&#x60; on the local server (the one you are currently talking to) 
        /// </summary>
        /// <param name="printerId">printer id</param>
        /// <param name="jobId">print job id</param>
        /// <returns>PrintJob</returns>
        PrintJob GetPrintJobInfo(string printerId, string jobId);
        /// <summary>
        /// Get information for this printer 
        /// </summary>
        /// <param name="printerId">printer id</param>
        /// <returns>Printer</returns>
        Printer GetPrinter(string printerId);
        /// <summary>
        /// Add a new print job to this printer queue 
        /// </summary>
        /// <param name="printerId">printer id</param>
        /// <param name="printLines"></param>
        /// <returns></returns>
        void PostPrintJob(Nt.Data.Session session, List<string> printLines);
        /// <summary>
        /// Get list of jobs for this printer; note: the list of jobs will be cleaned up after some time Get a list of print jobs for this printer with &#x60;id&#x60; on the local server (the one you are currently talking to) 
        /// </summary>
        /// <param name="printerId">printer id</param>
        /// <returns>List&lt;PrintJob&gt;</returns>
        List<PrintJob> PrintersPrinterIdJobsGet(string printerId);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class PrintingApi : IPrintingApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrintingApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PrintingApi(String baseUrl)
        {
            BaseUrl = baseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string BaseUrl { get; private set; }

        /// <summary>
        /// Get Job information Get a list of print jobs for this printer with &#x60;id&#x60; on the local server (the one you are currently talking to) 
        /// </summary>
        /// <param name="printerId">printer id</param>
        /// <param name="jobId">print job id</param>
        /// <returns>PrintJob</returns>
        public PrintJob GetPrintJobInfo(string printerId, string jobId)
        {
            return null;
        }

        /// <summary>
        /// Get information for this printer 
        /// </summary>
        /// <param name="printerId">printer id</param>
        /// <returns>Printer</returns>
        public Printer GetPrinter(string printerId)
        {
            return null;
        }

        /// <summary>
        /// Add a new print job to this printer queue 
        /// </summary>
        /// <param name="printerId">printer id</param>
        /// <param name="printLines"></param>
        /// <returns></returns>
        public async void PostPrintJob(Nt.Data.Session session, List<string> printLines)
        {
            var requestPath = session.Printer + "/jobs";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var printData = new PrintData(printLines);
                    var printBytes = printData.ToBytes();
                    var requestIdentifier = new Microsoft.AspNetCore.Http.Features.HttpRequestIdentifierFeature();

                    var content = new ByteArrayContent(printBytes);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                    var sb = new StringBuilder();
                    sb.Append("Server Request ").Append("|");
                    sb.Append(session.SerialNumber).Append("|");
                    sb.Append(requestIdentifier.TraceIdentifier).Append("|");
                    sb.Append("POS").Append("|");
                    sb.Append(requestPath).Append("|");
                    var printDataString = printData.ToString();
                    //delete carriage return line feed (new lines) for logs
                    printDataString = printDataString.Replace(System.Environment.NewLine, "CRLF");

                    //shorten printDataString to max 500 characters in info log
                    if (printDataString.Length > 500)
                        sb.Append(printDataString.Substring(0, 500)).Append("...");
                    else
                        sb.Append(printDataString);

                    Nt.Logging.Log.Communication.Info(sb.ToString());

                    //write debug log if printDataStrinis over 500 characters long
                    if (printDataString.Length > 500)
                        Nt.Logging.Log.Communication.Debug(printDataString);

                    var requestUri = BaseUrl + requestPath;
                    var response = await httpClient.PostAsync(requestUri, content);

                    sb = new StringBuilder();
                    sb.Append("Client Response").Append("|");
                    sb.Append(session.SerialNumber).Append("|");
                    sb.Append(requestIdentifier.TraceIdentifier).Append("|");
                    sb.Append((int)response.StatusCode).Append("|");
                    Nt.Logging.Log.Communication.Info(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, requestPath);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return;
        }

        /// <summary>
        /// Get list of jobs for this printer; note: the list of jobs will be cleaned up after some time Get a list of print jobs for this printer with &#x60;id&#x60; on the local server (the one you are currently talking to) 
        /// </summary>
        /// <param name="printerId">printer id</param>
        /// <returns>List&lt;PrintJob&gt;</returns>
        public List<PrintJob> PrintersPrinterIdJobsGet(string printerId)
        {
            return null;
        }

    }
}
