/// <summary>
/// Write a program that reads from the console a sequence of words (strings on a single line, separated by a space).
/// Sort them alphabetically. Keep the sequence in List<string>.
/// </summary>
namespace SortWords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SortWords
    {
        public static void Main()
        {
            List<string> input = Console.ReadLine().Split().ToList();
            Console.WriteLine(string.Join(" ", input.OrderBy(x => x)));
        }
    }
}