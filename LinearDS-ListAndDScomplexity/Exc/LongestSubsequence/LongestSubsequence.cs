/// <summary>
/// Write a method that finds the longest subsequence of equal numbers in given List<int> and
/// returns the result as new List<int>. If several sequences has the same longest length, 
/// return the leftmost of them. Write a program to test whether the method works correctly. 
/// </summary>
namespace LongestSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LongestSubsequence
    {
        public static void Main()
        {
            List<int> inputs = Console.ReadLine().Split().Select(int.Parse).ToList();

            List<int> output = CalculateLongestSubsequence(inputs);
            Console.WriteLine(string.Join(" ", output));
        }

        public static List<int> CalculateLongestSubsequence(List<int> inputs)
        {
            int longestCount = 0;
            int longestCountNumber = 0;
            int currentCount = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                int previousNumber;
                int currentNumber = inputs[i];

                if (i > 0)
                {
                    previousNumber = inputs[i - 1];
                }
                else
                {
                    previousNumber = currentNumber;
                    longestCountNumber = currentNumber;
                }

                if (currentNumber.Equals(previousNumber))
                {
                    currentCount++;
                }
                else
                {
                    currentCount = 1;
                }

                if (currentCount > longestCount)
                {
                    longestCount = currentCount;
                    longestCountNumber = currentNumber;
                }
            }

            List<int> outputSequence = new List<int>();
            for (int i = 0; i < longestCount; i++)
            {
                outputSequence.Add(longestCountNumber);
            }

            return outputSequence;
        }
    }
}