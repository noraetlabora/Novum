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
                //check if request is authorized
                if (RequestNeedsAuthorization(httpContext.Request))
                {
                    var session = Data.Sessions.GetSession(httpContext.Request);
                    if (session == null)
                    {
                        LogUnauthorizedRequest(httpContext.Request);
                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }
                }

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
            var bodyAsText = Encoding.UTF8.GetString(buffer).Replace("\n", string.Empty);
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

        private void LogUnauthorizedRequest(HttpRequest request)
        {
            var sb = new StringBuilder();
            sb.Append(request.Method).Append(Pipe);
            sb.Append(request.Path).Append(Pipe);
            sb.Append("unauthorized request - no sessionId in cookies");

            Log.Server.Error(sb.ToString());
        }
    }
}