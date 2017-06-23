using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        KdTree tree = new KdTree();
        tree.Insert(new Point2D(5, 5));
        tree.Insert(new Point2D(3, 2));
        tree.Insert(new Point2D(2, 6));
        tree.Insert(new Point2D(8, 8));
        tree.Insert(new Point2D(8, 9));

        HashSet<Point2D> testHash = new HashSet<Point2D>();
        testHash.Add(new Point2D(1, 2));
        testHash.Add(new Point2D(2, 1));
        testHash.Add(new Point2D(1, 2));
        Console.WriteLine((new Point2D (1,1) == new object() as Point2D));
    }  
}