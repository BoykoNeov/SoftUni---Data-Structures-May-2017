namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;

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
            return other.Name.CompareTo(this.Name);
        }
    }

    public class SortBunniesByReverseNameAndNameLength : IComparer<Bunny>
    {
        public int Compare(Bunny x, Bunny y)
        {
            for (int i = 0; i < x.Name.Length && i < y.Name.Length; i++)
            {
                int a;

                if ((int)x.Name[x.Name.Length - 1 - i] == (int)y.Name[y.Name.Length - 1 - i])
                {
                    continue;
                }
                else
                {
                    a = ((int)x.Name[x.Name.Length - 1 - i]).CompareTo((int)y.Name[y.Name.Length - 1 - i]);
                    return a;
                }
            }
            return x.Name.Length.CompareTo(y.Name.Length);
        }
    }
}