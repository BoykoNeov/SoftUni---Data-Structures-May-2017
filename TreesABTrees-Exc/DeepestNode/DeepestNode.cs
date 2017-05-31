/// <summary>
/// Write a program to read the tree and find the deepest node
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;

internal class DeepestNode
{
    internal static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

    internal static void Main()
    {
        Dictionary<int, int> leafDepth = new Dictionary<int, int>();

        ReadTree();
        List<Tree<int>> results = new List<Tree<int>>();
        foreach (var kvp in nodeByValue)
        {
            if (kvp.Value.Children.Count == 0)
            {
                results.Add(kvp.Value);
                int depth = 0;
                Tree<int> currentLeaf = kvp.Value;
                while (currentLeaf.Parent != null)
                {
                    depth++;
                    currentLeaf = currentLeaf.Parent;
                }

                leafDepth.Add(kvp.Key, depth);
            }
        }

        var deepestNode = leafDepth.OrderByDescending(x => x.Value).FirstOrDefault();
        Console.WriteLine($"Deepest node: {deepestNode.Key}");
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