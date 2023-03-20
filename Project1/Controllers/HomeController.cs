using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {

        MVCcrudDBContext _context = new MVCcrudDBContext();


        public ActionResult Index()
        {
            var employeeList = _context.Employees.OrderBy(x => x.Name).ToList();
            return View(employeeList);
        }


        [HttpGet]
        public ActionResult Create() {
        
            return View();

        }

        [HttpPost]
        public ActionResult Create(Employee model)
        { 
            try
            {
                _context.Employees.Add(model);
                _context.SaveChanges();
                TempData["Message"] = "Employee successfully added to Employee List";
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Employee id already exist in the system");

            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _context.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Employee model)
        {
            
                var data = _context.Employees.Where(x => x.EmployeeId == model.EmployeeId).FirstOrDefault();
                if (data != null)
                {
                    data.Name = model.Name;
                    data.Department = model.Department;
                    data.Salary = model.Salary;
                    data.Designation = model.Designation;
                    data.ManagerId = model.ManagerId;
                    _context.SaveChanges();
                }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var data = _context.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            return View(data);
        }


        [HttpPost]
        public ActionResult Delete(Employee model)
        {
            var data = _context.Employees.Where(x => x.EmployeeId == model.EmployeeId).FirstOrDefault();
            _context.Employees.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var data = _context.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            return View(data);
        }

        
        public ActionResult Manager()
        {
            var managers = _context.Employees.Where(x => x.Designation == "Manager"|| x.Designation == "manager").OrderBy(x => x.Name).ToList();
            return View(managers);
        }
    }
}