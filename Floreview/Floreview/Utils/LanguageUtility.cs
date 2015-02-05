using Floreview.Models;
using Floreview.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.Utils
{
    public abstract class LanguageUtility
    {
        private static String[] monthsNL = { "januari", "februari", "maart", "april", "mei", "juni", "juli", "augustus", "september", "oktober", "november", "december" };
        private static String[] monthsEN = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        private static String[] monthsFR = { "janvier", "février", "mars", "avril", "mai", "juin", "juillet", "août", "septembre", "octobre", "novembre", "decembre" };
        private static String[] monthsDE = { "Januar", "Februar", "Mârz", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember" };

        public static String GetLanguageValue(HttpRequestBase request, params String[] args)
        {
            var result = "";
            var languageCookie = request.Cookies["floreview_language_cookie"];
            if (languageCookie != null && !String.IsNullOrEmpty(languageCookie.Value))
            {
                switch (languageCookie.Value)
                {
                    case "nl":
                        result = args[0];
                        break;
                    case "en":
                        result = args[1];
                        break;
                    case "fr":
                        result = args[2];
                        break;
                    case "de":
                        result = args[3];
                        break;
                    default:
                        result = args[0];
                        break;
                }
            }

            return result;
        }

        public static String GetLanguagePublishdate(HttpRequestBase request, int month)
        {
            var result = "";
            var languageCookie = request.Cookies["floreview_language_cookie"];
            if (languageCookie != null && !String.IsNullOrEmpty(languageCookie.Value))
            {
                switch (languageCookie.Value)
                {
                    case "nl":
                        result = monthsNL[month];
                        break;
                    case "en":
                        result = monthsEN[month];
                        break;
                    case "fr":
                        result = monthsFR[month];
                        break;
                    case "de":
                        result = monthsDE[month];
                        break;
                    default:
                        result = monthsNL[month];
                        break;
                }
            }

            return result;
        }

        public static String GetLanguageBlogContent(Blog blog, int languageID)
        {
            switch (languageID)
            {
                case 0:
                    return String.Format(blog.ContentNL,
                                            blog.TitleNL,
                                            Global.Blog_Datetime_Info,
                                            blog.PublishDate.ToString("dd") + "." + blog.PublishDate.ToString("MM") + "." + blog.PublishDate.Year,
                                            Global.Blog_Author_Info,
                                            blog.Author.AccessCode,
                                            blog.Author.FirstName,
                                            blog.Author.LastName);
                case 1:
                    return String.Format(blog.ContentEN,
                                            blog.TitleEN,
                                            Global.Blog_Datetime_Info,
                                            blog.PublishDate.ToString("dd") + "." + blog.PublishDate.ToString("MM") + "." + blog.PublishDate.Year,
                                            Global.Blog_Author_Info,
                                            blog.Author.AccessCode,
                                            blog.Author.FirstName,
                                            blog.Author.LastName);
                case 2:
                    return String.Format(blog.ContentFR,
                                            blog.TitleFR,
                                            Global.Blog_Datetime_Info,
                                            blog.PublishDate.ToString("dd") + "." + blog.PublishDate.ToString("MM") + "." + blog.PublishDate.Year,
                                            Global.Blog_Author_Info,
                                            blog.Author.AccessCode,
                                            blog.Author.FirstName,
                                            blog.Author.LastName);
                case 3:
                    return String.Format(blog.ContentDE,
                                            blog.TitleDE,
                                            Global.Blog_Datetime_Info,
                                            blog.PublishDate.ToString("dd") + "." + blog.PublishDate.ToString("MM") + "." + blog.PublishDate.Year,
                                            Global.Blog_Author_Info,
                                            blog.Author.AccessCode,
                                            blog.Author.FirstName,
                                            blog.Author.LastName);
            }

            return null;
        }
    }
}