using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class Utility
    {
        public static IEnumerable<int> ReadFileToNumbers(string filename)
        {
            var lines = ReadLinesFromFile(filename);
            var numbers = lines
                .Select(int.Parse);
            return numbers;
        }

        public static string[] ReadLinesFromFile(string filename)
        {
            var numberFile = ReadEntireFile(filename);
            var lines = numberFile.Split("\n");
            return lines;
        }

        public static string ReadEntireFile(string filename)
        {
            using var sr = new StreamReader(filename);
            var contents = sr.ReadToEnd();
            return contents;
        }
    }
}