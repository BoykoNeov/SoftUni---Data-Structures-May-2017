using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

/// <summary>
/// Botched up solution for "Shopping Center", keeping it for the sake of "How it shouldn't be done" :)
/// Check v2
/// </summary>
class Program
{
    public static void Main()
    {
        Dictionary<string, HashSet<Product>> byName = new Dictionary<string, HashSet<Product>>();
        Dictionary<string, HashSet<Product>> byProducer = new Dictionary<string, HashSet<Product>>();
        Dictionary<string, Dictionary<string, HashSet<Product>>> byNameByProducer = new Dictionary<string, Dictionary<string, HashSet<Product>>>();
        OrderedDictionary<decimal, HashSet<Product>> byPrice = new OrderedDictionary<decimal, HashSet<Product>>();

        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {

            List<string> inputs = Console.ReadLine().Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            switch (inputs[0])
            {
                case "AddProduct":
                    {
                        inputs.RemoveAt(0);
                        decimal price = decimal.Parse(inputs[inputs.Count - 2]);
                        string producer = inputs[inputs.Count - 1];
                        inputs.RemoveRange(inputs.Count - 2, 2);
                        string name = string.Join(" ", inputs);

                        Product newProduct = new Product(name, producer, price);

                        // Add product to dictionary by name
                        if (!byName.ContainsKey(name))
                        {
                            byName.Add(name, new HashSet<Product>());
                        }
                        byName[name].Add(newProduct);

                        // Add product to dictionary by producer
                        if (!byProducer.ContainsKey(producer))
                        {
                            byProducer.Add(producer, new HashSet<Product>());
                        }
                        byProducer[producer].Add(newProduct);

                        // Add product to dictionary by name and producer
                        if (!byNameByProducer.ContainsKey(name))
                        {
                            byNameByProducer.Add(name, new Dictionary<string, HashSet<Product>>());
                        }

                        if (!byNameByProducer[name].ContainsKey(producer))
                        {
                            byNameByProducer[name].Add(producer, new HashSet<Product>());
                        }
                        byNameByProducer[name][producer].Add(newProduct);

                        // Add product by price
                        if (!byPrice.ContainsKey(price))
                        {
                            byPrice.Add(price, new HashSet<Product>());
                        }
                        byPrice[price].Add(newProduct);

                        Console.WriteLine("Product added");
                        break;
                    }

                // Two cases
                case "DeleteProducts":
                    {
                        inputs.RemoveAt(0);
                        string producer = string.Empty;
                        string name = string.Empty;
                        decimal price = 0m;

                        int deleteCount = 0;

                        if (inputs.Count == 1)
                        {
                            producer = inputs[0];

                            if (!byProducer.ContainsKey(producer))
                            {
                                Console.WriteLine("No products found");
                                break;
                            }
                            else
                            {
                                foreach (Product product in byProducer[producer])
                                {
                                    name = product.Name;
                                    price = product.Price;
                                    producer = product.Producer;

                                    // remove product from byName
                                    byName[name].Remove(product);

                                    // remove product from byNameByProducer
                                    byNameByProducer[name][producer].Remove(product);

                                    // Remove product from byPrice
                                    byPrice[price].Remove(product);

                                }

                                // deletes entry (producer) from dictionary "by producer"
                                deleteCount = byProducer[producer].Count;
                                byProducer.Remove(producer);

                                if (deleteCount == 0)
                                {
                                    Console.WriteLine("No products found");
                                    break;
                                }
                                Console.WriteLine($"{deleteCount} products deleted");
                            }
                        }
                        else
                        {
                            producer = inputs.Last();
                            inputs.RemoveAt(inputs.Count - 1);

                            name = string.Join(" ", inputs);

                            if (!byNameByProducer.ContainsKey(name) || !byNameByProducer[name].ContainsKey(producer))
                            {
                                Console.WriteLine("No products found");
                                break;
                            }

                            deleteCount = byNameByProducer[name][producer].Count;

                            foreach (Product product in byNameByProducer[name][producer])
                            {
                                // remove product from byName
                                byName[name].Remove(product);

                                // remove products from byProducer
                                byProducer[producer].Remove(product);

                                // Remove product from byPrice
                                price = product.Price;
                                byPrice[price].Remove(product);
                            }

                            // Remove entries from byNameByProducer
                            byNameByProducer[name].Remove(producer);

                            if (deleteCount == 0)
                            {
                                Console.WriteLine("No products found");
                                break;
                            }

                            Console.WriteLine($"{deleteCount} products deleted");
                        }

                        break;
                    }


                case "FindProductsByName":
                    {
                        int foundProductsCount = 0;

                        inputs.RemoveAt(0);
                        string name = string.Join(" ",inputs);
                        if (!byName.ContainsKey(name))
                        {
                            Console.WriteLine("No products found");
                            break;
                        }

                        foundProductsCount = byName[name].Count;

                        foreach (Product product in byName[name].OrderBy(x => x.Name).ThenBy(x => x.Producer).ThenBy(x => x.Price))
                        {
                            Console.Write("{");
                            Console.Write("{0};{1};{2:f2}", product.Name, product.Producer, product.Price);
                            Console.WriteLine("}");
                        }

                        if (foundProductsCount == 0)
                        {
                            Console.WriteLine("No products found");
                            break;
                        }

                        break;
                    }

                case "FindProductsByProducer":
                    {
                        int foundProductsCount = 0;

                        string producer = inputs[1];
                        if (!byProducer.ContainsKey(producer))
                        {
                            Console.WriteLine("No products found");
                            break;
                        }

                        foundProductsCount = byProducer[producer].Count;

                        foreach (Product product in byProducer[producer].OrderBy(x => x.Name).ThenBy(x => x.Producer).ThenBy(x => x.Price))
                        {
                            Console.Write("{");
                            Console.Write("{0};{1};{2:f2}", product.Name, product.Producer, product.Price);
                            Console.WriteLine("}");
                        }

                        if (foundProductsCount == 0)
                        {
                            Console.WriteLine("No products found");
                            break;
                        }

                        break;
                    }

                case "FindProductsByPriceRange":
                    {
                        decimal lowerBoundary = decimal.Parse(inputs[1]);
                        decimal higherBoundary = decimal.Parse(inputs[2]);
                        var priceRange = byPrice.Range(lowerBoundary, true, higherBoundary, true);

                        if (priceRange.Count == 0)
                        {
                            Console.WriteLine("No products found");
                            break;
                        }

                        foreach (var kvp in priceRange)
                        {
                            foreach (var product in kvp.Value.OrderBy(x => x.Name).ThenBy(x => x.Producer).ThenBy(x => x.Price))
                            {
                                Console.WriteLine("{0};{1};{2:f2}", product.Name, product.Producer, product.Price);
                            }
                        }
                        break;
                    }
            }
        }
    }
}