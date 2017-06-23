using System;
using System.Collections.Generic;
using System.Linq;

public class SweepAndPruneLauncher
{
    public static void Main()
    {
        Dictionary<string, Item> ByName = new Dictionary<string, Item>();
        List<Item> itemList = new List<Item>();
        int tickCount = 0;
        bool gameStarted = false;

        string input = string.Empty;

        while ((input = Console.ReadLine()) != "end")
        {
            string[] parameters = input.Split();
            switch (parameters[0])
            {
                case "add":
                    {
                        Item item = new Item(parameters[1], int.Parse(parameters[2]), int.Parse(parameters[3]));
                        ByName.Add(parameters[1], item);
                        itemList.Add(item);
                        SortList(itemList);
                        break;
                    }

                case "start":
                    {
                        gameStarted = true;
                        break;
                    }

                case "tick":
                    {
                        tickCount++;
                        break;
                    }

                case "move":
                    {
                        ByName[parameters[1]].X1 = int.Parse(parameters[2]);
                        ByName[parameters[1]].Y1 = int.Parse(parameters[3]);
                        SortList(itemList);
                        break;
                    }
            }

            if (gameStarted && tickCount > 0)
            {
                CheckColisions(itemList);
            }

        }
    }

    private static void SortList(List<Item> itemList)
    {
        itemList.Sort();
    }

    private static void CheckColisions(List<Item> itemList)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            for (int j = 1; j < itemList.Count; j++)
            {

            }
        }
    }
}