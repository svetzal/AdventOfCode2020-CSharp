using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day02
    {
        public static void CountInvalidPasswordsForSled(string filename)
        {
            var lines = Utility.ReadLinesFromFile(filename);
            var validator = new SledPasswordValidator();

            int validLines = lines
                .Count(l => validator.Validate(l));

            Console.WriteLine($"{validLines} valid Sled passwords in file.");
        }

        public static void CountInvalidPasswordsForToboggan(string filename)
        {
            var lines = Utility.ReadLinesFromFile(filename);
            var validator = new TobogganPasswordValidator();

            int validLines = lines
                .Count(l => validator.Validate(l));

            Console.WriteLine($"{validLines} valid Toboggan passwords in file.");
        }
    }

    public class PasswordWithSpec
    {
        public int Low { get; set; }
        public int High { get; set; }
        public char Character { get; set; }
        public string Password { get; set; }
    }

    public abstract class Validator
    {
        private static readonly Regex Pattern = new Regex(@"^(\d+)-(\d+)\s(\w):\s(.*)$");

        public abstract bool Validate(string line);

        protected static PasswordWithSpec ExtractPasswordWithSpec(string line)
        {
            var groups = Pattern.Matches(line).First().Groups;
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

    public class SledPasswordValidator : Validator
    {
        public override bool Validate(string line)
        {
            PasswordWithSpec pws = ExtractPasswordWithSpec(line);
            var count = pws.Password.Count(c => c == pws.Character);
            return count >= pws.Low && count <= pws.High;
        }
    }

    public class TobogganPasswordValidator : Validator
    {
        public override bool Validate(string line)
        {
            var pws = ExtractPasswordWithSpec(line);
            var chars = pws.Password.ToCharArray();
            var lowChar = chars[pws.Low - 1];
            var highChar = chars[pws.High - 1];
            return (lowChar == pws.Character || highChar == pws.Character)
                   && !(lowChar == pws.Character && highChar == pws.Character);
        }
    }
}