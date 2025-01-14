﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using lethihongnhungg.DB;

namespace lethihongnhungg.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        // GET: Admin/Brand
        WebsiteEcomEntities objWebsiteEcomEntities = new WebsiteEcomEntities();
        public ActionResult Index()
        {
            var lstbrand = objWebsiteEcomEntities.Brands.ToArray();
            return View(lstbrand);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand objCategory)
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
                objWebsiteEcomEntities.Brands.Add(objCategory);
                objWebsiteEcomEntities.SaveChanges();
                return RedirectToAction("Index");
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
            var objCategory = objWebsiteEcomEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objWebsiteEcomEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);

        }

        [HttpPost]
        public ActionResult Delete(Brand objBrand)
        {
            var objCategory = objWebsiteEcomEntities.Brands.Where(n => n.Id == objBrand.Id).FirstOrDefault();
            objWebsiteEcomEntities.Brands.Remove(objCategory); objWebsiteEcomEntities.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = objWebsiteEcomEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);

        }

        [HttpPost]
        public ActionResult Edit(Brand objCategory)
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
            return RedirectToAction("Index");

        }
    }
}