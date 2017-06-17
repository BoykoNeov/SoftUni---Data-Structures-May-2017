using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace TrieRope
{
    class StringEditor : ITextEditor
    {

         
        public StringEditor()
        {

        }

        public void Login(string username)
        {
            throw new NotImplementedException();
        }

        public void Logout(string username)
        {
            throw new NotImplementedException();
        }

        public string Print(string username)
        {
            throw new NotImplementedException();
        }

        public void Prepend(string username, string str)
        {
            throw new NotImplementedException();
        }

        public void Clear(string username)
        {
            throw new NotImplementedException();
        }

        public void Delete(string username, int startIndex, int length)
        {
            throw new NotImplementedException();
        }

        public void Insert(string username, int index, string str)
        {
            throw new NotImplementedException();
        }

        public int Length(string username)
        {
            throw new NotImplementedException();
        }

        public void Substring(string username, int startIndex, int length)
        {
            throw new NotImplementedException();
        }

        public void Undo(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> Users(string prefix = "")
        {
            throw new NotImplementedException();
        }
    }
}