using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Sdk;

namespace AdventOfCode
{
    public class DayTwo
    {
        private static readonly Regex Pattern = new Regex(@"^(\d+)-(\d+)\s(\w):\s(.*)$");

        public static void CountInvalidPasswordsForSled(string filename)
        {
            var lines = Utility.ReadLinesFromFile("dayTwoInput.txt");

            int validLines = lines.Aggregate(0, (count, line) =>
            {
                var pw = ExtractPasswordWithSpec(line);
                return pw.IsValidForSled ? count+1 : count;
            });
            
            Console.WriteLine($"{validLines} valid Sled passwords in file.");
        }

        public static void CountInvalidPasswordsForToboggan(string filename)
        {
            var lines = Utility.ReadLinesFromFile("dayTwoInput.txt");

            int validLines = lines.Aggregate(0, (count, line) =>
            {
                var pw = ExtractPasswordWithSpec(line);
                return pw.IsValidForToboggan ? count + 1 : count;
            });
            
            Console.WriteLine($"{validLines} valid Toboggan passwords in file.");
        }

        private static PasswordWithSpec ExtractPasswordWithSpec(string line)
        {
            var matches = Pattern.Matches(line);
            Assert.True(matches.Count > 0);
            var match = matches.First();
            var groups = match.Groups;
            PasswordWithSpec pw = new PasswordWithSpec()
            {
                Low = int.Parse(groups[1].ToString()),
                High = int.Parse(groups[2].ToString()),
                Character = groups[3].ToString().ToCharArray()[0],
                Password = groups[4].ToString(),
            };
            return pw;
        }
    }

    public class PasswordWithSpec
    {
        public int Low { get; set; }
        public int High { get; set; }
        public char Character { get; set; }
        public string Password { get; set; }

        // TODO: Externalize strategy, make it a parameter
        
        public bool IsValidForSled
        {
            get
            {
                var count = Password.Count(c => c == Character);
                return count >= Low && count <= High;
            }
        }

        public bool IsValidForToboggan
        {
            get
            {
                var chars = Password.ToCharArray();
                var lowChar = chars[Low-1];
                var highChar = chars[High-1];
                return (lowChar == Character || highChar == Character)
                       && !(lowChar == Character && highChar == Character);
            }
        }
    }
}