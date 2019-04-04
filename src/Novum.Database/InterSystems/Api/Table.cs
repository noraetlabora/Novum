using System.Collections.Generic;
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

        public Dictionary<string, Novum.Data.Table> GetTables()
        {
            var tables = new Dictionary<string, Novum.Data.Table>();
            var dbString = Interaction.CallClassMethod("cmNT.Tisch", "GetTischListeAll", Data.Department);
            var tablesString = new Novum.Data.Utils.DataString(dbString);
            var tablesArray = tablesString.SplitByDoublePipes();

            foreach (string tableString in tablesArray)
            {
                if (string.IsNullOrEmpty(tableString))
                    continue;
                var table = new Novum.Data.Table(tableString);
                tables.Add(table.Id, table);
            }

            return tables;
        }
    }
}