using NMoneys;
using SWDevelopment.TariffComparison.Domain.Interfaces;
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
    public class AnnualFeeCalculator : IAnnualFeeCalculator
    {
        private const string SHOULD_BE_POSITIVE = "Should be positive number.";
        private readonly ITariffProvider tariffProvider;

        public string CurrencySymbol
        {
            get
            {
                return Currency.Euro.Symbol;
            }
        }

        internal AnnualFeeCalculator(ITariffProvider tariffProvider)
        {
            this.tariffProvider = tariffProvider;
        }

        /// <summary>  
        /// This method Calculate Annuual Fee.
        /// </summary>
        /// <param name="annualConsumption">
        /// Should be positive number. If not ArgumentOutOfRangeException will be thrown.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="annualConsumption"/> is negative or 0.
        /// </exception>
        public async Task<IEnumerable<TariffAnnualFee>> CalculateAnnualFee(int annualConsumption)
        {
            if (annualConsumption < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(annualConsumption), SHOULD_BE_POSITIVE);
            }
            return (await this.tariffProvider.GetAll())
                .Select(x => AnnualCalculation(annualConsumption, x))
                .OrderBy(x => x.AnnualCost);
        }

        private TariffAnnualFee AnnualCalculation(int annualConsumption, Tariff tariff)
        {
            // First count and add subscription fee
            var annualFee = new Money(tariff.CostPerYear + tariff.CostPerMonth * 12);
            if (annualConsumption > tariff.AnnualPrePaidConsumption)
            {
                var paidConsumption = (annualConsumption - tariff.AnnualPrePaidConsumption);
                var consumptionFee = Money.Multiply(tariff.CostPerConsumption, paidConsumption);
                annualFee+=consumptionFee;
            }
            return new TariffAnnualFee(tariff.TariffName, annualFee);
        }
    }
}
