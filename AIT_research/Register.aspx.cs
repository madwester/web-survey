﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//ADDED
using System.Data.SqlClient;
using System.Configuration;


namespace AIT_research
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //method to get current ip-address
        protected string GetIPAddress()
        {
            //get IP through PROXY
            //====================
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //should break ipAddress down, but here is what it looks like:
            // return ipAddress;
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] address = ipAddress.Split(',');
                if (address.Length != 0)
                {
                    return address[0];
                }
            }
            //if not proxy, get nice ip, give that back :(
            //ACROSS WEB HTTP REQUEST
            //=======================
            ipAddress = context.Request.UserHostAddress;//ServerVariables["REMOTE_ADDR"];

            if (ipAddress.Trim() == "::1")//ITS LOCAL(either lan or on same machine), CHECK LAN IP INSTEAD
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = System.Net.Dns.GetHostName();
                //Get Ip Host Entry
                System.Net.IPHostEntry ipHostEntries = System.Net.Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                System.Net.IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    ipAddress = arrIpAddress[1].ToString();
                }
                catch
                {
                    try
                    {
                        ipAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = System.Net.Dns.GetHostAddresses(stringHostName);
                            ipAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            ipAddress = "127.0.0.1";
                        }
                    }
                }
            }
            return ipAddress;
        }

        //button when user choose to be anonymous
        protected void continueAnonymousBtn_Click(object sender, EventArgs e)
        {
            string respondentIpAddress = GetIPAddress();
            SqlConnection connection = DatabaseHelper.GetConnection();

            //creating a string of the current time
            String currentTime = System.DateTime.Now.ToString("s");
            
            if (HttpContext.Current.Session["respondentSessionID"] == null)
            {
                SqlCommand command = new SqlCommand("INSERT INTO respondents (firstName, lastName, date, ipAddress)" +
                                                    " VALUES ('anonymous', 'anonymous', '" + currentTime + "', '" + respondentIpAddress + "');SELECT CAST(scope_identity() AS int)", connection);
                int newRespondentSessionID = (int)command.ExecuteScalar();
                HttpContext.Current.Session["respondentSessionID"] = newRespondentSessionID;

                //store answers into db
                List<Answer> answers = question.getListOfAnswerFromSession();
                foreach (Answer a in answers)
                {
                    //answerID solves itself
                    SqlCommand insertAnswerCommand = new SqlCommand("INSERT INTO answer (questionID, respondentID, text, optionID)" +
                                                                    " VALUES ('" + a.questionID + "', '" + newRespondentSessionID + "', '" + a.answerText + "', '" + a.optionID + "' );", connection);
                    int rowsaffected = insertAnswerCommand.ExecuteNonQuery();
                    if (rowsaffected <= 0)
                    {
                        //something bad happened
                    }
                }
                //empty list of answers in the session as they are stored in DB by this stage
                HttpContext.Current.Session["answers"] = null;
                Response.Redirect("Thankyou.aspx");
                connection.Close();
            }
        }
    }
}
