namespace Nt.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class Waiter
    {
        #region Properties

        /// <summary>
        /// Id of the waiter
        /// </summary>
        /// <value>1, 2, 3, ...</value>
        public string Id { get; set; }

        /// <summary>
        /// Name of the waiter
        /// </summary>
        /// <value>"Chef de rang", "Commis de rang", "Maître d’hôtel"</value>
        public string Name { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Waiter()
        {

        }
        #endregion
    }
}