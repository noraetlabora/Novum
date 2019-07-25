using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace Novum.Server.Services
{
    public class StaticDataService : StaticData.StaticDataBase
    {
        public override Task<CancellationReasons> GetCancellationReasons(Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            var cancellationReasons = new CancellationReasons();

            var cancellationReason = new CancellationReason();
            cancellationReason.Id = "1";
            cancellationReason.Name = "Meinung geändert";
            cancellationReasons.CancellationReasons_.Add(cancellationReason);
            cancellationReason = new CancellationReason();
            cancellationReason.Id = "2";
            cancellationReason.Name = "Ausverkauft";
            cancellationReasons.CancellationReasons_.Add(cancellationReason);
            cancellationReason = new CancellationReason();
            cancellationReason.Id = "6";
            cancellationReason.Name = "Reklamation";
            cancellationReasons.CancellationReasons_.Add(cancellationReason);

            return Task.FromResult(cancellationReasons);
        }

    }
}