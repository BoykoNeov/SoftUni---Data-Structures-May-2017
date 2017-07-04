using System;

class Launcher
{
    public static void Main()
    {
        //  Developer, Manager, Hr, TeamLead, Owner

        Position developer;
        Position manager;
        Position hr;
        Position teamleader;
        Position owner;


        bool a = (Enum.TryParse("0", true, out developer));
        a = (Enum.TryParse("1", true, out manager));
        a = (Enum.TryParse("2", true, out hr));
        a = (Enum.TryParse("3", true, out teamleader));
        a = (Enum.TryParse("4", true, out owner));





        var test = new Enterprise();


        test.Add(new Employee("Ivan", "Ivanov", 20, developer, DateTime.Now.AddYears(-1)));
        test.Add(new Employee("Ivan", "Ivanov", 25, hr, DateTime.Now.AddYears(-1)));
        test.Add(new Employee("Ivan", "Lalov", 30, teamleader, DateTime.Now.AddYears(-2)));
        test.Add(new Employee("Georgi", "Georgiev", 35, owner, DateTime.Now.AddYears(-3)));
        test.Add(new Employee("Pan", "Georgiev", 40, owner, DateTime.Now.AddYears(-3)));

        var t1 = test.Count;
        var t2 = test.AllWithPositionAndMinSalary(owner, 36);

       


    }
}
