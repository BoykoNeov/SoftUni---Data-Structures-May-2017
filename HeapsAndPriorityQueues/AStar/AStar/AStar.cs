using System;
using System.Collections.Generic;

public class AStar
{
    public Dictionary<Node, Node> Parent { get; private set; }
    public Dictionary<Node, int> Cost { get; private set; }
    public PriorityQueue<Node> Open { get; private set; }
    private char[,] Map { get; set; }

    public AStar(char[,] map)
    {
        this.Open = new PriorityQueue<Node>();
        this.Parent = new Dictionary<Node, Node>();
        this.Cost = new Dictionary<Node, int>();
        this.Map = map;
    }



    public static int GetH(Node current, Node goal)
    {
        int deltaX = Math.Abs(current.Col - goal.Col);
        int deltaY = Math.Abs(current.Row - goal.Row);

        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        this.Parent[start] = null;
        this.Cost[start] = 0;
        this.Open.Enqueue(start);

        while (Open.Count > 0)
        {
            Node currentNode = Open.Dequeue();
            if (currentNode.Equals(goal))
            {
                break;
            }


            List<Node> neighbours = new List<Node>
            {
                new Node(currentNode.Row + 1, currentNode.Col),
                new Node(currentNode.Row - 1, currentNode.Col),
                new Node(currentNode.Row, currentNode.Col + 1),
                new Node(currentNode.Row, currentNode.Col - 1)
            };

            foreach (Node neighbour in neighbours)
            {
                if (neighbour.Row >= Map.GetLength(0) || (neighbour.Row < 0))
                {
                    continue;
                }

                if (neighbour.Col >= Map.GetLength(1) || (neighbour.Col < 0))
                {
                    continue;
                }

                if (Map[neighbour.Row, neighbour.Col] == 'W' || Map[neighbour.Row, neighbour.Col] == 'P')
                {
                    continue;
                }

                int newCost = Cost[currentNode] + 1;

                if (!Cost.ContainsKey(neighbour) || Cost[neighbour] > newCost)
                {
                    Cost[neighbour] = newCost;
                    neighbour.F = newCost + GetH(neighbour, goal);
                    Open.Enqueue(neighbour);
                    Parent[neighbour] = currentNode;
                }

            }
        }

        Node lastNode = goal;
        if (!Parent.ContainsKey(lastNode))
        {
            yield return start;
        }
        else
        {
            yield return lastNode;
            while (Parent[lastNode] != null)
            {
                yield return Parent[lastNode];
                lastNode = Parent[lastNode];
            }
        }



    }
}