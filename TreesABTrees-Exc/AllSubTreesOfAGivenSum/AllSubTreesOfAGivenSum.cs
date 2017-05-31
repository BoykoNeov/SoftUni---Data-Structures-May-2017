/// <summary>
/// Write a program to read the tree and print all subtrees of a given sum
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;

internal class AllSubTreesOfAGivenSum
{
    internal static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

    internal static void Main()
    {
        var a = nodeByValue;
        ReadTree();
        int treeRequiredSum = int.Parse(Console.ReadLine());
        List<string> results = new List<string>();

        foreach (var kvp in nodeByValue)
        {
            Tree<int> currentNode = kvp.Value;
            List<int> currentTreeTraverse = currentNode.PreOrderDFS().ToList();

            if (currentTreeTraverse.Sum() == treeRequiredSum)
            {
                results.Add(string.Join(" ", currentTreeTraverse));
            }
        }

        Console.WriteLine($"Subtrees of sum {treeRequiredSum}:");
        foreach (string tree in results)
        {
            Console.WriteLine(tree);
        }
    }

    internal static Tree<int> GetTreeNodeByValue(int value)
    {
        if (!nodeByValue.ContainsKey(value))
        {
            nodeByValue[value] = new Tree<int>(value);
        }

        return nodeByValue[value];
    }

    internal static void AddEdge(int parent, int child)
    {
        Tree<int> parentNode = GetTreeNodeByValue(parent);
        Tree<int> childNode = GetTreeNodeByValue(child);

        parentNode.Children.Add(childNode);
        childNode.Parent = parentNode;
    }

    internal static void ReadTree()
    {
        int nodeCount = int.Parse(Console.ReadLine());
        for (int i = 1; i < nodeCount; i++)
        {
            string[] edge = Console.ReadLine().Split();
            AddEdge(int.Parse(edge[0]), int.Parse(edge[1]));
        }
    }

    internal static Tree<int> GetRootNode()
    {
        return nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
    }
}