using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Nt.Pos.Server
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<ByeResponse> SayBye(ByeRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ByeResponse
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
