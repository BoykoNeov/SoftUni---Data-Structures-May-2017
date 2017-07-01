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

            if (!bunniesByTeam.ContainsKey(team))
            {
                bunniesByTeam.Add(team, new SortedSet<Bunny>());
            }
            bunniesByTeam[team].Add(newBunny);

            nodesWithRoomsById[roomId].Value.bunniesInRoom.Add(newBunny);
        }

        public void Remove(int roomId)
        {
            if (!this.nodesWithRoomsById.ContainsKey(roomId))
            {
                throw new ArgumentException("No room with such id exists!");
            }

            LinkedListNode<Room> NodeWithRoomToDelete = nodesWithRoomsById[roomId];

            List<Bunny> bunniesToRemove = new List<Bunny>();
            foreach (Bunny bunny in NodeWithRoomToDelete.Value.bunniesInRoom)
            {
                bunniesToRemove.Add(bunny);
            }

            foreach (Bunny bunny in bunniesToRemove)
            {
                this.RemoveBunny(bunny);
            }


            roomsLink.Remove(NodeWithRoomToDelete);
            nodesWithRoomsById.Remove(roomId);
            roomsById.Remove(roomId);
            this.RoomCount--;


        }

        private void RemoveBunny(Bunny bunnyToRemove)
        {
            this.nodesWithRoomsById[bunnyToRemove.RoomId].Value.bunniesInRoom.Remove(bunnyToRemove);
            bunnyNames.Remove(bunnyToRemove.Name);
            bunniesByTeam[bunnyToRemove.Team].Remove(bunnyToRemove);

            char[] charArray = bunnyToRemove.Name.ToCharArray();
            Array.Reverse(charArray);
            reversedBunnyNames.Delete(new string(charArray));
            this.BunnyCount--;
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
                currentBunny.RoomId = roomsLink.First.Value.ID;
            }
            else
            {
                currentBunnyNode.Next.Value.bunniesInRoom.Add(currentBunny);
                currentBunny.RoomId = currentBunnyNode.Next.Value.ID;
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
                currentBunny.RoomId = roomsLink.Last.Value.ID;
            }
            else
            {
                currentBunnyNode.Previous.Value.bunniesInRoom.Add(currentBunny);
                currentBunny.RoomId = currentBunnyNode.Previous.Value.ID;
            }
        }

        public void Detonate(string bunnyName)
        {
            if (!bunnyNames.ContainsKey(bunnyName))
            {
                throw new ArgumentException("Bunny with this name doesn't exist!");
            }

            Bunny detonatingBunny = bunnyNames[bunnyName];

            List<Bunny> deadBunnies = new List<Bunny>();

            foreach (Bunny sufferingBunny in nodesWithRoomsById[detonatingBunny.RoomId].Value.bunniesInRoom)
            {
                if (sufferingBunny == detonatingBunny || sufferingBunny.Team == detonatingBunny.Team)
                {
                    continue;
                }

                sufferingBunny.Health -= 30;

                if (sufferingBunny.Health <= 0)
                {
                    deadBunnies.Add(sufferingBunny);
                }
            }

            foreach (Bunny deadBunny in deadBunnies)
            {
                detonatingBunny.Score++;
                this.RemoveBunny(deadBunny);
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            if ((team < 0) || (team > 4))
            {
                throw new IndexOutOfRangeException();
            }

            if (!bunniesByTeam.ContainsKey(team))
            {
                return null;
            }

            return bunniesByTeam[team];
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
           // throw new NotImplementedException();
            char[] charArray = suffix.ToCharArray();
            Array.Reverse(charArray);

            var bunniesBySuffix = reversedBunnyNames.GetByPrefix(new string(charArray));

            SortBunniesByReverseNameAndNameLength sortReverse = new SortBunniesByReverseNameAndNameLength();
            SortedSet<Bunny> sortedBunnies = new SortedSet<Bunny>(sortReverse);

            foreach (string bunnyName in bunniesBySuffix)
            {
                char[] charArray2 = bunnyName.ToCharArray();
                Array.Reverse(charArray2);
                string originalKey = new string(charArray2);
                sortedBunnies.Add(bunnyNames[originalKey]);
            }

            return sortedBunnies;
        }
    }
}