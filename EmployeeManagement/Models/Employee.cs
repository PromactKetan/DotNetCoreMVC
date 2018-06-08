using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Address { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string ContectNo { get; set; }

        [ForeignKey("Department")]
        [Required]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }

    }
}
    