using MotorcycleWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorcycleWeb.Areas.Admin.Controllers
{
    public class StatisticsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [AdminAuthorize(Roles = "Admin")]

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStatistics(string fromDate, string toDate)
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails on
                        o.Id equals od.OrderId
                        join p in db.Products
                        on od.ProductId equals p.Id
                        select new
                        {
                            CreatedDate = o.CreateDate,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            OriginalPrice = p.OriginalPrice
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                // SỬ DỤNG LỌC >= ĐỂ BAO GỒM TOÀN BỘ NGÀY BẮT ĐẦU
                query = query.Where(x => x.CreatedDate >= startDate);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                // TĂNG NGÀY KẾT THÚC LÊN 1 NGÀY VÀ LỌC < (NHỎ HƠN) ĐỂ BAO GỒM TRỌN VẸN NGÀY ĐÓ
                DateTime exclusiveEndDate = endDate.AddDays(1);
                query = query.Where(x => x.CreatedDate < exclusiveEndDate);
            }
            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(x => new
            {
                Date = x.Key.Value,
                TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                TotalSell = x.Sum(y => y.Quantity * y.Price)
            }).Select(x => new
            {
                Date = x.Date,
                DoanhThu = x.TotalSell,
                LoiNhuan = x.TotalSell - x.TotalBuy,
            });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}