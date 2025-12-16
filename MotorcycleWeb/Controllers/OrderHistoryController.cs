using MotorcycleWeb.Models;
using MotorcycleWeb.Models.EF;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MotorcycleWeb.Controllers
{
    [CustomerAuthorize]
    public class OrderHistoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Danh sách đơn hàng của người dùng hiện tại
        public ActionResult MyOrders()
        {
            var userEmail = User.Identity.GetUserName(); // hoặc GetUserId() nếu bạn lưu theo Id

            var orders = db.Orders
                .Where(o => o.Email == userEmail)
                .OrderByDescending(o => o.CreateDate)
                .ToList();

            return View(orders);
        }

        // Chi tiết đơn hàng
        public ActionResult Details(int id)
        {
            var userEmail = User.Identity.GetUserName();
            var order = db.Orders
                .Include("OrderDetails.Product")
                .FirstOrDefault(o => o.Id == id && o.Email == userEmail);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }
    }
}
