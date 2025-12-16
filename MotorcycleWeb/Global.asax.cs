using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MotorcycleWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["HomNay"] = 0;
            Application["HomQua"] = 0;
            Application["TuanNay"] = 0;
            Application["TuanTruoc"] = 0;
            Application["ThangNay"] = 0;
            Application["ThangTruoc"] = 0;
            Application["TatCa"] = 0;
            Application["visitors_online"] = 0;

        }
        void Session_Start(object sender, EventArgs e)
        {
            
            Session.Timeout = 1;
            Application.Lock();
            Application["visitors_online"] = Convert.ToInt32(Application["visitors_online"]) + 1;
            Application.UnLock();

            try
            {       
                var item = MotorcycleWeb.Models.Common.ThongKeTruyCap.ThongKe();              
                if (item != null)
                {
                    //Application["HomNay"] = item.Homnay;
                    //Application["HomQua"] = item.Homqua;
                    //Application["TuanNay"] = item.Tuannay;
                    //Application["TuanTruoc"] = item.Tuantruoc;
                    //Application["ThangNay"] = item.Thangnay;
                    //Application["ThangTruoc"] = item.Thangtruoc;
                    //Application["TatCa"] = item.Tatca;
                    Application["HomNay"] = long.Parse("0" + item.Homnay.ToString("#,###"));
                    Application["HomQua"] = long.Parse("0" + item.Homqua.ToString("#,###"));
                    Application["TuanNay"] = long.Parse("0" + item.Tuannay.ToString("#,###"));
                    Application["TuanTruoc"] = long.Parse("0" + item.Tuantruoc.ToString("#,###"));
                    Application["ThangNay"] = long.Parse("0" + item.Thangnay.ToString("#,###"));
                    Application["ThangTruoc"] = long.Parse("0" + item.Thangtruoc.ToString("#,###"));
                    Application["TatCa"] = int.Parse("0" + item.Tatca.ToString("#,###"));
                }
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine("L?i khi c?p nh?t th?ng kê truy c?p: " + ex.Message);
            }
        }
        void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["visitors_online"] = Convert.ToUInt32(Application["visitors_online"])-1;
            Application.UnLock();
        }

        protected void Application_BeginRequest()
        {
            var culture = new System.Globalization.CultureInfo("vi-VN");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
