using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Booking.Middlewares
{
    /// <summary>
    ///  Middleware for Logging all Requests and Response
    /// https://salslab.com/a/safely-logging-api-requests-and-responses-in-asp-net-core
    /// </summary>
    public class LogMiddleware
    {

        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                //read and log request body
                var requestBodyContent = await ReadRequestBody(httpContext.Request).ConfigureAwait(false);

                var originalBodyStream = httpContext.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    httpContext.Response.Body = responseBody;
                    await _next(httpContext).ConfigureAwait(false);

                    //read and log response body
                    string responseBodyContent = null;
                    responseBodyContent = await ReadResponseBody(httpContext.Response).ConfigureAwait(false);
                    await responseBody.CopyToAsync(originalBodyStream).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, "Middleware.Invoke");
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var json = Encoding.UTF8.GetString(buffer).Replace("\n", string.Empty);
            request.Body.Seek(0, SeekOrigin.Begin);
            LogJson("Client Request ", request.HttpContext.TraceIdentifier, request.Method.Substring(0, 3), request.Path.Value + request.QueryString, json);
            return json;
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var json = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            LogJson("Server Response", response.HttpContext.TraceIdentifier, response.StatusCode.ToString(), response.HttpContext.Request.Path.Value, json);
            return json;
        }

        private static void LogJson(string messageType, string traceId, string statusMethod, string path, string json)
        {
            var sb = new StringBuilder();
            sb.Append(NtBooking.BookingSystem.Type).Append("|");
            sb.Append(messageType).Append("|");
            sb.Append(traceId).Append("|");
            sb.Append(statusMethod).Append("|");
            sb.Append(path).Append("|");
            if (json.Length > 500)
            {
                sb.Append(json.Substring(0, 500)).Append("...");
            }
            else
            {
                sb.Append(json);
            }

            Nt.Logging.Log.Communication.Info(sb.ToString());
            if (json.Length > 500)
            {
                Nt.Logging.Log.Communication.Debug(json);
            }
        }
    }
}
