using System.Diagnostics.Eventing.Reader;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyMvcProject.Data;
using MyMvcProject.Models;
using MyMvcProject.Repositories;

public class EmployeeController : Controller
{
    private readonly IRepository<Employee> employeeRepository;

    public EmployeeController(IRepository<Employee> _employeeRepository)
    {
        employeeRepository = _employeeRepository;
    }

    public IActionResult Index()
    {
        var employees = employeeRepository.GetAll();
        return View(employees);
    }

    public IActionResult Show(int id)
    {
        var employee = employeeRepository.GetById(id);
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
            employeeRepository.Add(employee);
            employeeRepository.Save();
            return RedirectToAction("Index");
        }
        return View("Create", employee);
    }

    public IActionResult Edit(int id)
    {
        var employee = employeeRepository.GetById(id);

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
        if (id != employee.Id) return NotFound();

        if (ModelState.IsValid)
        {
            bool success = employeeRepository.Update(id, employee);
            if (success)
            {
                employeeRepository.Save();
                return RedirectToAction("Index");
            }
        }
        return View("Edit", employee);
    }

    public IActionResult Remove(int id)
    {
        employeeRepository.Remove(id);
        employeeRepository.Save();
        return RedirectToAction("Index");
    }
}