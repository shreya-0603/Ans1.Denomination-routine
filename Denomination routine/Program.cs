using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        //available denominations
        int[] denominations = { 10, 50, 100 };

        int[] payoutAmounts = { 30, 50, 60, 80, 140, 230, 370, 610, 980 };
        //If you would like to take a dynamic inputs from User ratherthan given input Use following.
        //Console.WriteLine("Enter payout amounts (comma-separated):");
        //int[] payoutAmounts = Array.ConvertAll(Console.ReadLine().Split(','), int.Parse);

        foreach (int amount in payoutAmounts)
        {
            List<List<int>> combinations = CalculateCombinations(amount, denominations);
            Console.WriteLine($"For {amount} EUR, the available payout denominations would be:");
            foreach (List<int> combo in combinations)
            {
                Dictionary<int, int> noteCounts = combo.GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count());
                string comboStr = string.Join(" + ", noteCounts.Select(kv => $"{kv.Value} x {kv.Key} EUR"));
                Console.WriteLine("• " + comboStr);
            }
            Console.WriteLine();
        }
    }

    static List<List<int>> CalculateCombinations(int payoutAmount, int[] denominations)
    {
        List<List<int>> combinations = new List<List<int>>();
        FindCombinations(payoutAmount, denominations, new List<int>(), combinations);
        return combinations;
    }

    static void FindCombinations(int remainingAmount, int[] denominations, List<int> currentCombination, List<List<int>> combinations)
    {
        if (remainingAmount == 0)
        {
            combinations.Add(new List<int>(currentCombination));
            return;
        }
        if (remainingAmount < 0 || denominations.Length == 0)
            return;

        //include the first denomination
        if (remainingAmount >= denominations[0])
        {
            currentCombination.Add(denominations[0]);
            FindCombinations(remainingAmount - denominations[0], denominations, currentCombination, combinations);
            currentCombination.RemoveAt(currentCombination.Count - 1);
        }

        //exclude the first denomination
        FindCombinations(remainingAmount, denominations[1..], currentCombination, combinations);
    }
}
