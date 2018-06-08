using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    public class EmployeeDepartmentAc : Employee
    {
        public IEnumerable<SelectListItem> SelectListItem { get; set; }
    }
}