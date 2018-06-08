using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        [Required]
        public string Departments { get; set; }

        [StringLength(60, MinimumLength = 10)]
        [Required]
        public string Deescription { get; set;}

        //public ICollection<Employee> Employees { get; set; }
    }
}
