using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

/// <summary>
/// Class holding all collections for Shopping Center task
/// </summary>
class ProductList
{
    private Dictionary<string, OrderedBag<Product>> byName;
    private Dictionary<string, Bag<Product>> byNameAndProducer;
    private Dictionary<string, OrderedBag<Product>> byProducer;
    private OrderedDictionary<decimal, Bag<Product>> byPrice;

    public ProductList()
    {
        this.byName = new Dictionary<string, OrderedBag<Product>>();
        this.byNameAndProducer = new Dictionary<string, Bag<Product>>();
        this.byProducer = new Dictionary<string, OrderedBag<Product>>();
        this.byPrice = new OrderedDictionary<decimal, Bag<Product>>();
    }

    /// <summary>
    /// Adds product to the shoping list
    /// </summary>
    /// <param name="name">product name</param>
    /// <param name="price">product price</param>
    /// <param name="producer">product producer</param>
    public void Add(string name, decimal price, string producer)
    {
        Product product = new Product(name, producer, price);

        this.byName.AppendValueToKey(name, product);
        this.byNameAndProducer.AppendValueToKey(name + producer, product);
        this.byProducer.AppendValueToKey(producer, product);
        this.byPrice.AppendValueToKey(price, product);
    }

    /// <summary>
    /// Deletes product from the collection by producer
    /// </summary>
    /// <param name="producer">product producer</param>
    /// <returns>number of deleted items</returns>
    public int DeleteByProducer(string producer)
    {
        if (!this.byProducer.ContainsKey(producer))
        {
            return 0;
        }

        OrderedBag<Product> productsToDelete = this.byProducer[producer];
        this.byProducer.Remove(producer);

        foreach (Product product in productsToDelete)
        {
            this.byName[product.Name].Remove(product);
            if (byName[product.Name].Count == 0)
            {
                byName.Remove(product.Name);
            }

            this.byNameAndProducer[product.Name + product.Producer].Remove(product);
            if (byNameAndProducer[product.Name + product.Producer].Count == 0)
            {
                byNameAndProducer.Remove(product.Name + product.Producer);
            }

            this.byPrice[product.Price].Remove(product);
            if (byPrice[product.Price].Count == 0)
            {
                byPrice.Remove(product.Price);
            }
        }

        return productsToDelete.Count;
    }

    /// <summary>
    /// Deletes a product from the collection by product name and product producer
    /// </summary>
    /// <param name="name">product name</param>
    /// <param name="producer">product producer</param>
    /// <returns>number of deleted items</returns>
    public int DeleteByNameAndProducer(string name, string producer)
    {
        string key = name + producer;
        if (!this.byNameAndProducer.ContainsKey(key))
        {
            return 0;
        }

        Bag<Product> productsToDelete = this.byNameAndProducer[key];
        this.byNameAndProducer.Remove(key);

        foreach (Product product in productsToDelete)
        {
            this.byName[product.Name].Remove(product);
            if (byName[product.Name].Count == 0)
            {
                byName.Remove(product.Name);
            }

            this.byProducer[product.Producer].Remove(product);
            if (byProducer[product.Producer].Count == 0)
            {
                byProducer.Remove(product.Producer);
            }

            this.byPrice[product.Price].Remove(product);
            if (byPrice[product.Price].Count == 0)
            {
                byPrice.Remove(product.Price);
            }
        }

        return productsToDelete.Count;
    }

    /// <summary>
    /// Finds a product by name
    /// </summary>
    /// <param name="name">product name</param>
    /// <returns>OrderedBag with results or null if no results</returns>
    public OrderedBag<Product> FindByName (string name)
    {
        if (!this.byName.ContainsKey(name))
        {
            return null;
        }

        return this.byName[name];
    }

    /// <summary>
    /// Finds a product and inputs it to the console by producer
    /// </summary>
    /// <param name="producer">product producer</param>
    /// <returns>OrderedBag with results or null if no results</returns>
    public OrderedBag<Product> FindByProducer(string producer)
    {
        if (!this.byProducer.ContainsKey(producer))
        {
            return null;
        }

        return this.byProducer[producer];
    }

    /// <summary>
    /// Finds a product by price range
    /// </summary>
    /// <param name="lowerBoundary">lower price (inclusive)</param>
    /// <param name="upperBoundary">higher price (inclusive)</param>
    /// <returns>OrderedBag with results or null if no results</returns>
    public OrderedBag<Product> FindByPriceRange(decimal lowerBoundary, decimal upperBoundary)
    {
        var resultsRange = this.byPrice.Range(lowerBoundary, true, upperBoundary, true);

        if (resultsRange.Count == 0)
        {
            return null;
        }

        OrderedBag<Product> resultsByPrice = new OrderedBag<Product>();
        foreach (var kvp in resultsRange)
        {
            resultsByPrice.AddMany(kvp.Value);
        }

        return resultsByPrice;
    }
}