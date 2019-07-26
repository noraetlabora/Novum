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

            table = new Table();
            table.Id = "1012";
            table.Name = "12";
            table.Amount = 34.56;
            tables.Tables_.Add(table);

            table = new Table();
            table.Id = "1020";
            table.Name = "20";
            table.Amount = 987.65;
            tables.Tables_.Add(table);

            return Task.FromResult(tables);
        }

    }
}