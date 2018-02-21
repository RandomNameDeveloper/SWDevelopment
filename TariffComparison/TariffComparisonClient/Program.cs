using NMoneys;
using SWDevelopment.TariffComparison.Implementation;
using TariffProviderType = SWDevelopment.TariffComparison.Implementation.CalculatorBuilder.TariffProviderType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWDevelopment.TariffComparison.Domain.Model;

namespace TariffComparisonClient
{
    class Program
    {
        private static string EXIT_COMMAND = "exit";
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var toExit = false;

            var calculator = CalculatorBuilder.Create(TariffProviderType.InMemory);
            foreach (var input in args)
            {
                toExit = await Calculate(calculator, input);
                if (toExit) break;
            }
            
            while (!toExit)
            {
                Console.WriteLine("For exit please type 'exit'.");
                Console.WriteLine("Please provide consumption:");

                var inputStr = Console.ReadLine();
                foreach (var input in inputStr.Split(' '))
                {
                    toExit = await Calculate(calculator, input);
                    if (toExit) break;
                }
            };

            Console.WriteLine("Thanks for using this product!");
        }

        private static async Task<bool> Calculate(AnnualFeeCalculator calculator, string input)
        {
            var isValid = ProcessUserInput(input, out bool toExit, out int consumption);
            if (!toExit && isValid)
            {
                try
                {
                    var fees = await calculator.CalculateAnnualFee(consumption);
                    PrintResult(fees, calculator.CurrencySymbol);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Problem happened: {0}", ex.Message);
                }
            }

            return toExit;
        }

        private static bool ProcessUserInput(string input, out bool toExit, out int consumption)
        {
            consumption = -1;
            toExit = input.ToLower().Contains(EXIT_COMMAND);

            if (!toExit && (!int.TryParse(input, out consumption) || consumption < 0))
            {
                Console.WriteLine($"Consumption: {input} is not valid. Please provide natural number.");
                return false;
            }

            return true;
        }

        private static void PrintResult(IEnumerable<TariffAnnualFee> fees, string currencySymbol)
        {
            if (fees.Any())
            {
                Console.WriteLine("Tarif name - Annual costs({0}/year)", currencySymbol);
                foreach (var fee in fees)
                {
                    Console.WriteLine($"{fee.TariffName} - {fee.AnnualCost.Amount.ToString()}");
                }
                Console.WriteLine('\n');
            }
            else
            {
                Console.WriteLine("No tariff found.");
            }
        }
    }
}
