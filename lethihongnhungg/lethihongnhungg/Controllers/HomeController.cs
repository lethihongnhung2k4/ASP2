using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lethihongnhungg.Models;

using lethihongnhungg.DB;

namespace lethihongnhungg.Controllers
{
    public class HomeController : Controller

    {

        WebsiteEcomEntities objWebsiteEcomEntities = new WebsiteEcomEntities();
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.ListProduct = objWebsiteEcomEntities.Products.ToList();
            model.ListCategory = objWebsiteEcomEntities.Categories.ToList();

            return View(model);
        }
        //public ActionResult Index1()
        //{
        //    WebsiteEcomEntities objWebsiteEcomEntities = new WebsiteEcomEntities();
        //    var lstProduct = objWebsiteEcomEntities.Products.ToList();
        //    return View(lstProduct);
        //}
       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}