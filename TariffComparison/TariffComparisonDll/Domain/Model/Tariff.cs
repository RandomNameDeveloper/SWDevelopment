using NMoneys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWDevelopment.TariffComparison.Domain.Model
{
    public class Tariff
    {
        public const string TARIFF_NAME_ERROR = "Tariff name should exist.";

        /// <summary>  
        /// DTO for Tariffs
        /// </summary>
        /// <param name="tariffName">
        /// Should be. If null or empty ArgumentException will be thrown.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="tariffName"/> is null or empty.
        /// </exception>
        public Tariff(string tariffName,
            CurrencyIsoCode currencyIsoCode = CurrencyIsoCode.EUR,
            decimal costPerMonth = 0,
            decimal costPerYear = 0,
            decimal costPerConsumption = 0,
            int annualPrePaidConsumption = 0)
        {
            if (string.IsNullOrEmpty(tariffName))
            {
                throw new ArgumentException(TARIFF_NAME_ERROR);
            }
            this.TariffName = tariffName;
            this.CurrencyIsoCode = currencyIsoCode;
            this.CostPerMonth = new Money(costPerMonth, currencyIsoCode);
            this.CostPerYear = new Money(costPerYear, currencyIsoCode);
            this.CostPerConsumption = new Money(costPerConsumption, currencyIsoCode);
            this.AnnualPrePaidConsumption = annualPrePaidConsumption;
        }

        public string TariffName { get; }
        public CurrencyIsoCode CurrencyIsoCode { get; }
        public Money CostPerMonth { get; }
        public Money CostPerConsumption { get; }
        public Money CostPerYear { get; }
        public int AnnualPrePaidConsumption { get; }
    }
}
