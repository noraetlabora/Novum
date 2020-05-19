using Nt.Fiscal.Systems.Providers;
using Nt.Fiscal.Systems.Providers.Efsta;
using Nt.Fiscal.Systems.Providers.Fiskaltrust;
using Nt.Logging;

namespace Nt.Fiscal.Systems.Factories
{
    public class FiscalProviderFactory
    {
        static public FiscalProvider Create(ProviderConfiguration providerConfiguration)
        {
            var country = FiscalCountryFactory.Create(providerConfiguration.Country);
            if (country is null)
            {
                Log.Server.Error(new System.Text.StringBuilder().Append("Country ").Append(providerConfiguration.Country.ToString("g")).Append(" is not implemented"));
                return null;
            }
            switch (providerConfiguration.Provider)
            {
                case Enums.Provider.Efsta:
                    Log.Server.Info(new System.Text.StringBuilder().Append("Creating ").Append("Efsta provider for ").Append(country.Country.ToString("g")));
                    var providerEfsta = new FiscalProviderEfsta(country);
                    if (!providerEfsta.SupportedCountries.Contains(country.Country))
                    {
                        Log.Server.Error(new System.Text.StringBuilder().Append("Country ").Append(country.Country.ToString("g")).Append(" is not supported for Efsta provider"));
                        return null;
                    }
                    return providerEfsta;

                case Enums.Provider.Fiskaltrust:
                    Log.Server.Info(new System.Text.StringBuilder().Append("Creating ").Append("Fiskaltrust provider for ").Append(country.Country.ToString("g")));
                    var providerFiskaltrust = new FiscalProviderFiskaltrust(country);
                    if (!providerFiskaltrust.SupportedCountries.Contains(country.Country))
                    {
                        Log.Server.Error(new System.Text.StringBuilder().Append("Country ").Append(country.Country.ToString("g")).Append(" is not supported for Fiskaltrust provider"));
                        return null;
                    }
                    return providerFiskaltrust;

                default:
                    Log.Server.Error(new System.Text.StringBuilder().Append("No provider configured."));
                    return null;
            }
        }
    }
}