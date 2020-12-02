namespace AdventOfCode
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Day One
            DayOne.TwoNumbers("dayOneInput.txt");
            DayOne.ThreeNumbers("dayOneInput.txt");
            
            // Day Two
            DayTwo.CountInvalidPasswordsForSled("dayTwoInput.txt");
            DayTwo.CountInvalidPasswordsForToboggan("dayTwoInput.txt");
        }
    }
}
