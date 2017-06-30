using System;
using System.Collections.Generic;
using Classes;
using Interfaces;
using Wintellect.PowerCollections;

public class PitFortressCollection : IPitFortress
{
    public Dictionary<string, Player> playersByName;
    public OrderedSet<Player> playersSet;

    public OrderedDictionary<int, OrderedBag<Minion>> minionsByX;
    public OrderedSet<Minion> minionsSet;

    public OrderedSet<Mine> minesSet;

    public PitFortressCollection()
    {
        playersByName = new Dictionary<string, Player>();
        playersSet = new OrderedSet<Player>();

        minionsByX = new OrderedDictionary<int, OrderedBag<Minion>>();
        minionsSet = new OrderedSet<Minion>();

        minesSet = new OrderedSet<Mine>();

        PlayersCount = 0;
        MinionsCount = 0;
        MinesCount = 0;
    }

    public int PlayersCount { get; private set; }

    public int MinionsCount { get; private set; }

    public int MinesCount { get; private set; }

    public void AddPlayer(string name, int mineRadius)
    {

        if (mineRadius < 0)
        {
            throw new ArgumentException("Player radius is negative");
        }

        Player newPlayer = new Player(name, mineRadius);

        if (this.playersByName.ContainsKey(newPlayer.Name))
        {
            throw new ArgumentException("Player with this name already exists");
        }

        this.PlayersCount++;
        this.playersByName.Add(name, newPlayer);
        this.playersSet.Add(newPlayer);
    }

    public void AddMinion(int xCoordinate)
    {
        if (xCoordinate < 0 || xCoordinate > 1_000_000)
        {
            throw new ArgumentException("Minion XCoordinate is outside of range");
        }

        this.MinionsCount++;
        Minion newMinion = new Minion(xCoordinate, this.MinionsCount);

        if (!this.minionsByX.ContainsKey(xCoordinate))
        {
            this.minionsByX.Add(xCoordinate, new OrderedBag<Minion>());
        }

        this.minionsByX[newMinion.XCoordinate].Add(newMinion);
        this.minionsSet.Add(newMinion);
    }

    public void SetMine(string playerName, int xCoordinate, int delay, int damage)
    {
        if (!this.playersByName.ContainsKey(playerName))
        {
            throw new ArgumentException("Player with this mine associated name doesn't exist!");
        }

        if (xCoordinate < 0 || xCoordinate > 1_000_000)
        {
            throw new ArgumentException("X coordinate is outside allowed range");
        }

        if (delay < 1 || delay > 10_000)
        {
            throw new ArgumentException("Mine delay is outside allowed range!");
        }

        if (damage < 0 || damage > 100)
        {
            throw new ArgumentException("Mine damage is outside allowed range!");
        }

        this.MinesCount++;
        Mine newMine = new Mine(this.MinesCount, delay, damage, xCoordinate, this.playersByName[playerName]);
        minesSet.Add(newMine);
    }

    public IEnumerable<Minion> ReportMinions()
    {
        foreach (Minion minion in this.minionsSet)
        {
            yield return minion;
        }
    }

    public IEnumerable<Player> Min3PlayersByScore()
    {
        if (this.playersSet.Count < 3)
        {
            throw new ArgumentException("There are less than 3 players in the game");
        }

        List<Player> results = new List<Player>();
        results.Add(playersSet[0]);
        results.Add(playersSet[1]);
        results.Add(playersSet[2]);

        return results;
    }

    public IEnumerable<Player> Top3PlayersByScore()
    {
        if (this.playersSet.Count < 3)
        {
            throw new ArgumentException("There are less than 3 players in the game");
        }

        List<Player> results = new List<Player>();
        results.Add(playersSet[playersSet.Count - 1]);
        results.Add(playersSet[playersSet.Count - 2]);
        results.Add(playersSet[playersSet.Count - 3]);

        return results;
    }

    public IEnumerable<Mine> GetMines()
    {
        foreach (Mine mine in minesSet)
        {
            yield return mine;
        }
    }

    public void PlayTurn()
    {
        OrderedSet<Mine> explodingMines = new OrderedSet<Mine>();
        OrderedSet<Mine> nonExplodingMines = new OrderedSet<Mine>();

       foreach (Mine mine in this.minesSet)
        {
            mine.Delay--;

            if (mine.Delay == 0)
            {
                explodingMines.Add(mine);
            }
            else
            {
                nonExplodingMines.Add(mine);
            }
        }

       foreach (Mine exMine in explodingMines)
        {
            int explosionLowRange = exMine.XCoordinate - exMine.Player.Radius;
            int explosionHighRange = exMine.XCoordinate + exMine.Player.Radius;

            var affectedMinions = minionsByX.Range(explosionLowRange, true, explosionHighRange, true);

            List<Minion> minionsToRemove = new List<Minion>();

            foreach (KeyValuePair<int, OrderedBag<Minion>> kvp in affectedMinions)
            {
                foreach (Minion minion in kvp.Value)
                {
                    minion.Health -= exMine.Damage;

                    if (minion.Health <= 0)
                    {
                        this.playersSet.Remove(exMine.Player);
                        exMine.Player.Score++;
                        this.playersSet.Add(exMine.Player);
                        minionsToRemove.Add(minion);
                    }
                }
            }

            foreach (Minion minion in minionsToRemove)
            {
                this.minionsSet.Remove(minion);
                this.minionsByX[minion.XCoordinate].Remove(minion);
            }
        }


        minesSet = nonExplodingMines;
    }
}
