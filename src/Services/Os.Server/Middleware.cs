using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os.Server
{
    /// <summary>
    ///  Middleware for Logging all Requests and Response
    /// https://salslab.com/a/safely-logging-api-requests-and-responses-in-asp-net-core
    /// </summary>
    public class Middleware
    {
        private static string[] initRequests = {
            "/api/v2/actions/init/registergateway",
            "/api/v2/actions/init/registerclient"
        };

        private static string[] notLoggingRequests = {
            "/api/v2/hoststatus"
        };
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
                var serialNumber = "125-00000000";
                var session = Sessions.GetSession(httpContext.Request);
                if (session == null)
                {
                    //check if request is authorized
                    if (RequestNeedsAuthorization(httpContext.Request))
                    {
                        LogUnauthorizedRequest(httpContext.Request);
                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }
                }
                else
                {
                    serialNumber = session.SerialNumber;
                }

                //read and log request body
                var requestBodyContent = await ReadRequestBody(httpContext.Request, serialNumber);

                var originalBodyStream = httpContext.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    httpContext.Response.Body = responseBody;
                    //do stuff in controllers
                    await _next(httpContext);

                    //read and log response body
                    string responseBodyContent = null;
                    responseBodyContent = await ReadResponseBody(httpContext.Response, serialNumber);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, "Middleware.Invoke");
            }
        }

        private bool RequestNeedsAuthorization(HttpRequest request)
        {
            if (request.Method.Equals("GET"))
            {
                return false;
            }

            if (initRequests.Contains(request.Path.Value.ToLower()))
            {
                return false;
            }

            return true;
        }

        private async Task<string> ReadRequestBody(HttpRequest request, string serialNumber)
        {
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var json = Encoding.UTF8.GetString(buffer).Replace("\n", string.Empty);
            request.Body.Seek(0, SeekOrigin.Begin);

            var sb = new StringBuilder();
            sb.Append("Client Request ").Append("|");
            sb.Append(serialNumber).Append("|");
            sb.Append(request.HttpContext.Connection.Id).Append("|");
            sb.Append(request.Method.Substring(0, 3)).Append("|");
            sb.Append(request.Path.Value).Append(request.QueryString).Append("|");
            sb.Append(json);

            if (!notLoggingRequests.Contains(request.Path.Value.ToLower()))
            {
                Nt.Logging.Log.Communication.Info(sb.ToString());
            }

            return json;
        }

        private async Task<string> ReadResponseBody(HttpResponse response, string serialNumber)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var json = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            var sb = new StringBuilder();
            sb.Append("Server Response").Append("|");
            sb.Append(serialNumber).Append("|");
            sb.Append(response.HttpContext.Connection.Id).Append("|");
            sb.Append(response.StatusCode).Append("|");
            sb.Append(response.HttpContext.Request.Path.Value).Append("|");
            if (json.Length > 500)
            {
                sb.Append(json.Substring(0, 500)).Append("...");
            }
            else
            {
                sb.Append(json);
            }

            if (!notLoggingRequests.Contains(response.HttpContext.Request.Path.Value.ToLower()))
            {
                Nt.Logging.Log.Communication.Info(sb.ToString());
                if (json.Length > 500)
                {
                    Nt.Logging.Log.Communication.Debug(json);
                }
            }

            return json;
        }

        private void LogUnauthorizedRequest(HttpRequest request)
        {
            var sb = new StringBuilder();
            sb.Append(request.Method).Append("|");
            sb.Append(request.Path.Value).Append("|");
            sb.Append("unauthorized request - no sessionId in cookies");

            Nt.Logging.Log.Server.Error(sb.ToString());
        }
    }
}
