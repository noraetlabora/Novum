using System.Threading.Tasks;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

namespace Novum.Server.Services
{
    public class StaticDataService : StaticData.StaticDataBase
    {
        public override Task<CancellationReasons> GetCancellationReasons(Empty request, ServerCallContext context)
        {
            var cancellationReasons = new CancellationReasons();

            var cancellationReason = new CancellationReason();
            cancellationReason.Id = "1";
            cancellationReason.Name = "Meinung geändert";
            cancellationReasons.CancellationReasons_.Add(cancellationReason);
            cancellationReason = new CancellationReason();
            cancellationReason.Id = "2";
            cancellationReason.Name = "Ausverkauft";
            cancellationReasons.CancellationReasons_.Add(cancellationReason);
            cancellationReason = new CancellationReason();
            cancellationReason.Id = "6";
            cancellationReason.Name = "Reklamation";
            cancellationReasons.CancellationReasons_.Add(cancellationReason);

            return Task.FromResult(cancellationReasons);
        }

        public override Task<Theme> GetTheme(Empty request, ServerCallContext context)
        {
            var theme = new Theme();

            theme.Primary = 0xFF25A146;
            theme.SecondaryVariant = 0xFF262626;
            theme.Secondary = 0xFFFFFFFF;
            theme.Background = 0xFFFFFFFF;
            theme.Surface = 0xFF000000;
            theme.OnPrimary = 0xFF000000;
            theme.OnSecondary = 0xFFAAAAAA;
            theme.OnBackground = 0xFF000000;
            theme.OnSurface = 0xFFFFFFFF;
            
            return Task.FromResult(theme);
        }

    }
}