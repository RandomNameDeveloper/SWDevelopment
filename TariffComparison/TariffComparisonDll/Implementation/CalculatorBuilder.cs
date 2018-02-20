using SWDevelopment.TariffComparison.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWDevelopment.TariffComparison.Implementation
{
    /// <summary>  
    ///  The class contains the responsibility for creating AnnualFeeCalculator.
    /// </summary>
    public class CalculatorBuilder
    {
        public const string BASIC_ELECTRICITY_TARIFF = "basic electricity tariff";
        public const string PACKAGE_TARIFF = "Packaged tariff";

        /// <summary>  
        ///  The enumeration contains currently available types of TariffProviders for AnnualFeeCalculator.
        /// </summary>
        public enum TariffProviderType
        {
            InMemory
        }

        /// <summary>  
        ///  Function for creation AnnualFeeCalculator with particular TariffProvider.
        /// </summary>
        public static AnnualFeeCalculator Create(TariffProviderType providerType)
        {
            switch (providerType)
            {
                case TariffProviderType.InMemory:
                default:
                    return new AnnualFeeCalculator(new InMemoryTariffProvider(PrepareDefaultTariffs()));
            }
        }

        private static IEnumerable<Tariff> PrepareDefaultTariffs()
        {
            return new List<Tariff>
            {
                new Tariff(BASIC_ELECTRICITY_TARIFF, costPerMonth: 5m, costPerConsumption: 0.22m),
                new Tariff(PACKAGE_TARIFF, costPerYear: 800m, costPerConsumption: 0.30m, annualPrePaidConsumption: 4000)
            };
        }
    }
}
