using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

namespace Novum.Server.Services
{
    public class SystemService : System.SystemBase
    {
        public override Task<Empty> Ping(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new Empty());
        }

    }
}