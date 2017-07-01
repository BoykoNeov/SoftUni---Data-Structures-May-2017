namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        LinkedList<Room> roomsLink;
        Dictionary<int, LinkedListNode<Room>> nodesWithRoomsById;
        OrderedSet<int> roomsById;
        Dictionary<string, Bunny> bunnyNames;
        Dictionary<int, SortedSet<Bunny>> bunniesByTeam;
        Trie<byte> reversedBunnyNames;

        public BunnyWarsStructure()
        {
            roomsLink = new LinkedList<Room>();
            nodesWithRoomsById = new Dictionary<int, LinkedListNode<Room>>();
            roomsById = new OrderedSet<int>();
            bunnyNames = new Dictionary<string, Bunny>();
            bunniesByTeam = new Dictionary<int, SortedSet<Bunny>>();
            reversedBunnyNames = new Trie<byte>();
        }
        

        public int BunnyCount { get; set; }
        public int RoomCount { get; set; }

        public void AddRoom(int roomId)
        {
            if (nodesWithRoomsById.ContainsKey(roomId))
            {
                throw new ArgumentException("Room with this id already exists!");
            }

            //int previousRoomId = roomsById.RangeFrom(0, true).GetLast();
            //LinkedListNode<Room> previousNode;
            //nodesWithRoomsById.TryGetValue(previousRoomId, out previousNode);

            var nextRoomSet = roomsById.RangeTo(roomId, false);


            Room newRoom = new Room(roomId);
            LinkedListNode<Room> roomNode = new LinkedListNode<Room>(newRoom);
            
            if (nextRoomSet.Count == 0)
            {
                roomsLink.AddLast(roomNode);
            }
            else
            {
                int nextRoomId = nextRoomSet.GetFirst();
                LinkedListNode<Room> nextNode;
                nodesWithRoomsById.TryGetValue(nextRoomId, out nextNode);
                roomsLink.AddBefore(nextNode, roomNode);
            }

            nodesWithRoomsById.Add(roomId, roomNode);
            roomsById.Add(roomId);

            RoomCount++;
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if (team > 4 || team < 0)
            {
                throw new IndexOutOfRangeException("Not allowed team number!");
            }

            if (bunnyNames.ContainsKey(name))
            {
                throw new ArgumentException("Bunny with this name already exists!");
            }

            if (!nodesWithRoomsById.ContainsKey(roomId))
            {
                throw new ArgumentException("No room with this ID exists!");
            }

            Bunny newBunny = new Bunny(name, team, roomId);
            this.BunnyCount++;
            bunnyNames.Add(name, newBunny);

            char[] charArray = name.ToCharArray();
            Array.Reverse(charArray);
            reversedBunnyNames.Insert(new string(charArray), 0);
        }

        public void Remove(int roomId)
        {
            if (!this.nodesWithRoomsById.ContainsKey(roomId))
            {
                throw new ArgumentException("No room with such id exists!");
            }
        }

        public void Next(string bunnyName)
        {
            if (!bunnyNames.ContainsKey(bunnyName))
            {
                throw new ArgumentException("Bunny with this name doesn't exist!");
            }

            Bunny currentBunny = bunnyNames[bunnyName];
            LinkedListNode<Room> currentBunnyNode = nodesWithRoomsById[currentBunny.RoomId];
            currentBunnyNode.Value.bunniesInRoom.Remove(currentBunny);

            if (currentBunnyNode.Next == null)
            {
                roomsLink.First.Value.bunniesInRoom.Add(currentBunny);
            }
            else
            {
                currentBunnyNode.Next.Value.bunniesInRoom.Add(currentBunny);
            }
        }

        public void Previous(string bunnyName)
        {
            if (!bunnyNames.ContainsKey(bunnyName))
            {
                throw new ArgumentException("Bunny with this name doesn't exist!");
            }

            Bunny currentBunny = bunnyNames[bunnyName];
            LinkedListNode<Room> currentBunnyNode = nodesWithRoomsById[currentBunny.RoomId];
            currentBunnyNode.Value.bunniesInRoom.Remove(currentBunny);

            if (currentBunnyNode.Previous == null)
            {
                roomsLink.Last.Value.bunniesInRoom.Add(currentBunny);
            }
            else
            {
                currentBunnyNode.Previous.Value.bunniesInRoom.Add(currentBunny);
            }
        }

        public void Detonate(string bunnyName)
        {
            if (!bunnyNames.ContainsKey(bunnyName))
            {
                throw new ArgumentException("Bunny with this name doesn't exist!");
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            var a = reversedBunnyNames.GetByPrefix("suffix");
            foreach (string bunnyName in a)
            {
                yield return bunnyNames[bunnyName];
            }
        }
    }
}