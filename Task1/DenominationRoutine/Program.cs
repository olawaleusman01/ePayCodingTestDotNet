namespace DenominationRoutine;
class Program
{
    static void Main(string[] args)
    {
        // Payout Amount
        int payoutAmt = 100;

        // Array declaration for Catridges Denominations
        int[] catridgeDenominations = new int[] { 10, 50, 100 };

        CalculatePayoutCombinations(payoutAmt, catridgeDenominations);
    }


    public static void CalculatePayoutCombinations(int amount, int[] denominations)
    {
        // Check for invalid parameters
        if (amount == 0 || denominations == null || denominations.Length == 0)
            return;

        // Track the current combination
        int[] combinations = new int[denominations.Length];

        //Payout backtracking to calculate combination choices
        PayoutBackTrack(amount, denominations, combinations, 0);
    }

    private static void PayoutBackTrack(int remainingAmount, int[] denominations, int[] combinations, int currentIndex)
    {
        // a check to determine if the Amount Remaining is 0
        if (remainingAmount == 0)
        {
            // Print the current payout combination
            PrintCurrentCombination(combinations, denominations);
            return;
        }

        // Iterate through the denominations
        for (int i = currentIndex; i < denominations.Length; i++)
        {
            // Check if the current denomination is larger than the remaining amount
            if (denominations[i] > remainingAmount)
                continue;

            // Update the current combination
            combinations[i]++;

            //Payout backtracking on amount remaining

            PayoutBackTrack(remainingAmount - denominations[i], denominations, combinations, i);

            // Reset the most current combination
            combinations[i]--;
        }
    }

    private static void PrintCurrentCombination(int[] combinations, int[] denominations)
    {
        //Print Current Payout Possible
        for (int i = 0; i < combinations.Length; i++)
        {
            if (combinations[i] > 0)
                Console.Write(combinations[i] + " x " + denominations[i] + " EUR ");
        }
        Console.WriteLine();
    }

}
