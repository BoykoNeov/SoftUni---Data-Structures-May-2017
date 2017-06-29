using System;

class Product : IComparable<Product>
{
    public string Name { get; set; }
    public string Producer { get; set; }
    public decimal Price { get; set; }

    public Product(string name, string producer, decimal price)
    {
        this.Name = name;
        this.Producer = producer;
        this.Price = price;
    }

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
}