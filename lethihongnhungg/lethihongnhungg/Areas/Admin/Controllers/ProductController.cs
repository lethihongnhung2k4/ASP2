using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lethihongnhungg.DB;
using System.IO;
using System.Data.Entity;
using PagedList;

namespace lethihongnhungg.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        WebsiteEcomEntities objWebsiteEcomEntities = new WebsiteEcomEntities();
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            var listProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                //lấy ds sản phẩm theo từ khóa tìm kiếm
                listProduct = objWebsiteEcomEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy tất cả sản phẩm trong bảng Product
                listProduct = objWebsiteEcomEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            listProduct = listProduct.OrderByDescending(n => n.Id).ToList();
            return View(listProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
          
            try
            {
                if (objProduct.ImageUpload !=null )
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName  + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objProduct.Image = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                }
                objWebsiteEcomEntities.Products.Add(objProduct);
                objWebsiteEcomEntities.SaveChanges();
                return RedirectToAction("Index");

            }   
            catch(Exception)
            {
                return RedirectToAction("Index");
            }

          
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objWebsiteEcomEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objWebsiteEcomEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objWebsiteEcomEntities.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebsiteEcomEntities.Products.Remove(objProduct);
            objWebsiteEcomEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objWebsiteEcomEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit( Product objProduct)
        {
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objProduct.Image = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
            }
            objWebsiteEcomEntities.Entry(objProduct).State = EntityState.Modified;
            objWebsiteEcomEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}