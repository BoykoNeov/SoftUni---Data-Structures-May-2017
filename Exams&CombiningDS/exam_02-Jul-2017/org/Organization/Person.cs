﻿public class Person
{
    public Person(string name, double salary)
    {
        this.Name = name;
        this.Salary = salary;
    }

    public string Name { get; set; }
    public double Salary { get; set; }
    //public int NameLength
    //{
    //    get
    //    {
    //        return this.NameLength;
    //    }
    //    set
    //    {
    //        this.NameLength = this.Name.Length;
    //    }
    //}

    
}
