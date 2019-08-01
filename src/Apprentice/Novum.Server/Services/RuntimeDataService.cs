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

                var timespan = (DateTime.Now - ntTable.Updated);

                if (timespan.TotalMinutes < 2) 
                    table.State = TableState.Ordered;
                else if (timespan.TotalMinutes < 5)
                    table.State = TableState.Waiting;  
                else
                    table.State = TableState.Impatient;
                
                tables.Tables_.Add(table);
            }

            // var tables = new Tables();

            // var table = new Table();
            // table.Id = "1010";
            // table.Name = "10";
            // table.Amount = 12.34;
            // table.State = TableState.Ordered;
            // tables.Tables_.Add(table);


            // table = new Table();
            // table.Id = "1012";
            // table.Name = "12";
            // table.Amount = 34.56;
            // table.State = TableState.Waiting;
            // tables.Tables_.Add(table);

            // table = new Table();
            // table.Id = "1020";
            // table.Name = "20";
            // table.Amount = 987.65;
            // table.State = TableState.Impatient;
            // tables.Tables_.Add(table);

            // table = new Table();
            // table.Id = "1030";
            // table.Name = "30";
            // table.Amount = 45.90;
            // table.State = TableState.Ordered;
            // tables.Tables_.Add(table);

            // table = new Table();
            // table.Id = "1040";
            // table.Name = "40";
            // table.Amount = 30.34;
            // table.State = TableState.Impatient;
            // tables.Tables_.Add(table);

            // table = new Table();
            // table.Id = "1013";
            // table.Name = "13";
            // table.Amount = 105.55;
            // table.State = TableState.Ordered;
            // tables.Tables_.Add(table);




            return Task.FromResult(tables);
        }

    }
}