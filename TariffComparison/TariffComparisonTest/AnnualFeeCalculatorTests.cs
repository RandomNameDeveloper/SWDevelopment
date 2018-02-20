using NUnit.Framework;
using System;
using SWDevelopment.TariffComparison.Implementation;
using System.Linq;
using System.Collections.Generic;
using SWDevelopment.TariffComparison.Domain.Model;
using System.Threading.Tasks;

namespace TariffComparisonTest
{
    [TestFixture]
    public class AnnualFeeCalculatorTests
    {
        private readonly AnnualFeeCalculator calculator;

        public AnnualFeeCalculatorTests()
        {
            this.calculator = CalculatorBuilder.Create(CalculatorBuilder.TariffProviderType.InMemory);
        }

        [Test]
        public void ShouldThrowExceptionForIncorrectParameter()
        {
            Assert.Throws<ArgumentOutOfRangeException>(async () => await calculator.CalculateAnnualFee(-1));
        }

        [Test]
        public async Task TestInputConsumption1000()
        {
            var results = await calculator.CalculateAnnualFee(1000);
            Assert.AreEqual(GetCostForBasicTariff(results), 280.00m);
            Assert.AreEqual(GetCostForPackageTariff(results), 800.00m);
        }

        [Test]
        public async Task TestInputConsumption3500()
        {
            var results = await calculator.CalculateAnnualFee(3500);
            Assert.AreEqual(GetCostForBasicTariff(results), 830.00m);
            Assert.AreEqual(GetCostForPackageTariff(results), 800.00m);
        }

        [Test]
        public async Task TestInputConsumption4500()
        {
            var results = await calculator.CalculateAnnualFee(4500);
            Assert.AreEqual(GetCostForBasicTariff(results), 1050.00m);
            Assert.AreEqual(GetCostForPackageTariff(results), 950.00m);
        }

        [Test]
        public async Task TestInputConsumption6000()
        {
            var results = await calculator.CalculateAnnualFee(6000);
            Assert.AreEqual(GetCostForBasicTariff(results), 1380.00m);
            Assert.AreEqual(GetCostForPackageTariff(results), 1400.00m);
        }

        [Test]
        public async Task TestInputConsumption10000()
        {
            var results = await calculator.CalculateAnnualFee(10000);
            Assert.AreEqual(GetCostForBasicTariff(results), 2260.00m);
            Assert.AreEqual(GetCostForPackageTariff(results), 2600.00m);
        }

        private static decimal GetCostForBasicTariff(IEnumerable<TariffAnnualFee> results)
        {
            return ReadCostForTariff(results, CalculatorBuilder.BASIC_ELECTRICITY_TARIFF);
        }

        private static decimal GetCostForPackageTariff(IEnumerable<TariffAnnualFee> results)
        {
            return ReadCostForTariff(results, CalculatorBuilder.PACKAGE_TARIFF);
        }

        private static decimal ReadCostForTariff(IEnumerable<TariffAnnualFee> results, string tariffName)
        {
            return results.Where(x => x.TariffName == tariffName).Select(x => x.AnnualCost.Amount).FirstOrDefault();
        }
    }
}
