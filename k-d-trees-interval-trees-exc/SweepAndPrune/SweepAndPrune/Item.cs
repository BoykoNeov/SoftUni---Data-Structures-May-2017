using System;

public class Item : IComparable
{
    internal int X1 { get; set; }
    internal int Y1 { get; set; }
    int X2 { get { return X1 + 10; } }
    int Y2 { get { return Y1 + 10; } }
    string Name { get; set; }

    public Item(string name, int x1, int y1)
    {
        this.Name = name;
        this.X1 = x1;
        this.Y1 = y1;
    }

    public int CompareTo(object obj)
    {
        Item that = obj as Item;
        return this.X1.CompareTo(that.X1);
    }

    public static bool operator >(Item first, Item second)
    {
        if (first.X1 > second.X1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator <(Item first, Item second)
    {
        if (first.X1 < second.X1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator >=(Item first, Item second)
    {
        if (first.X1 >= second.X1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator <=(Item first, Item second)
    {
        if (first.X1 <= second.X1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool Equals(object obj)
    {
        Item that = obj as Item;
        if (that == null)
        {
            return false;
        }

        if (this.X1 == that.X1 &&
            this.X2 == that.X2 &&
            this.Y1 == that.Y1 &&
            this.Y2 == that.Y2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        int hash = 0;

        unchecked
        {
            hash = this.X1.GetHashCode();
            hash = hash * 17 + this.X2.GetHashCode();
            hash = hash * 17 + this.Y1.GetHashCode();
            hash = hash * 17 + this.Y2.GetHashCode();
        }

        return hash;
    }

    public bool Intersects(Item other)
    {
        bool doesIntersect = true;

        if (this.X1 >= other.X2)
        {
            doesIntersect = false;
        }

        if (this.X2 <= other.X1)
        {
            doesIntersect = false;
        }

        if (this.Y1 >= other.Y2)
        {
            doesIntersect = false;
        }

        if (this.Y2 <= other.Y1)
        {
            doesIntersect = false;
        }

        return doesIntersect;
    }
}