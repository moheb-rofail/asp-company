using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyMvcProject.Data;
using MyMvcProject.Models;

public class EmployeeController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var employees = _context.Employee.ToList();
        return View(employees);
    }

    public IActionResult Show(int id)
    {
        var employee = _context.Employee.FirstOrDefault(e => e.Id == id);
        return View("Show", employee);
    }

    public IActionResult Create()
    {
        return View("Create");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Create", employee);
    }

    public IActionResult Edit(int id)
    {
        var employee = _context.Employee.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

        return View("Edit", employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id, Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Employee.Update(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Edit", employee);
    }
}