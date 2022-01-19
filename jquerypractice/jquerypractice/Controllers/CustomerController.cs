using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jquerypractice.Models;

namespace jquerypractice.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            itemdbEntities entities = new itemdbEntities();
            return View(entities.customers);
        }

        public JsonResult InsertCustomers(List<customer> customers)
        {
            using (itemdbEntities entities = new itemdbEntities())
            {
                //Truncate Table to delete all old records.
                entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Customers]");

                //Check for NULL.
                if (customers == null)
                {
                    customers = new List<customer>();
                }

                //Loop and insert records.
                foreach (customer customer in customers)
                {
                    entities.customers.Add(customer);
                }
                int insertedRecords = entities.SaveChanges();
                return Json(insertedRecords);
            }
        }
    }
}