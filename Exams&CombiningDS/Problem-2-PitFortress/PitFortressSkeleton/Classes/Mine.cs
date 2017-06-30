namespace Classes
{
    using Interfaces;

    public class Mine : IMine
    {
        public int CompareTo(Mine other)
        {
            if (this.Delay != other.Delay)
            {
                return this.Delay.CompareTo(other.Delay);
            }
            else
            {
                return this.Id.CompareTo(other.Id);
            }
        }

        public Mine (int id, int delay, int damage, int xCoordinateInput, Player player)
        {
            this.Id = id;
            this.Delay = delay;
            this.Damage = damage;
            this.XCoordinate = xCoordinateInput;
            this.Player = player;
        }

        public int Id { get; private set; }

        public int Delay { get; set; }

        public int Damage { get; private set; }

        public int XCoordinate { get; private set; }

        public Player Player { get; private set; }
    }
}
