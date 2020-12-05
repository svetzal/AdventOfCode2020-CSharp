namespace AdventOfCode
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Day One
            Day01.TwoNumbers("Day01Input.txt");
            Day01.ThreeNumbers("Day01Input.txt");
            
            // Day Two
            Day02.CountInvalidPasswordsForSled("Day02Input.txt");
            Day02.CountInvalidPasswordsForToboggan("Day02Input.txt");
            
            // Day Three
            Day03.CountTrees("Day03Input.txt");
            Day03.TreeProduct("Day03Input.txt");
            
            // Day Four
            Day04.CountValidPassports("Day04Input.txt");
            Day04.CountReallyValidPassports("Day04Input.txt");
        }
    }
}
