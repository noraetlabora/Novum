using System;

namespace Nt.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 
        /// </summary>
         public enum PermissionType
        {
            /// <summary>
            /// 
            /// </summary>
            VoidCommitedOrder,
            /// <summary>
            /// 
            /// </summary>
            ModifierFaxInput
        }

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value>""</value>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>""</value>
        public string Program { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public PermissionType Type { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public bool Permitted { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="program"></param>
        /// <param name="type"></param>
        /// <param name="permitted"></param>
        public Permission(string name, string program, PermissionType type, bool permitted)
        {
            Name = name;
            Program = program;
            Type = type;
            Permitted = permitted;
        }
        #endregion
    }
}