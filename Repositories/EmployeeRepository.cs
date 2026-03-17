using Microsoft.AspNetCore.Mvc;
using MyMvcProject.Data;
using MyMvcProject.Models;

namespace MyMvcProject.Repositories;

public class EmployeeRepository:IRepository<Employee>
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Employee> GetAll()
    {
        return _context.Employee.ToList();
    }

    public Employee GetById(int id)
    {
        Employee emp = _context.Employee.FirstOrDefault(e => e.Id == id);
        return emp;
    }

    public bool Add(Employee emp)
    {
        _context.Add(emp);
        return true;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public bool Update(int id, Employee formEmp)
    {
        Employee employee = GetById(id);
        if (employee == null) return false;

        employee.Name = formEmp.Name;
        employee.Address = formEmp.Address;
        employee.Phone = formEmp.Phone;

        // _context.Update(emp); we did it manually above
        return true;       
    }

    public void Save()
    {
        _context.SaveChanges();
    }


    public bool Remove(int id)
    {
        Employee emp = GetById(id);
        if (emp != null){
            _context.Remove(emp);
            return true;
        }
        return false;
    }
}