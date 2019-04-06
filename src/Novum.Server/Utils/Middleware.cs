using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Linq;


namespace Novum.Server.Utils
{
    /// <summary>
    ///  Middleware for Logging all Requests and Response
    /// https://salslab.com/a/safely-logging-api-requests-and-responses-in-asp-net-core
    /// </summary>
    public class Middleware
    {
        private static char Pipe = '|';
        private static string[] initRequests = {"/api/v2/actions/Init/RegisterGateway",
                                                "/api/v2/actions/Init/RegisterClient"};
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
                var request = httpContext.Request;
                var requestBodyContent = await ReadRequestBody(request);
                if (RequestNeedsAuthorization(request))
                {
                    if (!request.Cookies.ContainsKey("sessionId"))
                    {
                        Log.Server.Error(string.Format("Middleware.Invoke|{0}|{1} had no cookie 'sessionId'", request.Method, request.Path.Value));
                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }
                }

                var originalBodyStream = httpContext.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    var response = httpContext.Response;
                    response.Body = responseBody;
                    await _next(httpContext);

                    string responseBodyContent = null;
                    responseBodyContent = await ReadResponseBody(response);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                Log.Server.Error("Middleware.Invoke|" + ex.Message);
                Log.Server.Error("Middleware.Invoke|" + ex.StackTrace);
            }
            finally
            {

            }
        }

        private bool RequestNeedsAuthorization(HttpRequest request)
        {
            if (request.Method.Equals("GET"))
                return false;
            if (initRequests.Contains(request.Path.Value))
                return false;
            return true;
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            var sb = new StringBuilder();
            sb.Append("Request ").Append(Pipe);
            sb.Append(request.HttpContext.TraceIdentifier).Append(Pipe);
            sb.Append(request.Method.Substring(0, 3)).Append(Pipe);
            sb.Append(request.Path).Append(Pipe);
            sb.Append(bodyAsText);

            Log.Json.Info(sb.ToString());

            return bodyAsText;
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            var sb = new StringBuilder();
            sb.Append("Response").Append(Pipe);
            sb.Append(response.HttpContext.TraceIdentifier).Append(Pipe);
            sb.Append(response.StatusCode).Append(Pipe);
            sb.Append(response.HttpContext.Request.Path).Append(Pipe);
            sb.Append(bodyAsText);

            Log.Json.Info(sb.ToString());

            return bodyAsText;
        }
    }
}