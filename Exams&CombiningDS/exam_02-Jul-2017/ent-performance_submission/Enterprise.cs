using System;
using System.Collections;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class Enterprise : IEnterprise
{
    Dictionary<Guid, Employee> byGuid = new Dictionary<Guid, Employee>();
    OrderedDictionary<double, HashSet<Employee>> bySalary = new OrderedDictionary<double, HashSet<Employee>>();
   // Dictionary<string, HashSet<Employee>> byFirstName = new Dictionary<string, HashSet<Employee>>();
    Dictionary<Position, HashSet<Employee>> byPosition = new Dictionary<Position, HashSet<Employee>>();

    public Enterprise()
    {
        this.byPosition.Add(Position.Developer, new HashSet<Employee>());
        this.byPosition.Add(Position.Hr, new HashSet<Employee>());
        this.byPosition.Add(Position.Manager, new HashSet<Employee>());
        this.byPosition.Add(Position.Owner, new HashSet<Employee>());
        this.byPosition.Add(Position.TeamLead, new HashSet<Employee>());
    }

    public int Count { get => this.byGuid.Count; }

    public void Add(Employee employee)
    {
        this.byGuid.Add(employee.Id, employee);
        this.byPosition[employee.Position].Add(employee);

        //if (!this.byFirstName.ContainsKey(employee.FirstName))
        //{
        //    this.byFirstName.Add(employee.FirstName, new HashSet<Employee>());
        //}

        //this.byFirstName[employee.FirstName].Add(employee);
    }

    private IEnumerable<Employee> Allwithmin(Position position, double minSalary)
    {
        foreach (var employee in this.byPosition[position])
        {
            if (employee.Salary >= minSalary)
            {
                yield return employee;
            }
        }
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        if (!this.byPosition.ContainsKey(position) || this.byPosition[position].Count == 0)
        {
            return new List<Employee>();
        }

         return this.Allwithmin(position, minSalary);
    }

    public bool Change(Guid guid, Employee employee)
    {
        if (!this.byGuid.ContainsKey(guid))
        {
            return false;
        }

        this.byGuid.Remove(guid);
        this.byGuid.Add(employee.Id, employee);
        return true;
    }

    public bool Contains(Guid guid)
    {
        return (this.byGuid.ContainsKey(guid));
    }

    public bool Contains(Employee employee)
    {
        return this.byGuid.ContainsKey(employee.Id);
    }

    public bool Fire(Guid guid)
    {
        if (!this.byGuid.ContainsKey(guid))
        {
            return false;
        }

        this.byGuid.Remove(guid);

        return true;
    }

    public Employee GetByGuid(Guid guid)
    {
        return this.byGuid[guid];
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        return this.byPosition[position];
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        throw new NotImplementedException();
    }


    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        foreach (var emp in this.byPosition[position])
        {
            if (emp.Salary == salary)
            {
                yield return emp;
            }
        }
    }

    public int GetCount()
    {
        return this.byGuid.Count;
    }


    public Position PositionByGuid(Guid guid)
    {
        if (!this.byGuid.ContainsKey(guid))
        {
            throw new InvalidOperationException();
        }

        return this.byGuid[guid].Position;
    }

    public bool RaiseSalary(int months, int percent)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        foreach (var a in this.byGuid.Values)
        {
            if (a.FirstName == firstName)
            {
                yield return a;
            }
        }
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        foreach (var kvp in this.byGuid)
        {
            yield return kvp.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}