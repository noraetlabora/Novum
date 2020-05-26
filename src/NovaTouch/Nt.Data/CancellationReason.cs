namespace Nt.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class CancellationResason
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public CancellationResason()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public CancellationResason(string id, string name)
        {
            Id = id;
            Name = name;
        }
        #endregion

    }

}