/// <summary>
/// Write a program that reads from the console a sequence of integer numbers (on a single line, separated by a space).
/// Calculate and print the sum and average of the elements of the sequence.
/// Keep the sequence in List<int>. Round the average to second digit after the decimal separator.
/// </summary>
namespace SumAndAverage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class SumAndAverage
    {
        static void Main()
        {
            List<int> input = Console.ReadLine().Split(new char[] { ' ' }).Select(int.Parse).ToList();
            long sum = input.Sum();
            double average = input.Average();

            Console.WriteLine($"Sum={sum}; Average={average:f2}");
        }
    }
}