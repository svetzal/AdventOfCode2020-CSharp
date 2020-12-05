using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day05
    {
        public static void HighestSeatId(string filename)
        {
            var lines = Utility.ReadLinesFromFile(filename);
            var highest = lines.Max(MakeBinaryInt);
            Console.WriteLine($"The highest seat ID I see is {highest}");
        }

        public static void MySeatId(string filename)
        {
            var lines = Utility.ReadLinesFromFile(filename);
            var ids = lines.Select(MakeBinaryInt)
                .OrderBy(i => i)
                .ToArray();
            for (var i = 0; i < ids.Length - 1; i++)
            {
                if (ids[i + 1] - ids[i] > 1)
                    Console.WriteLine($"My seat ID is {ids[i] + 1}");
            }

            // Console.WriteLine($"My seat ID is {mine}");
        }

        private static int MakeBinaryInt(string line)
        {
            return Convert.ToInt32(MakeBinaryString(line), 2);
        }

        private static string MakeBinaryString(string line)
        {
            return line
                .Replace('B', '1')
                .Replace('R', '1')
                .Replace('F', '0')
                .Replace('L', '0');
        }
    }
}