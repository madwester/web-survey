using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIT_research
{
    public class SessionHelper
    {
        public static void setUser(string username)
        {
            HttpContext.Current.Session["username"] = username;
        }

        public static string getUser()
        {
            if (HttpContext.Current.Session["username"] != null)
            {
                return (string)HttpContext.Current.Session["username"];
            }
            else {
                return "";
            } 
        }

        public static bool checkIfLoggedIn()
        {
            string username = getUser();
            if (username != null)
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}