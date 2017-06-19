using System;
using System.Linq;
using Wintellect.PowerCollections;

public class Launcher
{
    public static void Main()
    {
        string command = string.Empty;
        StringEditor textEditor = new StringEditor();

        while ((command = Console.ReadLine()) != "end")
        {
            string[] inputs = command.Split();

            try
            {
                if (inputs[0] == "login")
                {
                    textEditor.Login(inputs[1]);
                    continue;
                }

                if (inputs[0] == "users")
                {
                    if (inputs.Length > 1)
                    {
                        foreach (string user in textEditor.Users(inputs[1]))
                        {
                            Console.WriteLine(user);
                        }
                    }
                    else
                    {
                        foreach (string user in textEditor.Users())
                        {
                            Console.WriteLine(user);
                        }
                    }

                    continue;
                }

                if (inputs[0] == "logout")
                {
                    textEditor.Logout(inputs[1]);
                    continue;
                }

                if (inputs[1] == "prepend")
                {
                    string[] stringToPrepend = command.Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Take(1).ToArray();
                    textEditor.Prepend(inputs[0], stringToPrepend[0]);
                    continue;
                }

                if (inputs[1] == "print" && textEditor.userStrings.ContainsKey(inputs[0]))
                {
                    Console.WriteLine(textEditor.Print(inputs[0]));
                    continue;
                }

                if (inputs[1] == "delete" && textEditor.userStrings.ContainsKey(inputs[0]))
                {
                    textEditor.Delete(inputs[0], int.Parse(inputs[2]), int.Parse(inputs[3]));
                    continue;
                }

                if (inputs[1] == "delete" && textEditor.userStrings.ContainsKey(inputs[0]))
                {
                    textEditor.Delete(inputs[0], int.Parse(inputs[2]), int.Parse(inputs[3]));
                    continue;
                }

                if (inputs[1] == "insert" && textEditor.userStrings.ContainsKey(inputs[0]))
                {
                    string[] stringToInsert = command.Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Take(1).ToArray();
                    textEditor.Insert(inputs[0], int.Parse(inputs[2]), stringToInsert[0]);
                    continue;
                }

                if (inputs[1] == "length" && textEditor.userStrings.ContainsKey(inputs[0]))
                {
                    Console.WriteLine(textEditor.Length(inputs[0]));
                    continue;
                }

                if (inputs[1] == "clear" && textEditor.userStrings.ContainsKey(inputs[0]))
                {
                    textEditor.Clear(inputs[0]);
                    continue;
                }

                if (inputs[1] == "substring" && textEditor.userStrings.ContainsKey(inputs[0]))
                {
                    textEditor.Substring(inputs[0], int.Parse(inputs[2]), int.Parse(inputs[3]));
                    continue;
                }
            }
            catch
            {

            }
            }
    }
    }