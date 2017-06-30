namespace Classes
{
    using Interfaces;
    using System;

    public class Minion : IMinion
    {
        public Minion(int XCoordinateInput, int id)
        {
            this.XCoordinate = XCoordinateInput;
            this.Id = id;
            this.Health = 100;
        }

        public int CompareTo(Minion other)
        {
            if (this.XCoordinate != other.XCoordinate)
            {
                return this.XCoordinate.CompareTo(other.XCoordinate);
            }
            else
            {
                return this.Id.CompareTo(other.Id);
            }
        }

        public int Id { get; private set; }

        public int XCoordinate { get; private set; }

        public int Health { get; set; }
    }
}
