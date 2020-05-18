using Nt.Fiscal.Systems.Enums;

namespace Nt.Fiscal
{
    public class ProviderConfiguration
    {
        #region Provider configuration

        public Country Country { get; set; }
        public Provider Provider { get; set; }

        public string ProviderLocation { get; set; }

        #endregion Provider configuration

        #region Methods

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("Provider Configuration: ");
            builder.Append("Provider = ").Append(Provider.ToString()).Append("|");
            builder.Append("Country = ").Append(Country.ToString()).Append("|");
            builder.Append("Location = ").Append(ProviderLocation);
            return builder.ToString();
        }

        #endregion Methods
    }
}