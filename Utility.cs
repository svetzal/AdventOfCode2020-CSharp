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
            return ReadLinesFromFile(filename)
                .Select(int.Parse);
        }

        public static string[] ReadLinesFromFile(string filename)
        {
            return File.ReadAllLines(filename).ToArray();
        }

        public static string ReadEntireFile(string filename)
        {
            using var sr = new StreamReader(filename);
            var contents = sr.ReadToEnd();
            return contents;
        }

        public static IEnumerable<long> ReadFileToLongNumbers(string filename)
        {
            return ReadLinesFromFile(filename)
                .Select(long.Parse);
        }
    }
}