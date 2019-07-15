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
                var xx = Nt.Database.DB.Api.Misc.GetCancellationReason();       
                System.Diagnostics.Debug.WriteLine("juhu");      
                Nt.Logging.Log.Server.Error("shit");
                Nt.Logging.Log.Server.Info("happens");   

                
                
                



                
                
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
            return Task.FromResult(reply);
        }
        
    }
}