using System;

/// <summary>
/// Class for holding product information for ProductList
/// </summary>
public class Product : IComparable<Product>
{
    public string Name { get; set; }
    public string Producer { get; set; }
    public decimal Price { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">Name of product</param>
    /// <param name="producer">Name of producer</param>
    /// <param name="price">Product price</param>
    public Product(string name, string producer, decimal price)
    {
        this.Name = name;
        this.Producer = producer;
        this.Price = price;
    }

    /// <summary>
    /// Products are compared first by name, then by producer and lastly by price
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Product other)
    {
        if (this.Name != other.Name)
        {
            return this.Name.CompareTo(other.Name);
        }
        else if (this.Producer != other.Producer)
        {
            return this.Producer.CompareTo(other.Producer);
        }
        else
        {
            return this.Price.CompareTo(other.Price);
        }
    }

    /// <summary>
    /// Overrides ToString() to ease output for the task "Shopping center"
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return "{" + string.Format($"{this.Name};{this.Producer};{this.Price:f2}") + "}";
    }
}