using SWDevelopment.TariffComparison.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWDevelopment.TariffComparison.Domain.Interfaces
{
    interface IAnnualFeeCalculator
    {
        Task<IEnumerable<TariffAnnualFee>> CalculateAnnualFee(int annualConsumption);
    }
}
