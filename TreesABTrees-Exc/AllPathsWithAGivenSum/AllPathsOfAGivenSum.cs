﻿/// <summary>
/// Write a program to read the tree and print all paths of a given sum
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;

internal class AllPathsOfAGivenSum
{
    internal static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

    internal static void Main()
    {
        ReadTree();
        int pathRequiredSum = int.Parse(Console.ReadLine());

        List<string> paths = new List<string>();

        foreach (var kvp in nodeByValue)
        {
            if (kvp.Value.Children.Count == 0)
            {
                Tree<int> currentLeaf = kvp.Value;
                List<int> currentPathMembers = new List<int>();

                while (currentLeaf != null)
                {
                    currentPathMembers.Add(currentLeaf.Value);
                    currentLeaf = currentLeaf.Parent;
                }

                if (currentPathMembers.Sum() == pathRequiredSum)
                {
                    currentPathMembers.Reverse();
                    paths.Add(string.Join(" ", currentPathMembers));
                }
            }
        }

        Console.WriteLine($"Paths of sum {pathRequiredSum}:");
        foreach (string path in paths)
        {
            Console.WriteLine(path);
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