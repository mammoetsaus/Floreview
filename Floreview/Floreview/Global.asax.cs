using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Floreview
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie languageCookie = Request.Cookies["floreview_language_cookie"];
            if (languageCookie != null && !String.IsNullOrEmpty(languageCookie.Value))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(languageCookie.Value);
            }
            else
            {
                languageCookie = new HttpCookie("floreview_language_cookie", Request.UserLanguages[0].Substring(0, 2).ToLower());
                languageCookie.Expires = DateTime.Now.AddYears(1);

                Response.SetCookie(languageCookie);

                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(languageCookie.Value);
            }
        }
    }
}
