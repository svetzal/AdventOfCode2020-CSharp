using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day07
    {
        private static readonly Regex ContainsPattern = new Regex(@"^(.*)\s+contain\s+(.*)\.$");
        private static readonly Regex ContainerColourPattern = new Regex(@"^(.*)\s+bags$");
        private static readonly Regex ContentsPattern = new Regex(@"^(\d+)\s(.*)\sbags?");

        private static readonly List<BagSpec> BagSpecs = new List<BagSpec>();
        private static BagSpecEqualityComparer _bagSpecEqualityComparer = new BagSpecEqualityComparer();

        public static void BagsThatCanEventuallyHoldAShinyGoldBag(string filename)
        {
            LoadBagSpecs(filename);
            var bag = GetOrCreateBagSpec("shiny gold");
            var containers = ContainersThatCanHold(new[] {bag});
            Console.WriteLine($"{containers.Count()} bags can eventually contain one {bag}");
        }

        public static void BagsThatAShinyGoldBagCanHold(string filename)
        {
            if (BagSpecs.Count==0) LoadBagSpecs(filename);
            var bag = GetOrCreateBagSpec("shiny gold");
            var count = CountContainedBags(0, bag);
            Console.WriteLine($"A {bag.Colour} bag contains {count} bags.");
        }

        private static int CountContainedBags(int count, BagSpec bag)
        {
            foreach (var b in bag.PossibleContents)
            {
                count += CountContainedBags(1, b) * bag.Quantities[b];
            }
            return count;
        }

        private static IEnumerable<BagSpec> ContainersThatCanHold(IEnumerable<BagSpec> bags)
        {
            var containers = new HashSet<BagSpec>();
            foreach (var bag in bags)
            {
                var contents = BagSpecs.Where(b => b.PossibleContents.Contains(bag));
                foreach (var c in contents) containers.Add(c);
            }

            if (containers.Any())
            {
                var toAdd = ContainersThatCanHold(containers);
                foreach (var c in toAdd) containers.Add(c);
            }            
            return containers;
        }

        private class BagSpecEqualityComparer : IEqualityComparer<BagSpec>
        {
            public bool Equals(BagSpec x, BagSpec y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Colour == y.Colour;
            }

            public int GetHashCode(BagSpec obj)
            {
                return (obj.Colour != null ? obj.Colour.GetHashCode() : 0);
            }
        }

        private static void LoadBagSpecs(string filename)
        {
            var lines = Utility.ReadLinesFromFile(filename);

            foreach (var line in lines)
            {
                var (container, contents) = GetContainerSpec(line);
                var containerColour = GetContainerColor(container);
                var spec = GetOrCreateBagSpec(containerColour);
                AddPossibleContents(spec, contents);
            }
        }

        private static void AddPossibleContents(BagSpec bagSpec, string contents)
        {
            if (contents == "no other bags") return;

            var items = contents.Split(",").Select(s => s.Trim());
            foreach (var item in items)
            {
                var groups = ContentsPattern.Match(item).Groups;
                var quantity = Convert.ToInt32(groups[1].ToString());
                var colour = groups[2].ToString();

                var spec = GetOrCreateBagSpec(colour);
                bagSpec.AddPossibleContents(quantity, spec);
            }
        }

        private static BagSpec GetOrCreateBagSpec(string colour)
        {
            BagSpec spec;
            var candidates = BagSpecs.Where(b => b.Colour == colour);
            if (candidates.Any())
            {
                spec = candidates.First();
            }
            else
            {
                spec = new BagSpec
                {
                    Colour = colour
                };
                BagSpecs.Add(spec);
            }

            return spec;
        }

        private static string GetContainerColor(string container)
        {
            var groups = ContainerColourPattern.Match(container).Groups;
            return groups[1].ToString();
        }

        private static (string container, string contents) GetContainerSpec(string line)
        {
            var matches = ContainsPattern.Matches(line).First();
            var groups = matches.Groups;
            var container = groups[1].ToString();
            var contents = groups[2].ToString();
            return (container, contents);
        }
    }

    public class BagSpec
    {
        public string Colour { get; set; }
        public List<BagSpec> PossibleContents { get; }
        public Dictionary<BagSpec, int> Quantities { get; }

        public BagSpec()
        {
            PossibleContents = new List<BagSpec>();
            Quantities = new Dictionary<BagSpec, int>();
        }

        public override string ToString()
        {
            return $"{Colour} bag";
        }

        public void AddPossibleContents(int quantity, BagSpec spec)
        {
            PossibleContents.Add(spec);
            Quantities[spec] = quantity;
        }

    }
}