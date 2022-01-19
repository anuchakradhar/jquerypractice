using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jquerypractice.Models;

namespace jquerypractice.Controllers
{
    public class DynamicController : Controller
    {
        itemdbEntities db = new itemdbEntities(); 

        // GET: Dynamic
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllItems()
        {
            var data = db.tbl_Item.ToList();
            return Json(data: new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertCustomers(List<tbl_Item> items)
        {
                
                if (items == null)
                {
                    items = new List<tbl_Item>();
                }

                
                foreach (tbl_Item item in items)
                {
                    db.tbl_Item.Add(item);
                }
                db.SaveChanges();
                return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveData()
        {
            var list = db.floors.ToList();
            ViewBag.build_floor = new SelectList(db.floors.ToList(), "floor_id", "floor_name");
            return View(list);
        }

        [HttpPost]
        public ActionResult SaveData(List<floor> floor)
        {

            foreach (var data in floor)
            {
                db.floors.Add(data);
            }

            db.SaveChanges();

            
            return RedirectToAction("SaveData");
        }

        [HttpPost]
        public ActionResult UpdateFloor(floor floor)
        {

            var updatedFloor = (from c in db.floors
                                        where c.floor_id == floor.floor_id
                                        select c).FirstOrDefault();
            updatedFloor.floor_name = floor.floor_name;
            updatedFloor.area = floor.area;
            updatedFloor.rate = floor.rate;
            db.SaveChanges();


            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteFloor(int floor_id)
        {

            var data = (from c in db.floors
                                 where c.floor_id == floor_id
                                 select c).FirstOrDefault();
            db.floors.Remove(data);
            db.SaveChanges();


            return new EmptyResult();
        }

    }
}