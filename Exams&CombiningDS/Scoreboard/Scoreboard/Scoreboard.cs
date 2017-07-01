using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Scoreboard : IScoreboard
{
    public Scoreboard(int maxEntriesToKeep = 10)
    {
        throw new NotImplementedException();
    }

    public bool RegisterUser(string username, string password)
    {
        throw new NotImplementedException();
    }

    public bool RegisterGame(string game, string password)
    {
        throw new NotImplementedException();
    }

    public bool AddScore(string username, string userPassword, string game, string gamePassword, int score)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ScoreboardEntry> ShowScoreboard(string game)
    {
        throw new NotImplementedException();
    }

    public bool DeleteGame(string game, string gamePassword)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> ListGamesByPrefix(string gameNamePrefix)
    {
        throw new NotImplementedException();
    }
}