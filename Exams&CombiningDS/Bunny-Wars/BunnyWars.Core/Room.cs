using System.Collections.Generic;

namespace BunnyWars.Core
{
   public class Room
    {
       internal int ID { get; set; }
       internal HashSet<Bunny> bunniesInRoom;

        public Room (int id)
        {
            this.ID = id;
            bunniesInRoom = new HashSet<Bunny>();
        }
    }
}