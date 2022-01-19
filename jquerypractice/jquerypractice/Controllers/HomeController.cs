using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jquerypractice.Models; 

namespace jquerypractice.Controllers
{
    public class HomeController : Controller
    {
        itemdbEntities entities = new itemdbEntities();
        public ActionResult Index()
        {
            var customers = entities.customers.ToList();
            return View(customers);
         
        }

        [HttpPost]
        public JsonResult InsertCustomer(customer customer)
        {
            entities.customers.Add(customer);
            entities.SaveChanges();

            return Json(customer);
        }

        [HttpPost]
        public ActionResult UpdateCustomer(customer customer)
        {
            
                customer updatedCustomer = (from c in entities.customers
                                            where c.CustomerId == customer.CustomerId
                                            select c).FirstOrDefault();
                updatedCustomer.Name = customer.Name;
                updatedCustomer.Country = customer.Country;
                entities.SaveChanges();
            

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int customerId)
        {
           
                customer customer = (from c in entities.customers
                                     where c.CustomerId == customerId
                                     select c).FirstOrDefault();
                entities.customers.Remove(customer);
                entities.SaveChanges();
            

            return new EmptyResult();
        }
    }
}