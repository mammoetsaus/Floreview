using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Floreview.Controllers
{
    public class LanguageController : Controller
    {
        public ActionResult Change(String l)
        {
            if (ModelState.IsValid && !String.IsNullOrEmpty(l))
            {
                HttpCookie languageCookie = new HttpCookie("floreview_language_cookie", l);
                languageCookie.Expires = DateTime.Now.AddYears(1);
                Response.SetCookie(languageCookie);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}