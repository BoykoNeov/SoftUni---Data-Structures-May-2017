using System;
using System.Collections.Generic;
using System.Linq;

public class PersonCollection : IPersonCollection
{
    // TODO: define the underlying data structures here ...
    Dictionary<string, Person> personsByEmail;
    SortedDictionary<string, Person> personsSortedByEmail;
    Dictionary<string, SortedDictionary<string, Person>> personsByDomainSortedByEmail;
    Dictionary<string, SortedDictionary<string, Person>> personsByNameAndTownSortedByEmail;
    SortedDictionary<int, SortedDictionary<string, Person>> personsByAgeSortedByAgeThenEmail;
    SortedDictionary<int, Dictionary<string, SortedDictionary<string, Person>>> personsByAgeAndTown;

    public PersonCollection()
    {
        personsByEmail = new Dictionary<string, Person>();
        personsByDomainSortedByEmail = new Dictionary<string, SortedDictionary<string, Person>>();
        personsByNameAndTownSortedByEmail = new Dictionary<string, SortedDictionary<string, Person>>();
        personsByAgeSortedByAgeThenEmail = new SortedDictionary<int, SortedDictionary<string, Person>>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.personsByEmail.ContainsKey(email))
        {
            return false;
        }

        Person newPerson = new Person(email, name, age, town);

        // Adds person to the main dictionary
        this.personsByEmail.Add(email, newPerson);

        // Adds person to the domain dictionary
        string personDomain = email.Split('@')[1];
        if (!this.personsByDomainSortedByEmail.ContainsKey(personDomain))
        {
            this.personsByDomainSortedByEmail.Add(personDomain, new SortedDictionary<string, Person>());
        }
        this.personsByDomainSortedByEmail[personDomain].Add(email, newPerson);

        // Adds person to the name+town dictionary
        string namePlusTown = string.Concat(name, town);
        if (!this.personsByNameAndTownSortedByEmail.ContainsKey(namePlusTown))
        {
            this.personsByNameAndTownSortedByEmail.Add(namePlusTown, new SortedDictionary<string, Person>());
        }
        this.personsByNameAndTownSortedByEmail[namePlusTown].Add(email, newPerson);

        // Adds person to the age dictionary
        if (!this.personsByAgeSortedByAgeThenEmail.ContainsKey(age))
        {
            this.personsByAgeSortedByAgeThenEmail.Add(age, new SortedDictionary<string, Person>());
        }
        this.personsByAgeSortedByAgeThenEmail[age].Add(email, newPerson);

        return true;
    }

    public int Count
    {
        get
        {
            return this.personsByEmail.Count;
        }
    }

    public Person FindPerson(string email)
    {
        if (!this.personsByEmail.ContainsKey(email))
        {
            return null;
        }
        else
        {
            return this.personsByEmail[email];
        }
    }

    public bool DeletePerson(string email)
    {
        if (!this.personsByEmail.ContainsKey(email))
        {
            return false;
        }
        else
        {
            string name = this.personsByEmail[email].Name;
            string town = this.personsByEmail[email].Town;
            int age = this.personsByEmail[email].Age;

            // Deletes person from the name + town dictionary
            string namePlusTown = string.Concat(name, town);
            this.personsByNameAndTownSortedByEmail[namePlusTown].Remove(email);

            // Deletes person from the main dictionary
            this.personsByEmail.Remove(email);

            // Deletes person from the domain dictionary
            string personDomain = email.Split('@')[1];
            this.personsByDomainSortedByEmail[personDomain].Remove(email);

            // Deletes person from the age dictionary
            this.personsByAgeSortedByAgeThenEmail[age].Remove(email);

            return true;
        }
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        if (this.personsByDomainSortedByEmail.ContainsKey(emailDomain))
        {
            foreach (KeyValuePair<string, Person> kvp in this.personsByDomainSortedByEmail[emailDomain])
            {
                yield return kvp.Value;
            }
        }
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        string namePlusTown = string.Concat(name, town);

        if (this.personsByNameAndTownSortedByEmail.ContainsKey(namePlusTown))
        {
            foreach (KeyValuePair<string, Person> kvp in this.personsByNameAndTownSortedByEmail[namePlusTown])
            {
                yield return kvp.Value;
            }
        }
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        foreach (var ages in this.personsByAgeSortedByAgeThenEmail)
        {
            if (ages.Key >= startAge && ages.Key <= endAge)
            {
                foreach (KeyValuePair<string, Person> kvp in ages.Value)
                {
                    yield return kvp.Value;
                }
            }
        }
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        foreach (var ages in this.personsByAgeSortedByAgeThenEmail)
        {
            if (ages.Key >= startAge && ages.Key <= endAge)
            {
                foreach (KeyValuePair<string, Person> kvp in ages.Value)
                {
                    if (kvp.Value.Town == town)
                    {
                        yield return kvp.Value;
                    }
                }
            }
        }
    }
}