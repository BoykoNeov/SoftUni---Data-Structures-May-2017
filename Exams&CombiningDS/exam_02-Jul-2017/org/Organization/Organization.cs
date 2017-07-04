using System;
using System.Collections;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class Organization : IOrganization
{
    HashSet<Person> allEmployees;
    OrderedDictionary<int, List<Person>> byNameSize;
    Dictionary<string, List<Person>> byName;
    List<Person> byInsertOrder;

    public Organization()
    {
        allEmployees = new HashSet<Person>();
        byNameSize = new OrderedDictionary<int, List<Person>>();
        byName = new Dictionary<string, List<Person>>();
        byInsertOrder = new List<Person>();
    }

    public IEnumerator<Person> GetEnumerator()
    {
        for (int i = 0; i < this.byInsertOrder.Count; i++)
        {
            yield return byInsertOrder[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int Count { get; set; }

    public bool Contains(Person person)
    {
        return allEmployees.Contains(person);
    }

    public bool ContainsByName(string name)
    {
        return this.byName.ContainsKey(name);
    }

    public void Add(Person person)
    {
        this.Count++;
        this.allEmployees.Add(person);
        this.byInsertOrder.Add(person);

        if (!this.byName.ContainsKey(person.Name))
        {
            this.byName.Add(person.Name, new List<Person>());
        }

        this.byName[person.Name].Add(person);

        if (!this.byNameSize.ContainsKey(person.Name.Length))
        {
            this.byNameSize.Add(person.Name.Length, new List<Person>());
        }

        this.byNameSize[person.Name.Length].Add(person);
    }

    public Person GetAtIndex(int index)
    {
        if (index >= this.byInsertOrder.Count)
        {
            throw new IndexOutOfRangeException("Employee index out of range");
        }

        return byInsertOrder[index];
    }

    public IEnumerable<Person> GetByName(string name)
    {
        IEnumerable<Person> res = new List<Person>();
        if (!this.byName.ContainsKey(name))
        {
            return res;
        }
        else
        {
            return this.byName[name];
        }
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        for (int i = 0; i < this.byInsertOrder.Count && i < count; i++)
        {
            yield return this.byInsertOrder[i];
        }
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        foreach (var kvp in this.byNameSize.Range(minLength, true, maxLength, true))
        {
            foreach (Person employee in kvp.Value)
            {
                yield return employee;
            }
        }
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        if (!this.byNameSize.ContainsKey(length))
        {
            throw new ArgumentException("No employees with such name length");
        }

        return this.byNameSize[length];
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        for (int i = 0; i < this.byInsertOrder.Count; i++)
        {
            yield return byInsertOrder[i];
        }
    }
}