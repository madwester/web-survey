using System;
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
    public partial class loginStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            //creating login inputs from user to variables
            string username = usernameTextbox.Text;
            string password = passwordTextbox.Text;

            try
            {
                //connecting to db
                SqlConnection connection = DatabaseHelper.GetConnection();
                //checking in db if user exist and if password is correct with that user
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM staff WHERE username = '" + username + "'AND password = '" + password + "'", connection);
                int rowsaffected = (int)command.ExecuteScalar();
                //if the command passes rowsaffected will be more than 0
                if (rowsaffected > 0)
                {
                    //username exist and password is corrent
                    SessionHelper.setUser(username);
                    Response.Redirect("searchPage.aspx");
                }
                else
                {
                    //username or password is incorrect
                    loginErrorLabel.Text = "Your username or password is invalid.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not continue log in staff..");
            }
        }
    }
}