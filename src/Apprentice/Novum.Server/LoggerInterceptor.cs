using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Novum.Server
{
     public class LoggerInterceptor : Interceptor
    {

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var stopWatch = Stopwatch.StartNew();
        var response = await base.UnaryServerHandler(request, context, continuation);

        stopWatch.Stop();
        System.Diagnostics.Debug.WriteLine("request - response took " + stopWatch.ElapsedMilliseconds + "ms");

        return response;
    }
  }
}