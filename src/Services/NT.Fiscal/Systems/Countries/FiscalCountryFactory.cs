using NT.Fiscal.Systems.Countries;
using NT.Fiscal.Systems.Enums;

namespace NT.Fiscal.Systems.Factories
{
    public class FiscalCountryFactory
    {
        static public FiscalCountry Create(Country country)
        {
            switch (country)
            {
                case Enums.Country.Austria:
                    return new FiscalCountry_Austria();

                default:
                    return new FiscalCountry_Austria();
            }
        }
    }
}