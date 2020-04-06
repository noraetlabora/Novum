using Nt.Logging;
using NT.Fiscal.Systems.Providers;
using NT.Fiscal.Systems.Providers.Efsta;

namespace NT.Fiscal.Systems.Factories
{
    public class FiscalProviderFactory
    {
        static public FiscalProvider Create(ProviderConfiguration providerConfiguration)
        {
            var country = FiscalCountryFactory.Create(providerConfiguration.Country);
            switch (providerConfiguration.Provider)
            {
                case Enums.Provider.Efsta:
                    Log.Server.Info(new System.Text.StringBuilder().Append("Creating ").Append("Efsta provider for ").Append(country.Country.ToString("g")));
                    return new FiscalProviderEfsta(country);

                default:
                    Log.Server.Warn(new System.Text.StringBuilder().Append("No provider configured. Creating default ").Append("Efsta provider for ").Append(country.Country.ToString("g")));
                    return new FiscalProviderEfsta(country);
            }
        }
    }
}