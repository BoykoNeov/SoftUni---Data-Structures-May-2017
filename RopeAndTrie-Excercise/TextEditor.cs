using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace TrieRope
{
    class StringEditor : ITextEditor
    {
        public Trie<string> loggedUsers;
        public Dictionary<string, Stack<BigList<char>>> userStrings;

        public StringEditor()
        {
            loggedUsers = new Trie<string>();
            userStrings = new Dictionary<string, Stack<BigList<char>>>();
        }

        public void Login(string username)
        {
            loggedUsers.Insert(username, string.Empty);
            userStrings[username] = new Stack<BigList<char>>();
        }

        public void Logout(string username)
        {
            loggedUsers.Delete(username);
            userStrings.Remove(username);
        }

        public string Print(string username)
        {
            if (!userStrings.ContainsKey(username))
            {
                return null;
            }

            string currentString = string.Empty;
            currentString = string.Join("", userStrings[username].Peek());
            return currentString;
        }

        public void Prepend(string username, string str)
        {
            if (!userStrings.ContainsKey(username))
            {
                return;
            }

            BigList<char> newString = new BigList<char>();

            if (userStrings[username].Count > 0)
            {
                newString = userStrings[username].Peek().Clone();
            }

            newString.AddRangeToFront(str);
            userStrings[username].Push(newString);
        }

        public void Clear(string username)
        {
            if (!userStrings.ContainsKey(username))
            {
                return;
            }

            BigList<char> newString = new BigList<char>();
            userStrings[username].Push(newString);
        }

        public void Delete(string username, int startIndex, int length)
        {
            if (!userStrings.ContainsKey(username) || userStrings[username].Count == 0)
            {
                return;
            }

            try
            {
                BigList<char> newString = new BigList<char>();
                newString = userStrings[username].Peek().Clone();
                newString.RemoveRange(startIndex, length);
                userStrings[username].Push(newString);
            }
            catch
            {
            }

        }

        public void Insert(string username, int index, string str)
        {
            if (!userStrings.ContainsKey(username) || userStrings[username].Count == 0)
            {
                return;
            }

            try
            {
                BigList<char> newString = new BigList<char>();
                newString = userStrings[username].Peek().Clone();
                newString.InsertRange(index, str);
                userStrings[username].Push(newString);
            }
            catch
            {
            }
        }

        public int Length(string username)
        {
            if (!userStrings.ContainsKey(username) || userStrings[username].Count == 0)
            {
                return 0;
            }
            else
            {
                int result = userStrings[username].Peek().Count;
                return result;
            }
        }

        public void Substring(string username, int startIndex, int length)
        {
            if (!userStrings.ContainsKey(username) || userStrings[username].Count == 0)
            {
                return;
            }

            try
            {
                BigList<char> newString = new BigList<char>();
                newString.AddRangeToFront(userStrings[username].Peek().Range(startIndex, length));
                userStrings[username].Push(newString);
            }
            catch
            {
            }
        }

        public void Undo(string username)
        {
            if (!userStrings.ContainsKey(username) || userStrings[username].Count <= 1)
            {
                return;
            }

            userStrings[username].Pop();

        }

        public IEnumerable<string> Users(string prefix = "")
        {
           return loggedUsers.GetByPrefix(prefix);
        }
    }
}