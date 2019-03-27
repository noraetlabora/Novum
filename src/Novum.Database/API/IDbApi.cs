using System;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbApi
    {
        Database.API.IDbMisc Misc { get; }
        Database.API.IDbTable Table { get; }
        Database.API.IDbUser User { get; }
        Database.API.IDbMenu Menu { get; }
        Database.API.IDbPrinter Printer { get; }
        Database.API.IDbPayment Payment { get; }
        Database.API.IDbModifier Modifier { get; }
        Database.API.IDbArticle Article { get; }
    }
}