using SWDevelopment.TariffComparison.Domain.Interfaces;
using SWDevelopment.TariffComparison.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWDevelopment.TariffComparison.Implementation
{
    internal class InMemoryTariffProvider : ITariffProvider
    {
        public InMemoryTariffProvider(IEnumerable<Tariff> tariffs)
        {
            this.Tariffs = tariffs;
        }

        public async Task<IEnumerable<Tariff>> GetAll()
        {
            // Just dummy async call
            return await Task.FromResult(this.Tariffs);
        }

        private IEnumerable<Tariff> Tariffs { get; }
    }
}
