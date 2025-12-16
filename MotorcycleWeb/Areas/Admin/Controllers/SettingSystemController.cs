using MotorcycleWeb.Models;
using MotorcycleWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MotorcycleWeb.Areas.Admin.Controllers
{
    public class SettingSystemController : Controller
    {
        private ApplicationDbContext db  = new ApplicationDbContext();
        // GET: Admin/SettingSystem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_Setting()
        {
            var item = db.SystemSettings.ToList();
            return PartialView(item);
        }
        [HttpPost]
        public ActionResult AddSetting(SettingSystemViewModel req)
        {
            
            var checkTitle = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle"));
            if (checkTitle == null || string.IsNullOrEmpty(checkTitle.SettingValue))
            {
                SystemSetting set = new SystemSetting();
                set.SettingKey = "SettingTitle";
                set.SettingValue = req.SettingTitle;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkTitle.SettingValue = req.SettingTitle;
                db.Entry(checkTitle).State = EntityState.Modified;
            }

            var checkLogo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
            if (checkLogo == null || string.IsNullOrEmpty(checkLogo.SettingValue))
            {
                SystemSetting set = new SystemSetting();
                set.SettingKey = "SettingLogo";
                set.SettingValue = req.SettingLogo;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkLogo.SettingValue = req.SettingLogo;
                db.Entry(checkLogo).State = EntityState.Modified;
            }

      
            var checkHotline = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline"));
            if (checkHotline == null || string.IsNullOrEmpty(checkHotline.SettingValue))
            {
                SystemSetting set = new SystemSetting();
                set.SettingKey = "SettingHotline";
                set.SettingValue = req.SettingHotline;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkHotline.SettingValue = req.SettingHotline;
                db.Entry(checkHotline).State = EntityState.Modified;
            }

           
            var checkEmail = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
            if (checkEmail == null || string.IsNullOrEmpty(checkEmail.SettingValue))
            {
                SystemSetting set = new SystemSetting();
                set.SettingKey = "SettingEmail";
                set.SettingValue = req.SettingEmail;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkEmail.SettingValue = req.SettingEmail;
                db.Entry(checkEmail).State = EntityState.Modified;
            }

          
            var checkTitleSeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitleSeo"));
            if (checkTitleSeo == null || string.IsNullOrEmpty(checkTitleSeo.SettingValue))
            {
                SystemSetting set = new SystemSetting();
                set.SettingKey = "SettingTitleSeo";
                set.SettingValue = req.SettingTitleSeo;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkTitleSeo.SettingValue = req.SettingTitleSeo;
                db.Entry(checkTitleSeo).State = EntityState.Modified;
            }

            
            var checkDesSeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingDesSeo"));
            if (checkDesSeo == null || string.IsNullOrEmpty(checkDesSeo.SettingValue))
            {
                SystemSetting set = new SystemSetting();
                set.SettingKey = "SettingDesSeo";
                set.SettingValue = req.SettingDesSeo;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkDesSeo.SettingValue = req.SettingDesSeo;
                db.Entry(checkDesSeo).State = EntityState.Modified;
            }

            
            var checkKeySeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingKeySeo"));
            if (checkKeySeo == null || string.IsNullOrEmpty(checkKeySeo.SettingValue))
            {
                SystemSetting set = new SystemSetting();
                set.SettingKey = "SettingKeySeo";
                set.SettingValue = req.SettingKeySeo;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkKeySeo.SettingValue = req.SettingKeySeo;
                db.Entry(checkKeySeo).State = EntityState.Modified;
            }

            
            db.SaveChanges();

            
            return RedirectToAction("Index");

        }

    }
}