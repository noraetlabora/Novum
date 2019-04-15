using System;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Apis
    /// </summary>
    public interface IDbApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        Database.Api.IDbMisc Misc { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        Database.Api.IDbTable Table { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        Database.Api.IDbWaiter Waiter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        Database.Api.IDbMenu Menu { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        Database.Api.IDbPrinter Printer { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        Database.Api.IDbPayment Payment { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        Database.Api.IDbModifier Modifier { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        Database.Api.IDbArticle Article { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        Database.Api.IDbOrder Order { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        Database.Api.IDbPos Pos { get; }

        /// <summary>
        /// 
        /// </summary>
        void Initialize();
    }
}