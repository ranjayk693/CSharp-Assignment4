using System;
using System.Linq;
using System.Collections.Generic;


public enum SalaryType
{
    Monthly,
    Performance,
    Bonus
}


//Defining class Employee
public class Employee
{
    public int EmployeeID { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public int Age { get; set; }
}


//Defining class Salary
public class Salary
{
    public int EmployeeID { get; set; }
    public int Amount { get; set; }
    public SalaryType Type { get; set; }
}
//Main class
public class Program
{
    IList<Employee> employeeList;
    IList<Salary> salaryList;

    public Program()
    {
        //List of employee
        employeeList = new List<Employee> {
            new Employee { EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49 },
            new Employee { EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32 },
            new Employee { EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28 },
            new Employee { EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26 },
            new Employee { EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20 },
            new Employee { EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60 },
            new Employee { EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53 }
        };

        //List of salary
        salaryList = new List<Salary> {
            new Salary { EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly },
            new Salary { EmployeeID = 1, Amount = 500, Type = SalaryType.Performance },
            new Salary { EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus },
            new Salary { EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly },
            new Salary { EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus },
            new Salary { EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly },
            new Salary { EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly },
            new Salary { EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly },
            new Salary { EmployeeID = 5, Amount = 600, Type = SalaryType.Performance },
            new Salary { EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus },
            new Salary { EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly },
            new Salary { EmployeeID = 6, Amount = 400, Type = SalaryType.Performance },
            new Salary { EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly }
        };
    }

    //main method
    public static void Main()
    {
        Program program = new Program();

        program.Task1();

        program.Task2();

        program.Task3();
    }


    //Task1 method
    public void Task1()
    {
        var totalSalaries = from emp in employeeList
                            join sal in salaryList on emp.EmployeeID equals sal.EmployeeID
                            group sal.Amount by new { emp.EmployeeFirstName, emp.EmployeeLastName } into g
                            orderby g.Sum() ascending
                            select new
                            {
                                EmployeeName = $"{g.Key.EmployeeFirstName} {g.Key.EmployeeLastName}",
                                TotalSalary = g.Sum()
                            };

        Console.WriteLine("Total Salary of all employees in ascending order:");
        foreach (var item in totalSalaries)
        {
            Console.WriteLine($"{item.EmployeeName}: {item.TotalSalary}");
        }
        Console.WriteLine();
    }


    //Task2 Method
    public void Task2()
    {
        var secondOldestEmployee = (from emp in employeeList
                                    orderby emp.Age descending
                                    select emp).Skip(1).FirstOrDefault();

        var monthlySalary = (from sal in salaryList
                             where sal.EmployeeID == secondOldestEmployee.EmployeeID && sal.Type == SalaryType.Monthly
                             select sal.Amount).Sum();

        Console.WriteLine("Employee details of 2nd oldest employee:");
        Console.WriteLine($"Employee ID: {secondOldestEmployee.EmployeeID}");
        Console.WriteLine($"Name: {secondOldestEmployee.EmployeeFirstName} {secondOldestEmployee.EmployeeLastName}");
        Console.WriteLine($"Age: {secondOldestEmployee.Age}");
        Console.WriteLine($"Total Monthly Salary: {monthlySalary}");
        Console.WriteLine();
    }

    //Task3 method
    public void Task3()
    {
        var meanSalaries = from emp in employeeList
                           where emp.Age > 30
                           join sal in salaryList on emp.EmployeeID equals sal.EmployeeID
                           group sal.Amount by sal.Type into g
                           select new
                           {
                               SalaryType = g.Key,
                               MeanSalary = g.Average()
                           };

        Console.WriteLine("Mean of Monthly, Performance, Bonus salaries of employees > 30:");
        foreach (var item in meanSalaries)
        {
            Console.WriteLine($"{item.SalaryType}: {item.MeanSalary}");
        }
        Console.WriteLine();
    }
}

