using System.Text;

namespace Nt.Services.Arguments
{
    /// <summary>
    /// 
    /// </summary>
    public class OsArguments
    {
        public string DatabaseIp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public uint DatabasePort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DatabaseNamespace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DatabaseUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DatabasePassword { get; set; }
        /// <summary>
        /// port in default.json flag "posAdapterUrl"
        /// </summary>
        public uint OsServerPort { get; set; }
        /// <summary>
        /// ip address of Orderman OrderSolutions Server
        /// </summary>
        /// <value></value>
        public string OsClientIp { get; set; }
        /// <summary>
        /// port of Orderman OrderSolutions Server
        /// in default.json in flag httpApiPort"
        /// </summary>
        /// <value></value>
        public uint OsClientPort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OsArguments()
        {
            DatabaseIp = "localhost";
            DatabasePort = 0;
            DatabaseNamespace = "";
            DatabaseUser = "";
            DatabasePassword = "";
            OsServerPort = 0;
            OsClientIp = "localhost";
            OsClientPort = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("OsArguments: ").Append(System.Environment.NewLine);
            builder.Append("dbIp:   ").Append(DatabaseIp).Append(System.Environment.NewLine);
            builder.Append("dbPrt:  ").Append(DatabasePort).Append(System.Environment.NewLine);
            builder.Append("dbNs:   ").Append(DatabaseNamespace).Append(System.Environment.NewLine);
            builder.Append("dbUsr:  ").Append(DatabaseUser).Append(System.Environment.NewLine);
            if (DatabasePassword.Length > 0)
                builder.Append("dbPwd:  <set>").Append(System.Environment.NewLine);
            else
                builder.Append("dbPwd:  <not set>").Append(System.Environment.NewLine);
            builder.Append("osSPrt: ").Append(OsServerPort).Append(System.Environment.NewLine);
            builder.Append("osCIp:  ").Append(OsClientIp).Append(System.Environment.NewLine);
            builder.Append("osCPrt: ").Append(OsClientPort).Append(System.Environment.NewLine);
            return builder.ToString();
        }
    }
}
