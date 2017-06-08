using System;

public class Node : IComparable<Node>
{
    public Node(int row, int col)
    {
        this.Row = row;
        this.Col = col;
    }

    public int Row { get; internal set; }
    public int Col { get; internal set; }
    public int F { get; internal set; }

    public int CompareTo(Node other)
    {
        return this.F.CompareTo(other.F);
    }

    public override bool Equals(object obj)
    {
        var other = (Node)obj;
        return this.Col == other.Col && this.Row == other.Row;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = 31 * (hash + this.Row.GetHashCode());
            hash = 31 * (hash + this.Col.GetHashCode());
            return hash;
        }

    }

    public override string ToString()
    {
        return this.Row + " " + this.Col;
    }
}