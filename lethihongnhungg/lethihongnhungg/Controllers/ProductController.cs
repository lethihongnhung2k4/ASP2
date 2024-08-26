using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lethihongnhungg.DB;

namespace lethihongnhungg.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        WebsiteEcomEntities objWebsiteEcomEntities = new WebsiteEcomEntities();
       
        public ActionResult Detail(int id)
        {
            var detail = objWebsiteEcomEntities.Products.Find(id);
            return View(detail);
        }
        //public ActionResult Detail()
        //{
        //    return View();
        //}

    }
}