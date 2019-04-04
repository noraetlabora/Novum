using System;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Apis
    /// </summary>
    public interface IDbApi
    {
        Database.Api.IDbMisc Misc { get; }
        Database.Api.IDbTable Table { get; }
        Database.Api.IDbWaiter Waiter { get; }
        Database.Api.IDbMenu Menu { get; }
        Database.Api.IDbPrinter Printer { get; }
        Database.Api.IDbPayment Payment { get; }
        Database.Api.IDbModifier Modifier { get; }
        Database.Api.IDbArticle Article { get; }
        Database.Api.IDbOrder Order { get; }
        void Initialize();
    }
}