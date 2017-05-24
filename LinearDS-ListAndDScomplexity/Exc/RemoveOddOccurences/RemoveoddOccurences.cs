/// <summary>
/// Write a program that removes from given sequence all numbers that occur odd number of times.
/// </summary>
namespace RemoveOddOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RemoveOddOccurences
    {
        public static void Main()
        {
            string[] inputs = Console.ReadLine().Split();
            Dictionary<string, int> occurences = new Dictionary<string, int>();

            for (int i = 0; i < inputs.Length; i++)
            {
                string currentString = inputs[i];
                if (!occurences.ContainsKey(currentString))
                {
                    occurences.Add(currentString, 0);
                }

                occurences[currentString]++;
            }

            StringBuilder output = new StringBuilder();
            foreach (string input in inputs)
            {
                if (occurences[input] % 2 == 0)
                {
                    output.Append($"{input} ");
                }
            }

            Console.WriteLine(output.ToString().Trim());
        }
    }
}