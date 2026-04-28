using Dapper;
using EmployeeManagementSystem.DatabaseConnection;
using EmployeeManagementSystem.Models;
using System.Data;

namespace EmployeeManagementSystem
{
    public class Service
    {
        private readonly DbConnection _connection;

        public Service()
        {
            _connection = new DbConnection();
        }

        //Retrieve all employees
        public IEnumerable<Employee> GetEmployees()
        {
            using var connection = _connection.StartConnection();

            var employees = connection.Query<Employee>(
                "Select * from Employees"
                );
            return employees;
        }

        //Retrieve a single employee by ID
        //Handle case when employee does not exist
        public Employee? GetEmployeeDetailsById(int id)
        {

            using var connection = _connection.StartConnection();
            var employee = connection.QueryFirstOrDefault<Employee>(
                "Select * from Employees where Id = @Id",
                new { Id = id }
                );
            if (employee is null)
            {
                Console.WriteLine("Employee Not Found");
            }
            return employee;
        }


        // 3. Scalar Query
        // Get total number of employees
        public int GetTotalEmployeeCount()
        {
            using var connection = _connection.StartConnection();
            return connection.QueryFirstOrDefault<int>(
                "select count(*) from Employees");
        }

        //Get highest salary
        public decimal GetHighestSalary()
        {
            using var connection = _connection.StartConnection();
            return connection.QueryFirstOrDefault<decimal>(
                "select max(Salary) from Employees");
        }


        //Get average salary
        public decimal GetAverageSalary()
        {
            using var connection = _connection.StartConnection();
            return connection.QueryFirstOrDefault<decimal>(
                "select avg(Salary) from Employees");
        }


        // 4. Data Manipulation(Execute)
        //Insert a new employee

        public void InsertEmployee(Employee employee)
        {
            using var connection = _connection.StartConnection();
            var affectedRows = connection.Execute(
                "Insert into Employees (Name, Salary, DepartmentId) values (@Name, @Salary, @DepartmentId)",
                new { employee.Name, employee.Salary, employee.DepartmentId }
                );

            if (affectedRows > 0)
            {
                Console.WriteLine($"Employee {employee.Name} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to insert employee {employee.Name}.");
            }
        }

        //Update employee salary
        public void UpdateEmployee(int id, decimal salary)
        {
            using var connection = _connection.StartConnection();
            var affectedRows = connection.Execute(
                "Update Employees set Salary = @Salary where Id = @Id",
                new { Salary = salary, Id = id }
                );

            if (affectedRows > 0)
            {
                Console.WriteLine($"Employee with ID {id} updated successfully.");
            }
            else
            {
                Console.WriteLine($"Employee with ID {id} not found.");
            }
        }

        //Delete employee
        public void DeleteEmployee(int id)
        {
            using var connection = _connection.StartConnection();
            var affectedRows = connection.Execute(
                "Delete from Employees where Id = @Id",
                new { Id = id }
                );

            if (affectedRows > 0)
            {
                Console.WriteLine($"Employee with ID {id} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Employee with ID {id} not found.");
            }
        }


        // 5. Relationship Handling (Multi-Mapping)

        // Retrieve employees along with their department data

        // Display employee name +department name

        // Ensure correct mapping between both entities

        public IEnumerable<Employee> GetEmployeesWithDepartment()
        {
            using var connection = _connection.StartConnection();
            var employees = connection.Query<Employee, Department, Employee>(
                "Select e.Id, e.Name, e.Salary, e.DepartmentId, d.Id, d.Name from Employees e inner join Departments d on e.DepartmentId = d.Id",
                (employee, department) =>
                {
                    employee.Department = department;
                    return employee;
                },
                splitOn: "Id"
                );
            return employees;

        }


        // Create and use stored procedures for:
        // Retrieve all employees

        public IEnumerable<Employee> GetEmployeesUsingSP()
        {
            using var connection = _connection.StartConnection();

            return connection.Query<Employee>(
                "GetAllEmployees",
                commandType: CommandType.StoredProcedure
            );
        }

        // Insert employee
        public void InsertEmployeeUsingSP(Employee employee)
        {
            using var connection = _connection.StartConnection();

            var affectedRows = connection.Execute(
                "InsertEmployee",
                new
                {
                    employee.Name,
                    employee.Salary,
                    employee.DepartmentId
                },
                commandType: CommandType.StoredProcedure
            );

            if (affectedRows > 0)
                Console.WriteLine($"Employee {employee.Name} inserted successfully (SP).");
            else
                Console.WriteLine($"Failed to insert employee {employee.Name} (SP).");
        }


    }
}