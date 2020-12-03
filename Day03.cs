using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;

namespace AdventOfCode
{
    public class Day03
    {
        public static void CountTrees(string filename)
        {
            var map = new Map(filename);
            var treeCount = map.CountTreesOnSlope(3, 1);
            Console.WriteLine($"Found {treeCount} trees!");
        }

        public static void TreeProduct(string filename)
        {
            var map = new Map(filename);
            
            // TODO: Figure out tuples in C# - will it make this less awkward?
            var slopes = new List<int[]>()
            {
                new int[] {1, 1},
                new int[] {3, 1},
                new int[] {5, 1},
                new int[] {7, 1},
                new int[] {1, 2},
            };

            var product = 1;
            foreach (var slope in slopes)
            {
                // TODO: Destructuring with tuple to simplify?
                int sx = slope[0];
                int sy = slope[1];
                var treeCount = map.CountTreesOnSlope(sx, sy);
                product *= treeCount;
            }
            Console.WriteLine($"Found {product} product of tree counts!");
        }
    }

    public class Map
    {
        private List<char[]> _rows;

        public Map(string filename)
        {
            _rows = Utility.ReadLinesFromFile(filename)
                .Select(r => r.ToCharArray())
                .ToList();
        }

        private int Width => _rows[0].Length;
        private int Height => _rows.Count;

        public bool HasTree(int x, int y)
        {
            return _rows[y][x % Width] == '#';
        }

        public int CountTreesOnSlope(int dx, int dy)
        {
            int trees = 0;
            int px = 0;
            for (int py = 0; py < Height; py += dy)
            {
                if (HasTree(px, py)) trees++;
                px += dx;
            }

            return trees;
        }
    }
}