using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jquerypractice.Models;
using jquerypractice.ViewModel;

namespace jquerypractice.Controllers
{
    public class EmployeeController : Controller
    {
        itemdbEntities db = new itemdbEntities();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllEmployee()
        {
            var data = db.employees.ToList();
            return Json(data: new {success = true, data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddUpdateEmployee(EmployeeViewModel objEmployeeViewModel)
        {
            string Message = "Data Successfully Updated";
            if (!ModelState.IsValid)
            {
                var errorlist = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage.ToList());
                return Json(data: new { success = false, Message = "Validation Error", Errorlist = errorlist });
          
            }

            employee objemployee = db.employees.FirstOrDefault(model => model.EmployeeID == objEmployeeViewModel.EmployeeId) ?? new employee();

            objemployee.EmployeeID = objEmployeeViewModel.EmployeeId;
            objemployee.FirstName = objEmployeeViewModel.FirstName;
            objemployee.LastName = objEmployeeViewModel.LastName;
            objemployee.Department = objEmployeeViewModel.Department;
            objemployee.JobType = objEmployeeViewModel.JobType;
            objemployee.Salary = objEmployeeViewModel.Salary;

            if (objEmployeeViewModel.EmployeeId == 0)
            {
                Message = "Data Successfully Added";
                db.employees.Add(objemployee);
            }

            db.SaveChanges();
            return Json(data: new {Success = true, Message = Message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditEmployee(int employeeId)
        {
            return Json(data:db.employees.FirstOrDefault(model => model.EmployeeID == employeeId), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult DeleteEmployee(int employeeId)
        {
            string Message = "Data Deleted Successfully.";
            var data = db.employees.Single(model => model.EmployeeID == employeeId);
            db.employees.Remove(data);
            db.SaveChanges();
            return Json(data: new { Success = true, Message = Message }, JsonRequestBehavior.AllowGet);
        }
    }
}