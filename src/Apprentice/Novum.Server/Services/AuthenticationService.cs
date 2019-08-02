using System;
using System.Threading.Tasks;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

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

        public override Task<LoginReply> Login(LoginRequest request, ServerCallContext context)
        {
            var reply = new LoginReply();
            try 
            {
                //logic
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Communication.Error(ex);
            }
            return Task.FromResult(reply);
        }

        public override Task<Empty> Logout(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new Empty());
        }
    }
}
