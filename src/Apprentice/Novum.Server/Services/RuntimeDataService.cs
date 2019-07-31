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
            var session = new Nt.Data.Session();
            session.ClientId = "1001";
            session.WaiterId = "987";
            session.PosId = "RK2";
            session.SerialNumber = "125-123456789";

            var ntTables = Nt.Database.DB.Api.Table.GetTables(session);

            foreach (var ntTable in ntTables.Values)
            {
                var table = new Table();
                table.Id = ntTable.Id;
                table.Name = ntTable.Name;
                table.Amount = (double)ntTable.Amount;
                table.Guests = ntTable.Guests;



                var timespan = (DateTime.Now - ntTable.Updated).TotalMinutes;

                if (timespan > 5)
                {
                    table.State = TableState.Impatient;

                }
                else if (timespan > 2)
                {
                    table.State = TableState.Waiting;
                }
                else
                {
                    table.State = TableState.Ordered;
                }



                tables.Tables_.Add(table);
            }





            return Task.FromResult(tables);
        }

    }
}