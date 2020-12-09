using System;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace AdventOfCode
{
    public static class Day09
    {
        public static void FindFirstNumberNotASum(string filename)
        {
            var numbers = Utility.ReadFileToLongNumbers(filename).ToArray();
            var indexNotASum = FindFirstIndexWhereNotASum(numbers);
            Console.WriteLine($"First number where it isn't a sum of two of" +
                              $" the prior 25 numbers is {numbers[indexNotASum]}");
        }

        public static void FindEncryptionWeakness(string filename)
        {
            var numbers = Utility.ReadFileToLongNumbers(filename).ToArray();
            var indexNotASum = FindFirstIndexWhereNotASum(numbers);
            var target = numbers[indexNotASum];

            var encryptionWeakness = -1L;
            
            for (int a = 0; a < indexNotASum - 1 && encryptionWeakness < 0; a++)
            {
                for (int b = a; b < indexNotASum && Sum(numbers, a, b) <= target && encryptionWeakness < 0; b++)
                {
                    if (Sum(numbers, a, b) == target)
                    {
                        encryptionWeakness = Min(numbers, a, b) + Max(numbers, a, b);
                    }
                }
            }
            
            Console.WriteLine($"The encryption weakness is {encryptionWeakness}");
        }

        private static long Sum(long[] numbers, int start, int end)
        {
            var sum = 0L;
            for (var i = start; i < end; i++) sum += numbers[i];
            return sum;
        }

        private static long Min(long[] numbers, int start, int end)
        {
            var min = long.MaxValue;
            for (var i = start; i<end; i++)
                if (numbers[i] < min)
                    min = numbers[i];
            return min;
        }

        private static long Max(long[] numbers, int start, int end)
        {
            var max = long.MinValue;
            for (var i = start; i<end; i++)
                if (numbers[i] > max)
                    max = numbers[i];
            return max;
        }

        private static int FindFirstIndexWhereNotASum(long[] numbers)
        {
            var indexNotASum = 0;
            for (var i = 25; i < numbers.Length; i++)
            {
                var found = FindWhetherTwoNumbersSumToTarget(i, numbers);
                if (!found)
                {
                    indexNotASum = i;
                    break;
                }
            }

            return indexNotASum;
        }

        private static bool FindWhetherTwoNumbersSumToTarget(int i, long[] numbers)
        {
            var found = false;
            for (var a = i - 25; a < i - 1; a++)
            {
                for (var b = a + 1; b < i; b++)
                {
                    if (numbers[a] + numbers[b] == numbers[i])
                    {
                        Console.WriteLine($"{numbers[a]} + {numbers[b]} = {numbers[i]}");
                        found = true;
                    }
                }
            }

            return found;
        }
    }
}