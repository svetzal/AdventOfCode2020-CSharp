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
            
            // Day Five
            Day05.HighestSeatId("Day05Input.txt");
            Day05.MySeatId("Day05Input.txt");
            
            // Day Six
            Day06.SumOfAllQuestionsAnsweredYes("Day06Input.txt");
            Day06.SumOfAllQuestionsAllYes("Day06Input.txt");
            
            // Day Seven
            Day07.BagsThatCanEventuallyHoldAShinyGoldBag("Day07Input.txt");
            Day07.BagsThatAShinyGoldBagCanHold("Day07Input.txt");
            
            // Day Eight
            Day08.FindAccumulatorAtInfiniteLoop("Day08Input.txt");
            Day08.MutateToFindNormalExitAccumulator("Day08Input.txt");
        }
    }
}
