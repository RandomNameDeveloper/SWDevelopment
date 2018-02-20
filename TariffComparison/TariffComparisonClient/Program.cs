using NMoneys;
using SWDevelopment.TariffComparison.Implementation;
using TariffProviderType = SWDevelopment.TariffComparison.Implementation.CalculatorBuilder.TariffProviderType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffComparisonClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int consumption = 0;
            if (args.Length < 0)
            {
                Console.WriteLine("Please provide consumption.");
            }
            else if (!int.TryParse(args[0], out consumption) || consumption < 1)
            {
                Console.WriteLine("Please provide positive, natural number.");
            }
            else 
            {
                var calculator = CalculatorBuilder.Create(TariffProviderType.InMemory);
                try
                {
                    var fees = await calculator.CalculateAnnualFee(consumption);

                    if (fees.Any())
                    {
                        Console.WriteLine("Tarif name - Annual costs({0}/year)", calculator.CurrencySymbol);
                        foreach (var fee in fees)
                        {
                            Console.WriteLine($"{fee.TariffName} - {fee.AnnualCost.Amount.ToString()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No tariff found.");
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Problem happened: {0}", ex.Message);
                }
            }
        }
    }
}
