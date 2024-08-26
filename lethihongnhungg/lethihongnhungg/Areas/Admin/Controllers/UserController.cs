using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using lethihongnhungg.DB;
using System.Text;

namespace lethihongnhungg.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        WebsiteEcomEntities objWebsiteEcomEntities = new WebsiteEcomEntities();
        public ActionResult Index()
        {
            var lstuser = objWebsiteEcomEntities.Users.ToArray();
            return View(lstuser);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User objUser)
        {
            try
            {

                objUser.password = CreateMD5(objUser.password);
                objWebsiteEcomEntities.Users.Add(objUser);
                objWebsiteEcomEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                ModelState.AddModelError("", "Không thể thêm . Lỗi: " + ex.Message);
                return View(objUser);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objUser = objWebsiteEcomEntities.Users.Where(n => n.userid == id).FirstOrDefault();
            return View(objUser);


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            // Tìm user theo id
            var objUser = objWebsiteEcomEntities.Users.FirstOrDefault(n => n.userid == id);

            // Kiểm tra nếu user không tồn tại
            if (objUser == null)
            {
                return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy user
            }

            return View(objUser);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Tìm user theo id
            var objUser = objWebsiteEcomEntities.Users.FirstOrDefault(n => n.userid == id);

            // Kiểm tra nếu user không tồn tại
            if (objUser == null)
            {
                return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy user
            }

            // Xóa user và lưu thay đổi
            objWebsiteEcomEntities.Users.Remove(objUser);
            objWebsiteEcomEntities.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objUser = objWebsiteEcomEntities.Users.Where(n => n.userid == id).FirstOrDefault();
            return View(objUser);

        }

        [HttpPost]
        public ActionResult Edit(User objUser)
        {

            objUser.password = CreateMD5(objUser.password);
            objWebsiteEcomEntities.Entry(objUser).State = EntityState.Modified;
            objWebsiteEcomEntities.SaveChanges();
            return RedirectToAction("Index");

        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                //return Convert.ToHexString(hashBytes); 
                // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                StringBuilder obj = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    obj.Append(hashBytes[i].ToString("X2"));
                }
                return obj.ToString();
            }
        }
    }
}