namespace Nt.Services.Arguments
{
    /// <summary>
    /// 
    /// </summary>
    public class OsArguments
    {
        public string DatabaseIp { get; private set; }
        public string DatabasePort { get; private set; }
        public string DatabaseNamespace { get; private set; }
        public string DatabaseUser { get; private set; }
        public string DatabasePassword { get; private set; }
        public string OsServerIp { get; private set; }
        public string OsServerPort { get; private set; }
        public string OsClientPort { get; private set; }
    }
}
