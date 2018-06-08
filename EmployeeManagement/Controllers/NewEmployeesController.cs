using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class NewEmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public NewEmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: NewEmployees
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.employees.Include(e => e.Department);
            return View(await employeeContext.ToListAsync());
        }

        // GET: NewEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employees
                .Include(e => e.Department)
                .SingleOrDefaultAsync(m => m.EmpID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: NewEmployees/Create
        public IActionResult Create()
        {
            ViewData["Departments"] = new SelectList(_context.departments, "DepartmentID", "Departments");
            return View();
        }

        // POST: NewEmployees/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpID,FirstName,LastName,Address,Qualification,ContectNo,DepartmentID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departments"] = new SelectList(_context.departments, "DepartmentID", "Departments", employee.DepartmentID);
            return View(employee);
        }

        // GET: NewEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employees.SingleOrDefaultAsync(m => m.EmpID == id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["Departments"] = new SelectList(_context.departments, "DepartmentID", "Departments", employee.DepartmentID);
            return View(employee);
        }

        // POST: NewEmployees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpID,FirstName,LastName,Address,Qualification,ContectNo,DepartmentID")] Employee employee)
        {
            if (id != employee.EmpID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmpID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departments"] = new SelectList(_context.departments, "DepartmentID", "DepartmentID", employee.DepartmentID);
            return View(employee);
        }

        // GET: NewEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employees
                .Include(e => e.Department)
                .SingleOrDefaultAsync(m => m.EmpID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: NewEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.employees.SingleOrDefaultAsync(m => m.EmpID == id);
            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.employees.Any(e => e.EmpID == id);
        }
    }
}
