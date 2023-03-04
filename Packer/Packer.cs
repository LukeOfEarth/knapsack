using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Models;

namespace com.mobiquity.packer
{
    public class Packer
    {
        private const int MaxWeightLimit = 100;

        public string Pack(string filePath)
        {
            try
            {
                var lines = File.ReadLines(filePath);
                var packages = new List<Package>();

                foreach (var line in lines)
                {
                    var parts = line.Split(':');

                    if (parts.Length != 2)
                    {
                        throw new APIException($"Invalid input format: {line}");
                    }

                    var weightLimit = int.Parse(parts[0].Trim());

                    if (weightLimit > MaxWeightLimit)
                    {
                        throw new APIException($"Max weight limit exceeded: {weightLimit}");
                    }

                    var items = ParseItems(parts[1]);

                    var package = PackItems(weightLimit, items);
                    packages.Add(package);
                }

                return string.Join(Environment.NewLine, packages.Select(p => p.ToString()));
            }
            catch (Exception ex)
            {
                throw new APIException("Error processing file", ex);
            }
        }

        private static List<Item> ParseItems(string itemString)
        {
            return itemString.Trim().Split(' ')
                        .Select(s =>
                        {
                            var values = s.Trim('(', ')').Split(',');
                            return new Item(
                                int.Parse(values[0].Trim()),
                                double.Parse(values[1].Trim(), CultureInfo.InvariantCulture),
                                decimal.Parse(values[2].Substring(1))
                            );
                        })
                        .ToList();
        }

        private static Package PackItems(int weightLimit, List<Item> items)
        {
            var itemCombinations = GetItemCombinations(items);

            var possiblePackages = itemCombinations
                .Where(c => c.Sum(i => i.Weight) <= weightLimit)
                .Select(c => new Package { Items = c })
                .ToList();

            if (!possiblePackages.Any())
            {
                return new Package();
            }

            return possiblePackages
                .OrderByDescending(p => p.Items.Sum(i => i.Cost))
                .ThenBy(p => p.Items.Sum(i => i.Weight))
                .First();
        }

        private static List<List<Item>> GetItemCombinations(List<Item> items)
        {
            var result = new List<List<Item>> { new List<Item>() };

            for (var i = 0; i < items.Count; i++)
            {
                var current = items[i];

                for (var j = result.Count - 1; j >= 0; j--)
                {
                    var combination = result[j].ToList();
                    combination.Add(current);
                    result.Add(combination);
                }
            }

            return result;
        }
    }
}
