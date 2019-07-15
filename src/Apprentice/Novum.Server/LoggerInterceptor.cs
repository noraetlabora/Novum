using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Text;

namespace Novum.Server
{
    public class LoggerInterceptor : Interceptor
    {

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {

            var sb = new StringBuilder();
            sb.Append("|Request|").Append(request.ToString());
            Nt.Logging.Log.Communication.Info(sb.ToString());
            var response = await base.UnaryServerHandler(request, context, continuation);
            sb = new StringBuilder();
            sb.Append("|Response|").Append(response.ToString());
            Nt.Logging.Log.Communication.Info(sb.ToString());

            return response;
        }
    }
}