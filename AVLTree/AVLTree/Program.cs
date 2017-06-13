using System;

public class Program
{
    public static void Main()
    {
        AVL<int> tree = new AVL<int>();
        tree.Insert(5);
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(2);

    }
}