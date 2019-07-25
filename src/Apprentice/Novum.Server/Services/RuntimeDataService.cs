using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

namespace Novum.Server.Services
{
    public class RuntimeDataService : RuntimeData.RuntimeDataBase
    {
        public override Task<Tables> GetTables(Empty request, ServerCallContext context)
        {
            var tables = new Tables();

            var table = new Table();
            table.Id = "1010";
            table.Name = "10";
            table.Amount = 12.34;

            tables.Tables_.Add(table);

            return Task.FromResult(tables);
        }

    }
}