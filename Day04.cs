using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day04
    {
        public static void CountValidPassports(string filename)
        {
            var validRecords = ReadPassportsFromFile(filename)
                .Count(p => p.HasAllRequiredKeys);

            Console.WriteLine($"Found {validRecords} valid passports!");
        }

        public static void CountReallyValidPassports(string filename)
        {
            var validRecords = ReadPassportsFromFile(filename)
                .Count(p => p.PassesAllValidators);
            
            Console.WriteLine($"Found {validRecords} really valid passports!");
        }

        private static List<Passport> ReadPassportsFromFile(string filename)
        {
            var records = ConvertRecordStringsToDictionaries(
                Utility.ReadEntireFile(filename)
                    .Split("\n\n")
            );
            return records;
        }

        private static List<Passport> ConvertRecordStringsToDictionaries(string[] records)
        {
            var pattern = new Regex(@"(\S+):(\S+)");
            var passports = new List<Passport>();
            foreach (var record in records)
            {
                var matches = pattern.Matches(record);
                var passport = new Passport();
                foreach (Match match in matches)
                {
                    var groups = match.Groups;
                    var key = groups[1].ToString();
                    var val = groups[2].ToString();
                    passport.Add(key, val);
                }

                passports.Add(passport);
            }

            return passports;
        }
    }

    internal class Passport : Dictionary<string,string>
    {
        private readonly string[] _requiredKeys = new[]
        {
            "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"
        };

        private readonly Regex _fourDigitsPattern = new Regex(@"^\d{4}$");
        private readonly Regex _heightPattern = new Regex(@"^(\d+)(cm|in)$");
        private readonly Regex _hairColourPattern = new Regex(@"^#[0-9a-fA-F]{6}$");
        private readonly Regex _eyeColourPattern = new Regex(@"^amb|blu|brn|gry|grn|hzl|oth$");
        private readonly Regex _passportIdPattern = new Regex(@"^\d{9}$");
        private readonly Dictionary<string, Func<string, bool>> _validators;

        public Passport()
        {
            _validators = new Dictionary<string, Func<string, bool>>
            {
                { "byr", (string value) => _fourDigitsPattern.IsMatch(value) && NumberIsBetween(value, 1920, 2002) },
                { "iyr", (string value) => _fourDigitsPattern.IsMatch(value) && NumberIsBetween(value, 2010, 2020) },
                { "eyr", (string value) => _fourDigitsPattern.IsMatch(value) && NumberIsBetween(value, 2020, 2030) },
                { "hgt", (string value) =>
                    {
                        var matches = _heightPattern.Matches(value);
                        if (matches.Count > 0)
                        {
                            var groups = matches[0].Groups;
                            var length = int.Parse(groups[1].ToString());
                            if (groups[2].ToString() == "cm")
                            {
                                return length >= 150 && length <= 193;
                            }
                            else // "in"
                            {
                                return length >= 59 && length <= 76;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                },
                { "hcl", (string value) => _hairColourPattern.IsMatch(value) },
                { "ecl", (string value) => _eyeColourPattern.IsMatch(value) },
                { "pid", (string value) => _passportIdPattern.IsMatch(value) },
            };
        }
        
        private static bool NumberIsBetween(string value, int low, int high)
        {
            var intValue = int.Parse(value);
            return intValue >= low && intValue <= high;
        }

        public bool HasAllRequiredKeys => _requiredKeys.All(ContainsKey);

        public bool PassesAllValidators => HasAllRequiredKeys && _requiredKeys.All(CallValidatorForKey);

        private bool CallValidatorForKey(string key) => _validators[key](this[key]);
    }
}