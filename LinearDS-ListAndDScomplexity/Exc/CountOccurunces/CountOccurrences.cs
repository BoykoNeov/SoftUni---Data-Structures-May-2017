/// <summary>
/// Write a program that finds in given array of integers how many times each of them occurs. 
/// The input sequence holds numbers in range [0…1000]. The output should hold all numbers that 
/// occur at least once along with their number of occurrences. 
/// </summary>
namespace CountOccurrences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CountOccurrences
    {
        public static void Main()
        {


            string[] inputs = Console.ReadLine().Split();
            Dictionary<int, int> occurences = new Dictionary<int, int>();

            for (int i = 0; i < inputs.Length; i++)
            {
                int currentName = int.Parse(inputs[i]);
                if (!occurences.ContainsKey(currentName))
                {
                    occurences.Add(currentName, 0);
                }

                occurences[currentName]++;
            }

            foreach (var kvp in occurences.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value} times");
            }
        }
    }
}