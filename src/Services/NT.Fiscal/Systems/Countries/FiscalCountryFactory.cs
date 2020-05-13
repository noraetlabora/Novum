using Nt.Fiscal.Systems.Countries;
using Nt.Fiscal.Systems.Enums;

namespace Nt.Fiscal.Systems.Factories
{
    public class FiscalCountryFactory
    {
        static public FiscalCountry Create(Country country)
        {
            switch (country)
            {
                case Enums.Country.Austria:
                    return new FiscalCountry_Austria();

                case Enums.Country.Germany:
                    return new FiscalCountry_Germany();

                default:
                    return null;
            }
        }
    }
}