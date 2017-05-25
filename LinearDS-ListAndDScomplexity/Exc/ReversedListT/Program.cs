/// <summary>
/// work test on ReversedList
/// </summary>

using System;

public class Program
{
    public static void Main()
    {
        ReversedList<int> proba = new ReversedList<int>();
        proba.Add(1);
        proba.Add(2);
        proba.Add(3);
        proba.Add(4);
        proba.Add(5);
        Console.WriteLine("Count = " + proba.Count);
        Console.WriteLine("Capacity = " + proba.Capacity);
        foreach (int a in proba)
        {
            Console.WriteLine(a);
        }

    }
}