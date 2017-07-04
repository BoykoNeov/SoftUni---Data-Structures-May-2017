using System;

class Launcher
{
    static void Main(string[] args)
    {
        Enterprise test = new Enterprise();
        test.Add(new Employee("a", "b", 20, Position.Developer, DateTime.Now));
    }
}
