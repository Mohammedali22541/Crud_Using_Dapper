using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Salary: {Salary}, DepartmentId: {DepartmentId}";
        }
    }


}
