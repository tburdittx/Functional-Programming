using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgramming
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int number = 10000;
            var countOfPoisonedApples = PickApples().Take(number).Count(apple => apple.Poisoned);
            Console.WriteLine("{0}", countOfPoisonedApples);

            var mostFrequentNonRedPoisonedColour = PickApples().Take(number)
                .Where(apple => apple.Colour != "Red")
                .GroupBy(apple => apple.Colour)
                .OrderByDescending(group => group.Count())
                .First()
                .Key;
            Console.WriteLine($"{mostFrequentNonRedPoisonedColour}");

            var longestRunOfRedsCSharp7 = PickApples().Take(number)
                .Aggregate((Current: 0, Max: 0),
                (acc, apple) => apple.Colour == "Red" && !apple.Poisoned ?
                (acc.Current + 1, Math.Max(acc.Current + 1, acc.Max)) : (0, acc.Max)).Item2;
            Console.WriteLine($"{longestRunOfRedsCSharp7}");

          
            var greenPairs = PickApples().Take(number)
               .Zip(PickApples().Skip(1), (a, b) => a.Colour == "Green" && b.Colour == "Green")
               .Where(apple => apple).ToList().Count;
          
            Console.WriteLine($"{greenPairs}");

            Console.ReadLine();
        }
        private static IEnumerable<Apple> PickApples()
        {
            int colourIndex = 1;
            int poisonIndex = 7;

            while (true)
            {
                yield return new Apple
                {
                    Colour = GetColour(colourIndex),
                    Poisoned = poisonIndex % 41 == 0
                };

                colourIndex += 5;
                poisonIndex += 37;
            }
        }

        private static string GetColour(int colourIndex)
        {
            if (colourIndex % 13 == 0 || colourIndex % 29 == 0)
            {
                return "Green";
            }

            if (colourIndex % 11 == 0 || colourIndex % 19 == 0)
            {
                return "Yellow";
            }

            return "Red";
        }

        private class Apple
        {
            public string Colour { get; set; }
            public bool Poisoned { get; set; }

            public override string ToString()
            {
                return $"{Colour} apple{(Poisoned ? " (poisoned!)" : "")}";
            }
        }
    }
}
