using System;

public class Interval
{
    public double Lo { get; set; }
    public double Hi { get; set; }

    public Interval(double lo, double hi)
    {
        ValidateInterval(lo, hi);
        this.Lo = lo;
        this.Hi = hi;
    }

    public bool Intersects(double lo, double hi)
    {
        ValidateInterval(lo, hi);
        return this.Lo < hi && this.Hi > lo;
    }

    public override bool Equals(object obj)
    {
        var other = (Interval)obj;
        return this.Lo == other.Lo && this.Hi == other.Hi;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 7;
            hash = (13 * hash) + this.Lo.GetHashCode();
            hash = (13 * hash) + this.Hi.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(Interval firstInterval, Interval secondInterval)
    {
        return firstInterval.Equals(secondInterval);
    }

    public static bool operator !=(Interval firstInterval, Interval secondInterval)
    {
        return !firstInterval.Equals(secondInterval);
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", this.Lo, this.Hi);
    }

    private static void ValidateInterval(double lo, double hi)
    {
        if (hi < lo)
        {
            throw new ArgumentException();
        }
    }
}