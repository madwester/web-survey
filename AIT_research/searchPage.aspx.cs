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
    public partial class searchPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*SqlConnection connection = DatabaseHelper.GetConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM [option] WHERE questionID = " + connection);
            SqlDataReader reader = command.ExecuteReader();

            ListItem optionItem = new ListItem(reader["value"].ToString());*/
        }

        //variable for each question id
    }
}