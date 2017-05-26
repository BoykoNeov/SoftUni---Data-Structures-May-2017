/// <summary>
/// We are given a labyrinth of size N x N. Some of its cells are empty (0) and some are full (x).
/// We can move from an empty cell to another empty cell if they share common wall. Given a starting position (*)
/// calculate and fill in the array the minimal distance from this 
/// position to any other cell in the array. Use "u" for all unreachable cells.
/// </summary>
using System;
using System.Collections.Generic;

public class DistanceInLabyrinth
{
    public static void Main()
    {
        int labyrinthSize = int.Parse(Console.ReadLine());
        string[,] labyrinth = new string[labyrinthSize, labyrinthSize];

        Queue<Point> traverseOrder = new Queue<Point>();

        for (int y = 0; y < labyrinthSize; y++)
        {
            string labyrinthRow = Console.ReadLine();

            for (int x = 0; x < labyrinthSize; x++)
            {
                labyrinth[y, x] = labyrinthRow[x].ToString();
                if (labyrinth[y, x] == "*")
                {
                    Point start = new Point(y, x, 0);
                    traverseOrder.Enqueue(start);
                }
            }
        }

        while (traverseOrder.Count != 0)
        {
            Point currentPoint = traverseOrder.Dequeue();
            int y = currentPoint.Y;
            int x = currentPoint.X;
            int currentPointDistance = currentPoint.PreviousPointDistance + 1;

            int currentPointValue = 0;

            if (labyrinth[y ,x] != "*")
            {
                if (int.TryParse(labyrinth[y, x], out currentPointValue) && currentPointValue == 0)
                {
                    labyrinth[y, x] = currentPointDistance.ToString();
                }
                else
                {
                    continue;
                }
            }
            else
            {
                currentPointDistance--;
            }

            if (x + 1 < labyrinthSize)
            {
                traverseOrder.Enqueue(new Point(y, x + 1, currentPointDistance));
            }

            if (x - 1 >= 0)
            {
                traverseOrder.Enqueue(new Point(y, x - 1, currentPointDistance));
            }

            if (y + 1 < labyrinthSize)
            {
                traverseOrder.Enqueue(new Point(y + 1, x, currentPointDistance));
            }

            if (y - 1 >= 0)
            {
                traverseOrder.Enqueue(new Point(y - 1, x, currentPointDistance));
            }
        }

        for (int y = 0; y < labyrinthSize; y++)
        {
            for (int x = 0; x < labyrinthSize; x++)
            {
                if (labyrinth[y, x] == "0")
                {
                    labyrinth[y, x] = "u";
                }

                Console.Write(labyrinth[y, x]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}