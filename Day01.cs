using System;
using System.Linq;

namespace AdventOfCode
{
    public static class Day01
    {
        public static void TwoNumbers(string filename)
        {
            var numbers = Utility.ReadFileToNumbers(filename)
                .ToList();

            for (var a = 0; a < numbers.Count; a++)
            {
                for (var b = a + 1; b < numbers.Count; b++)
                {
                    if (numbers[a] + numbers[b] == 2020)
                        Console.WriteLine(
                            $"{numbers[a]} * {numbers[b]} = {numbers[a] * numbers[b]}");
                }
            }
        }
        
        public static void ThreeNumbers(string filename)
        {
            var numbers = Utility.ReadFileToNumbers(filename)
                .ToList();

            for (var a = 0; a < numbers.Count; a++)
            {
                for (var b = a + 1; b < numbers.Count; b++)
                {
                    for (var c = b + 1; c < numbers.Count; c++)
                    {
                        if (numbers[a] + numbers[b] + numbers[c] == 2020)
                            Console.WriteLine(
                                $"{numbers[a]} * {numbers[b]} * {numbers[c]} = {numbers[a] * numbers[b] * numbers[c]}");
                    }
                }
            }
        }
    }
}