using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lethihongnhungg.DB;
using lethihongnhungg.Models;

namespace lethihongnhungg.Controllers
{
    public class PaymentController : Controller
    {
        WebsiteEcomEntities objWebsiteEcomEntities = new WebsiteEcomEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {

                //lấy thông từ giỏ hàng từ biến session
                var lstcart = (List<CartModel>)Session["cart"];
                //gán dữ liệu cho Order
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());

                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objWebsiteEcomEntities.Orders.Add(objOrder);
                //lưu thông tin dữ liệu vào bảng order 
                objWebsiteEcomEntities.SaveChanges();
                //Lấy OrderId vừa mới tạo để lưu vào bảng OrderDetail.
                int intOrderId = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                foreach (var item in lstcart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);

                }
                objWebsiteEcomEntities.OrderDetails.AddRange(lstOrderDetail);
                objWebsiteEcomEntities.SaveChanges();

            }
            return View();
        }
    }
}