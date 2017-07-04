using System;
using System.Collections;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class Enterprise : IEnterprise
{
    Dictionary<Guid, Employee> byGuid;
    OrderedDictionary<double, Dictionary<Position, HashSet<Employee>>> bySalaryAndPosition;
    Dictionary<Position, HashSet<Employee>> byPosition;
    Dictionary<string, HashSet<Employee>> byFirstName;
    Dictionary<int, HashSet<Employee>> byMonthsSinceHire;
    Dictionary<string, HashSet<Employee>> byFirstLastNamePosition;

    public Enterprise()
    {
        byGuid = new Dictionary<Guid, Employee>();
        bySalaryAndPosition = new OrderedDictionary<double, Dictionary<Position, HashSet<Employee>>>();
        byFirstName = new Dictionary<string, HashSet<Employee>>();

        byPosition = new Dictionary<Position, HashSet<Employee>>();
        byMonthsSinceHire = new Dictionary<int, HashSet<Employee>>();
        byFirstLastNamePosition = new Dictionary<string, HashSet<Employee>>();
    }

    public int Count { get => this.byGuid.Count; }

    public void Add(Employee employee)
    {
        if (!byFirstLastNamePosition.ContainsKey(employee.FirstName + employee.LastName + employee.Position.ToString()))
        {
            byFirstLastNamePosition.Add(employee.FirstName + employee.LastName + employee.Position.ToString(), new HashSet<Employee>());
        }
        this.byFirstLastNamePosition[employee.FirstName + employee.LastName + employee.Position.ToString()].Add(employee);

        int monthsSinceHire = Math.Abs((employee.HireDate.Year - 2017) * 12) + employee.HireDate.Month - 7;
        if (!this.byMonthsSinceHire.ContainsKey(monthsSinceHire))
        {
            this.byMonthsSinceHire.Add(monthsSinceHire, new HashSet<Employee>());
        }
        this.byMonthsSinceHire[monthsSinceHire].Add(employee);

        if (!this.byFirstName.ContainsKey(employee.FirstName))
        {
            this.byFirstName.Add(employee.FirstName, new HashSet<Employee>());
        }
        this.byFirstName[employee.FirstName].Add(employee);

        if (!this.byPosition.ContainsKey(employee.Position))
        {
            this.byPosition.Add(employee.Position, new HashSet<Employee>());
        }
        this.byPosition[employee.Position].Add(employee);


        this.byGuid.Add(employee.Id, employee);

        if (!this.bySalaryAndPosition.ContainsKey(employee.Salary))
        {
            this.bySalaryAndPosition.Add(employee.Salary, new Dictionary<Position, HashSet<Employee>>());
        }

        if (!this.bySalaryAndPosition[employee.Salary].ContainsKey(employee.Position))
        {
            this.bySalaryAndPosition[employee.Salary].Add(employee.Position, new HashSet<Employee>());
        }

        this.bySalaryAndPosition[employee.Salary][employee.Position].Add(employee);
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        foreach (var kvp in this.bySalaryAndPosition.RangeFrom(minSalary, true))
        {
            foreach (var kvp2 in kvp.Value)
            {
                if (kvp2.Key == position)
                {
                    foreach (var emp in kvp2.Value)
                    {
                        yield return emp;
                    }
                }
            }
        }
    }

    public bool Change(Guid guid, Employee employee)
    {
        if (this.byGuid.ContainsKey(guid))
        {
            Employee currentEmployee = this.byGuid[guid];

            //Remove employee
            this.Fire(guid);

            this.Add(employee);

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Contains(Guid guid)
    {
        return this.byGuid.ContainsKey(guid);
    }

    public bool Contains(Employee employee)
    {
        return this.byGuid.ContainsKey(employee.Id);
    }

    public bool Fire(Guid guid)
    {
        if (!byGuid.ContainsKey(guid))
        {
            return false;
        }

        Employee currentEmployee = byGuid[guid];

        this.byGuid.Remove(guid);
        this.bySalaryAndPosition[currentEmployee.Salary][currentEmployee.Position].Remove(currentEmployee);
        this.byFirstName[currentEmployee.FirstName].Remove(currentEmployee);
        this.byPosition[currentEmployee.Position].Remove(currentEmployee);

        int monthsSinceHire = Math.Abs((currentEmployee.HireDate.Year - 2017) * 12) + currentEmployee.HireDate.Month - 7;
        this.byMonthsSinceHire[monthsSinceHire].Remove(currentEmployee);

        return true;
    }

    public Employee GetByGuid(Guid guid)
    {
        if (!this.byGuid.ContainsKey(guid))
        {
            throw new ArgumentException();
        }
        return this.byGuid[guid];
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        if (!this.byPosition.ContainsKey(position))
        {
            throw new InvalidOperationException();
        }
        return this.byPosition[position];
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        List<Employee> results = new List<Employee>();

        foreach (var kvp in this.bySalaryAndPosition.RangeFrom(minSalary, true))
        {
            foreach (var kvp2 in kvp.Value)
            {
                results.AddRange(kvp2.Value);
            }
        }

        if (results.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return results;
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        if (!this.bySalaryAndPosition.ContainsKey(salary) ||
            !this.bySalaryAndPosition[salary].ContainsKey(position) ||
            this.bySalaryAndPosition[salary][position].Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.bySalaryAndPosition[salary][position];
    }

    public Position PositionByGuid(Guid guid)
    {
        return this.byGuid[guid].Position;
    }

    public bool RaiseSalary(int months, int percent)
    {
        if (!this.byMonthsSinceHire.ContainsKey(months))
        {
            return false;
        }

        List<Employee> emploiesToRaise = new List<Employee>();

        foreach (var kvp in this.byMonthsSinceHire)
        {
            foreach (var employee in kvp.Value)
            {
                emploiesToRaise.Add(employee);
            }
        }

        foreach (Employee employee in emploiesToRaise)
        {
            this.bySalaryAndPosition[employee.Salary][employee.Position].Remove(employee);
            employee.Salary *= 1.3;

            if (!this.bySalaryAndPosition.ContainsKey(employee.Salary))
            {
                this.bySalaryAndPosition.Add(employee.Salary, new Dictionary<Position, HashSet<Employee>>());
            }

            if (!this.bySalaryAndPosition[employee.Salary].ContainsKey(employee.Position))
            {
                this.bySalaryAndPosition[employee.Salary].Add(employee.Position, new HashSet<Employee>());
            }

            this.bySalaryAndPosition[employee.Salary][employee.Position].Add(employee);
        }

        return true;
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        if (!this.byFirstName.ContainsKey(firstName) || this.byFirstName[firstName].Count == 0)
        {
            return new LinkedList<Employee>();
        }

        return this.byFirstName[firstName];
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        string key = firstName + lastName + position.ToString();
        if (!this.byFirstLastNamePosition.ContainsKey(key))
        {
            return new List<Employee>();
        }
        else
        {
            return this.byFirstLastNamePosition[key];
        }
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        List<Employee> results = new List<Employee>();

        foreach (Position position in positions)
        {
            results.AddRange(this.byPosition[position]);
        }

        return results;
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        var salaryCollection = this.bySalaryAndPosition.Range(minSalary, true, maxSalary, true);

        if (salaryCollection.Count == 0)
        {
            yield break;
        }

        foreach (var kvp in salaryCollection)
        {
            foreach (var byPos in kvp.Value)
            {
                foreach (var emp in byPos.Value)
                {
                    yield return emp;
                }
            }
        }
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