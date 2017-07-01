namespace BunnyWars.Core
{
    using System;

    public class Bunny : IComparable<Bunny>
    {
        public Bunny(string name, int team, int roomId)
        {
            this.Health = 100;
            this.Score = 0;
            this.Name = name;
            this.Team = team;
            this.RoomId = roomId;
        }

        public int RoomId { get; set; }

        public string Name { get; private set; }

        public int Health { get; set; }

        public int Score { get; set; }

        public int Team { get; private set; }

        public int CompareTo(Bunny other)
        {
                return this.Name.CompareTo(other.Name);
        }
    }
}