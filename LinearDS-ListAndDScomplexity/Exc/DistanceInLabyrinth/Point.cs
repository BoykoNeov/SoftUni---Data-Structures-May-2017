/// <summary>
/// class for holding point coordinates
/// </summary>
public class Point
{
    public int Y { get; private set; }
    public int X { get; private set; }
    public int PreviousPointDistance { get; private set; }

   public Point(int y, int x, int previousDistance)
    {
        this.Y = y;
        this.X = x;
        this.PreviousPointDistance = previousDistance;
    }

    public override bool Equals(object obj)
    {
        var other = obj as Point;
        if (other == null)
        {
            return false;
        }
        return this.X == other.X && this.Y == other.Y;
    }

    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;
            // Suitable nullity checks etc, of course :)
            hash = hash * 23 + Y.GetHashCode();
            hash = hash * 23 + X.GetHashCode();
            return hash;
        }
    }
}