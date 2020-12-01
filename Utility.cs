using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace AdventOfCode
{
    static internal class Utility
    {
        public static IEnumerable<int> ReadFileToNumbers(string filename)
        {
            using var sr = new StreamReader(filename);
            var numberFile = sr.ReadToEnd();
            var numbers = numberFile.Split("\n")
                .Select(IntegerType.FromString);
            return numbers;
        }
    }
}