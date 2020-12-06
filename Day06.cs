using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode
{
    public class Day06
    {
        public static void SumOfAllQuestionsAnsweredYes(string filename)
        {
            var groups = LoadGroups(filename);

            var flattened = groups
                .Select(
                    group => new HashSet<char>(
                        group.Aggregate(new List<char>(), WhereAnyWereYes)
                    )
                );

            var summed = CountQuestions(flattened).Sum();

            Console.WriteLine($"Sum of grouped questions where any yes is {summed}");
        }

        public static void SumOfAllQuestionsAllYes(string filename)
        {
            var groups = LoadGroups(filename);

            var flattened = groups
                .Select(
                    group => new HashSet<char>(
                        group.Aggregate(
                            @"abcdefghijklmnopqrstuvwxyz".ToCharArray().ToList(),
                            WhereAllWereYes
                        )
                    )
                );

            var summed = CountQuestions(flattened).Sum();

            Console.WriteLine($"Sum of grouped questions where all yes is {summed}");
        }

        private static List<char> WhereAnyWereYes(List<char> list, char[] questions)
        {
            var union = list.Union(questions).OrderBy(c => c).ToList();
            return union;
        }

        private static List<char> WhereAllWereYes(List<char> list, char[] questions)
        {
            var intersected = list.Intersect(questions).OrderBy(c => c).ToList();
            return intersected;
        }

        private static char[][][] LoadGroups(string filename)
        {
            var groups = Utility.ReadEntireFile(filename)
                .Split("\n\n")
                .Select(g => g.Split("\n").Select(l => l.ToCharArray()).ToArray())
                .ToArray();
            return groups;
        }

        private static IEnumerable<int> CountQuestions(IEnumerable<HashSet<char>> groups)
        {
            var counted = groups.Select(questions => questions.Count);
            return counted;
        }
    }
}