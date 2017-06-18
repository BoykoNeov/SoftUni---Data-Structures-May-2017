using System;
using Wintellect.PowerCollections;
namespace TrieRope
{
    public class Launcher
    {
        public static void Main()
        {
            Trie<string> proba = new Trie<string>();
            proba.Insert("bace", "123");
            Console.WriteLine(proba.GetValue("bace"));
            proba.Insert("bace", "653");
            Console.WriteLine(proba.GetValue("bace"));

            BigList<string> probaBigList = new BigList<string>();
            probaBigList.Insert(0, "erdfdgdfdg");
            Console.WriteLine(string.Join("", probaBigList));

            StringEditor stringEditor = new StringEditor();

            stringEditor.Login("pesho");
            //stringEditor.Login("pesho");

            stringEditor.Prepend("pesho", "1234567890");
            stringEditor.Prepend("pesho", "ABCDEFGHIG".ToLower());
            stringEditor.Prepend("pesho", "1234567890");
            stringEditor.Substring("pesho", 1, 2);
            Console.WriteLine(stringEditor.Print("pesho"));

            //stringEditor.Logout("pesho");
            //stringEditor.Undo("pesho");
            //stringEditor.Undo("pesho");
            //stringEditor.Undo("pesho");
            //stringEditor.Undo("pesho");



            //stringEditor.Prepend("pesho", "stringexample");
            //stringEditor.Delete("pesho", 3, 6);
            //stringEditor.Undo("pesho");

            //stringEditor.Substring("pesho", 0, 3);
            //stringEditor.Undo("pesho");


            //Console.WriteLine(stringEditor.Print("pesho"));
        }
    }
}