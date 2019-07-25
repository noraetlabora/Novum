using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace Novum.Server.Services
{
    public class AuthenticationService : Authentication.AuthenticationBase
    {
        public override Task<InitializeReply> Initialize(InitializeRequest request, ServerCallContext context)
        {
            var reply = new InitializeReply();
            try 
            {
                reply.UnixTimestamp = 4711;
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, "Initialize");
                var metadata = new Metadata();
                metadata.Add(new Metadata.Entry("title", ""));
                metadata.Add(new Metadata.Entry("message", string.Format("Seriennummer {0} nicht bekannt", request.Id)));
                throw new RpcException(Status.DefaultCancelled, metadata);
            }
            return Task.FromResult(reply);
        }
        
    }
}