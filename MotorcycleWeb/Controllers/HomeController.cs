using MotorcycleWeb.Models;
using MotorcycleWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorcycleWeb.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            
            //MotorcycleWeb.Models.SendGmail.SendMail("Test", "MotorcycleWeb", "Gửi thử thành công!", "email-nhan@gmail.com");
            return View();
        }
        public ActionResult Search(string searchText)
        {
            IEnumerable<Product> items = db.Products.OrderBy(x => x.Id);

            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x => x.Alias.Contains(searchText) || x.Title.Contains(searchText));
            }

            return View(items);
        }
        public ActionResult SoSanh(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return RedirectToAction("Index"); 
            }

            var idList = ids.Split(',').Select(int.Parse).ToList();

            
            var products = db.Products
                             .Where(p => idList.Contains(p.Id))
                             .ToList();

            return View(products); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Refresh()
        {
            var item = new ThongKeModel();
            ViewBag.visitors_online = HttpContext.Application["visitors_online"];
            var hn = HttpContext.Application["Homnay"];
            item.Homnay = HttpContext.Application["HomNay"].ToString();
            item.Homqua = HttpContext.Application["HomQua"].ToString();
            item.Tuannay = HttpContext.Application["TuanNay"].ToString();
            item.Tuantruoc = HttpContext.Application["TuanTruoc"].ToString();
            item.Thangnay = HttpContext.Application["ThangNay"].ToString();
            item.Thangtruoc = HttpContext.Application["ThangTruoc"].ToString();
            item.Tatca = HttpContext.Application["TatCa"].ToString();
            return PartialView("Refresh", item);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Partial_Subscribe()
        {

            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscribe(Subscribe req)
        {
            if (ModelState.IsValid)
            {
                db.Subscribes.Add(new Subscribe {Email =req.Email, CreateDate =DateTime.Now });
                db.SaveChanges();
                return Json(new {Success = true});
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage)
                              .ToList();
            return Json(new { Success = false, Errors = errors });
        }
    }
}