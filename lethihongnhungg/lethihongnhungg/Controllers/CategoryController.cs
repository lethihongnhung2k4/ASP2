using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lethihongnhungg.Models;
using lethihongnhungg.DB;

namespace lethihongnhungg.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        WebsiteEcomEntities objWebsiteEcomEntities = new WebsiteEcomEntities();
        public ActionResult Index()
        {
            var lstCategory = objWebsiteEcomEntities.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductByCategoryID(int id)
        {
            var lstproduct = objWebsiteEcomEntities.Products.Where(n => n.CategoryId == id).ToList();
            return View(lstproduct);

        }
    }
}