using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWDevelopment.TariffComparison.Implementation;
using System.Reflection;

namespace TariffComparisonTest
{
    [TestFixture]
    public class CalculatorBuilderTest
    {
        [Test]
        public void CalculatorNotNull()
        {
            var calculator = CalculatorBuilder.Create(CalculatorBuilder.TariffProviderType.InMemory);
            Assert.IsNotNull(calculator);
        }
    }
}
