/// <summary>
/// We are given the following sequence of numbers:
///•	S1 = N
///•	S2 = S1 + 1
///•	S3 = 2* S1 + 1
///•	S4 = S1 + 2
///•	S5 = S2 + 1
///•	S6 = 2* S2 + 1
///•	S7 = S2 + 2
///•	…
///Using the Queue<T> class, write a program to print its first 50 members for given N
/// </summary>
using System;
using System.Collections.Generic;

public class CalculateSequenceWithQueue
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        List<long> results = new List<long>();
        Queue<long> queue = new Queue<long>();
        queue.Enqueue(n);
        results.Add(n);

        while (results.Count < 50)
        {
            long k = queue.Dequeue();

            long firstOperator = k + 1;
            long secondOperator = (k * 2) + 1;
            long thirdOperator = k + 2;

            queue.Enqueue(firstOperator);
            queue.Enqueue(secondOperator);
            queue.Enqueue(thirdOperator);

            results.Add(firstOperator);

            //it really should contain one more such a check for adding the last element, but currently it works fine for n = 50 ;)
            if (results.Count == 50)
            {
                break;
            }
            results.Add(secondOperator);
            results.Add(thirdOperator);
        }

        Console.WriteLine(string.Join(", ", results));
    }
}