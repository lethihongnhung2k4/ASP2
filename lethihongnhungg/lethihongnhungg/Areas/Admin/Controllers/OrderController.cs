using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using lethihongnhungg.DB;

namespace lethihongnhungg.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        WebsiteEcomEntities objWebsiteEcomEntities = new WebsiteEcomEntities();
        public ActionResult Index()
        {
            var lstorder = objWebsiteEcomEntities.Orders.ToArray();
            return View(lstorder);
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            var objOrder = objWebsiteEcomEntities.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);


        }
    }
}