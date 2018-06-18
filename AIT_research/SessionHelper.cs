using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIT_research
{
    public class SessionHelper
    {
        //setting user, saving in session
        public static void setUser(string username)
        {
            try
            {
                HttpContext.Current.Session["username"] = username;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not set user");
            }
        }

        //getting current user from session
        public static string getUser()
        {
            try
            {
                if (HttpContext.Current.Session["username"] != null)
                {
                    return (string)HttpContext.Current.Session["username"];
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not get user");
                return "";
            }
        }
        //checking if someone is logged in or not
        public static bool checkIfLoggedIn()
        {
            try
            {
                string username = getUser();
                if (username != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not check if user is logged in or not.");
                return false;
            }
        }
    }
}