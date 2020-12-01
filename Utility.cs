using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    internal static class Utility
    {
        public static IEnumerable<int> ReadFileToNumbers(string filename)
        {
            using var sr = new StreamReader(filename);
            var numberFile = sr.ReadToEnd();
            var numbers = numberFile.Split("\n")
                .Select(int.Parse);
            return numbers;
        }
    }
}