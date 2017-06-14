using System;

class Program
{
    static void Main()
    {
        AVL<int> tree = new AVL<int>();
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(5);
        tree.Insert(130);
        tree.Insert(230);
        tree.Insert(330);
        tree.Insert(-30);
        tree.Insert(430);
        tree.Insert(530);
        tree.Insert(630);
        tree.Insert(730);
        tree.Insert(830);
        tree.Insert(930);
        tree.Insert(3030);
        tree.Delete(-30);
            


        Console.WriteLine();
        Console.WriteLine(tree.FindMin(tree.Root).Value);
    }
}
