using System;

public class ScoreboardEntry : IComparable<ScoreboardEntry>
{
    public int CompareTo(ScoreboardEntry other)
    {
        throw new NotImplementedException();
    }

    public int Score { get; set; }

    public string Username { get; set; }
}