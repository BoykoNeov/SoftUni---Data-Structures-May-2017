/// <summary>
/// Write a program that reads N integers from the console and reverses them using a stack.
/// Use the Stack<int> class from .NET Framework. Just put the input numbers in the stack and pop them. 
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;

public class ReverseNumbersWithStack
{
    public static void Main()
    {
        Stack<int> stack = new Stack<int>();
        IEnumerable<int> x = Console.ReadLine().Split().Select(int.Parse);
        foreach (int number in x)
        {
            stack.Push(number);
        }

        while (stack.Count > 0)
        {
            Console.Write(stack.Pop().ToString() + " ");
        }
    }
}