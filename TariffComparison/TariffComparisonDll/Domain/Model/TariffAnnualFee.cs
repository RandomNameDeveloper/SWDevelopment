using NMoneys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWDevelopment.TariffComparison.Domain.Model
{
    public class TariffAnnualFee
    {
        public TariffAnnualFee(string tariffName, Money annualCost)
        {
            this.TariffName = tariffName;
            this.AnnualCost = annualCost;
        }

        public string TariffName { get; }
        public Money AnnualCost { get; }

    }
}
