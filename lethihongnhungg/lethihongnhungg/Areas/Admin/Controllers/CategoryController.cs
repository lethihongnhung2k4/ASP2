using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.IO;
using System.Web;
using lethihongnhungg.DB;
using System.Web.Mvc;

namespace lethihongnhungg.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        WebsiteEcomEntities objWebsiteEcomEntities = new WebsiteEcomEntities();
        public ActionResult Index1()
        {
            var lstcategory = objWebsiteEcomEntities.Categories.ToArray();
            return View(lstcategory);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            try
            {
                if (objCategory.ImageUpload != null)
                {
                    // Lấy tên file không có phần mở rộng
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                    // Lấy phần mở rộng
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    // Tạo tên file mới với thời gian hiện tại để tránh trùng tên
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    // Đặt đường dẫn lưu file
                    string path = Path.Combine(Server.MapPath("~/Content/images/items/"), fileName);
                    // Lưu file vào thư mục
                    objCategory.ImageUpload.SaveAs(path);
                    // Lưu tên file vào cơ sở dữ liệu
                    objCategory.Image = fileName;
                }
                objWebsiteEcomEntities.Categories.Add(objCategory);
                objWebsiteEcomEntities.SaveChanges();
                return RedirectToAction("Index1");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                ModelState.AddModelError("", "Không thể thêm danh muc. Lỗi: " + ex.Message);
                return View(objCategory);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = objWebsiteEcomEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objWebsiteEcomEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);

        }

        [HttpPost]
        public ActionResult Delete(Category objCate)
        {
            var objCategory = objWebsiteEcomEntities.Categories.Where(n => n.Id == objCate.Id).FirstOrDefault();
            objWebsiteEcomEntities.Categories.Remove(objCategory); 
            objWebsiteEcomEntities.SaveChanges();
            return RedirectToAction("Index1");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = objWebsiteEcomEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);

        }

        [HttpPost]
        public ActionResult Edit(Category objCategory)
        {
            if (objCategory.ImageUpload != null)
            {
                // Lấy tên file không có phần mở rộng
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                // Lấy phần mở rộng
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                // Tạo tên file mới với thời gian hiện tại để tránh trùng tên
                fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                // Đặt đường dẫn lưu file
                string path = Path.Combine(Server.MapPath("~/Content/images/items/"), fileName);
                // Lưu file vào thư mục
                objCategory.ImageUpload.SaveAs(path);
                // Lưu tên file vào cơ sở dữ liệu
                objCategory.Image = fileName;
            }
            objWebsiteEcomEntities.Entry(objCategory).State = EntityState.Modified;
            objWebsiteEcomEntities.SaveChanges();
            return RedirectToAction("Index1");

        }
    }
}