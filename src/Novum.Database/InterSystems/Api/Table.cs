using System;
using System.Collections.Generic;
using InterSystems.Data.CacheClient;
using Novum.Database.Api;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Table : IDbTable
    {
        public Table()
        {

        }

        public Dictionary<string, Data.Table> GetTables(string department)
        {
            var tables = new Dictionary<string, Data.Table>();

            // var dbString = DB.Iris.ClassMethodString("cmNT.Tisch", "GetTischListeAll", department);
            // var tablesString = new Data.Utils.DataString(dbString);
            // var tablesArray = tablesString.SplitByDoublePipes();

            // foreach (string tableString in tablesArray)
            // {
            //     if (string.IsNullOrEmpty(tableString))
            //         continue;
            //     var table = new Data.Table(tableString);
            //     tables.Add(table.Id, table);
            // }

            return tables;
        }
    }
}