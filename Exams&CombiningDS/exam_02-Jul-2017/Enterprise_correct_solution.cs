using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enterprise : IEnterprise
{
    private Dictionary<Guid, Employee> employeesByGuid;

    public Enterprise()
    {
        this.employeesByGuid = new Dictionary<Guid, Employee>();
    }

    public int Count => this.employeesByGuid.Count;

    public void Add(Employee employee)
    {
        if (this.employeesByGuid.ContainsKey(employee.Id))
        {
            throw new InvalidOperationException();
        }

        this.employeesByGuid.Add(employee.Id, employee);
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        List<Employee> result = new List<Employee>();

        foreach (var empl in this.employeesByGuid.Values)
        {
            if (empl.Position == position && empl.Salary >= minSalary)
            {
                result.Add(empl);
            }
        }

        return result;
    }

    public bool Change(Guid guid, Employee employee)
    {
        if (!this.Contains(guid))
        {
            return false;
        }
        this.employeesByGuid[guid] = employee;
        return true;
    }

    public bool Contains(Guid guid)
    {
        if (this.employeesByGuid.ContainsKey(guid))
        {
            return true;
        }
        return false;
    }

    public bool Contains(Employee employee)
    {
        return this.Contains(employee.Id);
    }

    public bool Fire(Guid guid)
    {
        Employee fired;
        employeesByGuid.TryGetValue(guid, out fired);
        if (fired == null)
        {
            return false;
        }

        this.employeesByGuid.Remove(fired.Id);
        return true;
    }

    public Employee GetByGuid(Guid guid)
    {
        if (!this.Contains(guid))
        {
            throw new ArgumentException();
        }
        return this.employeesByGuid[guid];
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        List<Employee> result = new List<Employee>();
        foreach (var empl in this.employeesByGuid.Values)
        {
            if (empl.Position == position)
            {
                result.Add(empl);

            }
        }
        if (result.Count == 0)
        {
            throw new ArgumentException();
        }
        else
        {
            return result;
        }
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        List<Employee> result = new List<Employee>();
        foreach (var empl in this.employeesByGuid.Values)
        {
            if (empl.Salary >= minSalary)
            {
                result.Add(empl);
            }
        }

        if (result.Count == 0)
        {
            throw new InvalidOperationException("");
        }
        return result;
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        List<Employee> result = new List<Employee>();
        foreach (var empl in this.employeesByGuid.Values)
        {
            if (empl.Salary >= salary && empl.Position == position)
            {
                result.Add(empl);
            }
        }
        if (result.Count == 0)
        {
            throw new InvalidOperationException();

        }
        else
        {
            return result;
        }
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        foreach (var empl in this.employeesByGuid.Values)
        {
            yield return empl;
        }
    }

    public Position PositionByGuid(Guid guid)
    {
        if (!this.Contains(guid))
        {
            throw new InvalidOperationException();
        }
        return this.employeesByGuid[guid].Position;
    }

    public bool RaiseSalary(int months, int percent)
    {
        bool raised = false;
        foreach (var empl in employeesByGuid.Values)
        {
            if (empl.HireDate.AddMonths(months) <= DateTime.Today)
            {
                empl.Salary = empl.Salary + empl.Salary * percent / 100;
                raised = true;
            }
        }
        return raised;
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        foreach (var empl in this.employeesByGuid.Values)
        {
            if (empl.FirstName == firstName)
            {
                yield return empl;
            }
        }
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        foreach (var empl in this.employeesByGuid.Values)
        {
            if (empl.FirstName == firstName && empl.LastName == lastName && empl.Position == position)
            {
                yield return empl;
            }
        }
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        List<Employee> result = new List<Employee>();
        foreach (var empl in this.employeesByGuid.Values)
        {
            foreach (var pos in positions)
            {
                if (empl.Position == pos)
                {
                    result.Add(empl);
                }
            }
        }
        return result;
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        foreach (var empl in this.employeesByGuid.Values)
        {
            if (empl.Salary >= minSalary && empl.Salary <= maxSalary)
            {
                yield return empl;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    
}
