using MotorcycleWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorcycleWeb.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: News
        public ActionResult Index()
        {
            var items = db.News.ToList();
            return View(items);
        }
        public ActionResult Details(string alias)
        {
            var model = db.News.FirstOrDefault(x => x.Alias == alias);
            return View(model);
        }
        public ActionResult Partial_News_Home()
        {
            var item = db.News.Take(3).ToList(); 
            return PartialView(item);
        }
    }
}