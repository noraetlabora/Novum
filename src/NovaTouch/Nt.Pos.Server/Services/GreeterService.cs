using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

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
