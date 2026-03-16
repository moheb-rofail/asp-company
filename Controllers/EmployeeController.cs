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
    private readonly EmployeeRepository _employeeRepository;

    public EmployeeController(EmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult Index()
    {
        var employees = _employeeRepository.GetAll();
        return View(employees);
    }

    public IActionResult Show(int id)
    {
        var employee = _employeeRepository.GetById(id);
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
            _employeeRepository.Add(employee);
            _employeeRepository.Save();
            return RedirectToAction("Index");
        }
        return View("Create", employee);
    }

    public IActionResult Edit(int id)
    {
        var employee = _employeeRepository.GetById(id);

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
            bool success = _employeeRepository.Update(id, employee);
            if (success)
            {
                _employeeRepository.Save();
                return RedirectToAction("Index");
            }
        }
        return View("Edit", employee);
    }

    public IActionResult Remove(int id)
    {
        _employeeRepository.Remove(id);
        _employeeRepository.Save();
        return RedirectToAction("Index");
    }
}