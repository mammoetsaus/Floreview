using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Floreview.Controllers
{
    public class LanguageController : Controller
    {
        public ActionResult Change(String lang, String returnURL)
        {
            if (ModelState.IsValid && !String.IsNullOrEmpty(lang))
            {
                HttpCookie languageCookie = new HttpCookie("floreview_language_cookie", lang);
                languageCookie.Expires = DateTime.Now.AddYears(1);
                Response.SetCookie(languageCookie);
            }

            return Redirect(returnURL);
        }
    }
}