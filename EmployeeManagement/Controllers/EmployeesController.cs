using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View(await _context.employees.Include("Department").ToListAsync());
        }

        // Get Employee/ID
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = await _context.employees.Include("Department").SingleOrDefaultAsync(m => m.EmpID == id);
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        // Get Employee/Create
        public IActionResult Create()
        {
            List<SelectListItem> TimeZoneList = new List<SelectListItem>();
            var dep = _context.departments.AsEnumerable().ToList();
            foreach (var item in dep)
            {
                TimeZoneList.Add(new SelectListItem()
                {
                    Text = item.Departments,
                    Value = item.DepartmentID.ToString(),
                    Selected = false
                });
            }
            ViewBag.TimeZoneList = TimeZoneList;
            return View();
        }

        //public async Task<IActionResult> Create([Bind("EmpID,DepartmentID,FirstName,LastName,Address,Qualification,ContectNo")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(employee);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(employee);
        //}


        //Get Employee/Edit/id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            
            /////////
            List<SelectListItem> ddldept = new List<SelectListItem>();
            var dep = _context.departments.AsEnumerable().ToList();
            foreach (var item in dep)
            {
                ddldept.Add(new SelectListItem()
                {
                    Text = item.Departments,
                    Value = item.DepartmentID.ToString(),
                    Selected = false
                });
            }
            ViewBag.Department = ddldept;
            ///////////
            var Emp = await _context.employees.Include("Department").SingleOrDefaultAsync(m => m.EmpID == id);
            if (Emp == null)
            {
                return NotFound();
            }
            //ViewBag.Department = Emp;
            return View(Emp);
        }
        
    }
}
