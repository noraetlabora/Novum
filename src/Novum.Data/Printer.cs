using System;

namespace Novum.Data
{
    public class Printer
    {

        #region Constructor
        public Printer()
        {

        }
        #endregion

        #region Properties

        /// <summary>
        /// Id of the printer
        /// </summary>
        /// <value>"RD1", "RD99"</value>
        public string Id { get; set; }

        /// <summary>
        /// Name of the printer
        /// </summary>
        /// <value>"Rechnungsdrucker 1", "Orderman Gürteldrucker 1"</value>
        public string Name { get; set; }

        /// <summary>
        /// Type of the printer
        /// </summary>
        /// <value>"TM300", </value>
        public string Type { get; set; }

        /// <summary>
        /// Device contains the information where the printerjob has to go
        /// </summary>
        /// <value>"|WL|\\NOV-PROGAPP\BON", "|TCP|192.168.0.187:9100"</value>
        public string Device { get; set; }

        #endregion
    }
}