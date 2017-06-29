using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

/// <summary>
/// Contains Main() for task "Shopping center" and interprets console commands, outputs to the Console
/// </summary>
class ShoppingCenter
{
    static void Main()
    {
        ProductList collection = new ProductList();

        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            string line = Console.ReadLine();

            string command = line.Substring(0, line.IndexOf(' '));
            line = line.Substring(command.Length + 1, line.Length - command.Length - 1);

            switch (command)
            {
                case "AddProduct":
                    {
                        List<string> parameters = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        string producer = parameters.Last();
                        parameters.RemoveAt(parameters.Count - 1);

                        decimal price = decimal.Parse(parameters.Last());
                        parameters.RemoveAt(parameters.Count - 1);

                        string productName = parameters[0];

                        collection.Add(productName, price, producer);
                        Console.WriteLine("Product added");

                    }
                    break;

                case "DeleteProducts":
                    {
                        List<string> deleteParameters = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        int itemsDeletedCount = 0;

                        if (deleteParameters.Count > 1)
                        {
                            string productName = deleteParameters[0];
                            string producer = deleteParameters[1];
                            itemsDeletedCount = collection.DeleteByNameAndProducer(productName, producer);
                        }
                        else
                        {
                            string producer = deleteParameters[0];
                            itemsDeletedCount = collection.DeleteByProducer(producer);
                        }

                        if (itemsDeletedCount == 0)
                        {
                            Console.WriteLine("No products found");
                        }
                        else
                        {
                            Console.WriteLine($"{itemsDeletedCount} products deleted");
                        }
                    }
                    break;

                case "FindProductsByName":
                    {
                        OrderedBag<Product> resultsByName = collection.FindByName(line);

                        if (resultsByName == null)
                        {
                            Console.WriteLine("No products found");
                            break;
                        }

                        foreach (Product product in resultsByName)
                        {
                            Console.WriteLine(product.ToString());
                        }
                    }
                    break;

                case "FindProductsByProducer":
                    {
                        OrderedBag<Product> resultsByProducer = collection.FindByProducer(line);
                        if (resultsByProducer == null)
                        {
                            Console.WriteLine("No products found");
                            break;
                        }

                        foreach (Product product in resultsByProducer)
                        {
                            Console.WriteLine(product.ToString());
                        }
                    }
                    break;

                case "FindProductsByPriceRange":
                    {
                        List<string> findParameters = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        decimal lowerPrice = decimal.Parse(findParameters[0]);
                        decimal higherPrice = decimal.Parse(findParameters[1]);

                        OrderedBag<Product> resultsByPrice = collection.FindByPriceRange(lowerPrice, higherPrice);

                        if (resultsByPrice == null)
                        {
                            Console.WriteLine("No products found");
                            break;
                        }

                        foreach (Product product in resultsByPrice)
                        {
                            Console.WriteLine(product.ToString());
                        }
                    }
                    break;
            }
        }
    }
}