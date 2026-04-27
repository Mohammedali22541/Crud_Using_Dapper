using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();
            var allEmployees = service.GetEmployees();
            foreach (var employee in allEmployees)
            {
                Console.WriteLine(employee);
            }


            Console.WriteLine(new string('-', 90));
            var employeeDetails = service.GetEmployeeDetailsById(1);
            Console.WriteLine(employeeDetails);



            Console.WriteLine(new string('-', 90));
            var totalEmployeeCount = service.GetTotalEmployeeCount();
            Console.WriteLine($"Total Employee Count: {totalEmployeeCount}");



            Console.WriteLine(new string('-', 90));
            var highestSalary = service.GetHighestSalary();
            Console.WriteLine($"Highest Salary: {highestSalary}");



            Console.WriteLine(new string('-', 90));
            var averageSalary = service.GetAverageSalary();
            Console.WriteLine($"Average Salary: {averageSalary}");



            //Console.WriteLine(new string('-', 90));
            //service.InsertEmployee(new Models.Employee { Name = "Mohammed ali", Salary = 50000, DepartmentId = 1 });



            //Console.WriteLine(new string('-', 90));
            //service.UpdateEmployee(1, 60000);



            //Console.WriteLine(new string('-', 90));
            //service.DeleteEmployee(7);
            Console.WriteLine(new string('-', 90));
            Console.WriteLine("Employee With Department");
            Console.WriteLine(new string('-', 90));
            var employessWithDepartment = service.GetEmployeesWithDepartment();
            foreach (var employee in employessWithDepartment)
            {
                Console.WriteLine($"Employee: {employee.Name}, Department: {employee.Department?.Name}");
            }


            Console.WriteLine(new string('-', 90));
            Console.WriteLine("Employee Using Stored Procedure");
            Console.WriteLine(new string('-', 90));
            var employeesUsingSP = service.GetEmployeesUsingSP();
            foreach (var employee in employeesUsingSP)
            {
                Console.WriteLine(employee);
            }


            Console.WriteLine(new string('-', 90));
            Console.WriteLine("Insert Employee Using Stored Procedure");
             Console.WriteLine(new string('-', 90));
            service.InsertEmployeeUsingSP(new Models.Employee { Name = "ALi ali", Salary = 50000, DepartmentId = 1 });
        }
    }
}
