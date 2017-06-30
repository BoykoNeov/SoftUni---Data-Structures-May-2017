namespace Classes
{
    using System;
    using Interfaces;

    public class Player : IPlayer
    {
        public Player (string name, int mineRadius)
        {
            this.Score = 0;
            this.Name = name;
            this.Radius = mineRadius;
        }

        public int CompareTo(Player other)
        {
            if (this.Score != other.Score)
            {
                return this.Score.CompareTo(other.Score);
            }
            else
            {
                return this.Name.CompareTo(other.Name);
            }
        }

        public string Name { get; private set; }

        public int Radius { get; private set; }

        public int Score { get; set; }
    }
}