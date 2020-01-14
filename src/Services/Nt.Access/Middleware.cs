using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Access
{
    /// <summary>
    ///  Middleware for Logging all Requests and Response
    /// https://salslab.com/a/safely-logging-api-requests-and-responses-in-asp-net-core
    /// </summary>
    public class Middleware
    {

        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public Middleware(RequestDelegate next)
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
                var requestBodyContent = await ReadRequestBody(httpContext.Request);

                var originalBodyStream = httpContext.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    httpContext.Response.Body = responseBody;
                    await _next(httpContext);

                    //read and log response body
                    string responseBodyContent = null;
                    responseBodyContent = await ReadResponseBody(httpContext.Response);
                    await responseBody.CopyToAsync(originalBodyStream);
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
            var bodyAsText = Encoding.UTF8.GetString(buffer).Replace("\n", string.Empty);
            request.Body.Seek(0, SeekOrigin.Begin);

            var sb = new StringBuilder();
            sb.Append("Client Request ").Append("|");
            sb.Append(request.HttpContext.Connection.Id).Append("|");
            sb.Append(request.Method.Substring(0, 3)).Append("|");
            sb.Append(request.Path.Value).Append(request.QueryString).Append("|");
            sb.Append(bodyAsText);

            Nt.Logging.Log.Communication.Info(sb.ToString());

            return bodyAsText;
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            var sb = new StringBuilder();
            sb.Append("Server Response").Append("|");
            sb.Append(response.HttpContext.Connection.Id).Append("|");
            sb.Append(response.StatusCode).Append("|");
            sb.Append(response.HttpContext.Request.Path.Value).Append("|");
            if (bodyAsText.Length > 500)
            {
                sb.Append(bodyAsText.Substring(0, 500)).Append("...");
            }
            else
            {
                sb.Append(bodyAsText);
            }

            Nt.Logging.Log.Communication.Info(sb.ToString());
            if (bodyAsText.Length > 500)
            {
                Nt.Logging.Log.Communication.Debug(bodyAsText);
            }

            return bodyAsText;
        }
    }
}
