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
            var treeCount = map.CountTreesOnSlope((3, 1));
            Console.WriteLine($"Found {treeCount} trees!");
        }

        public static void TreeProduct(string filename)
        {
            var map = new Map(filename);
            var slopes = new[] {(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)};
            var product = slopes
                .Aggregate(1, (int p, (int,int)slope) => p * map.CountTreesOnSlope(slope));

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

        private bool HasTree(int x, int y)
        {
            return _rows[y][x % Width] == '#';
        }

        public int CountTreesOnSlope((int, int) slope)
        {
            var (dx, dy) = slope;
            var trees = 0;
            var px = 0;
            for (var py = 0; py < Height; py += dy)
            {
                if (HasTree(px, py)) trees++;
                px += dx;
            }

            return trees;
        }
    }
}