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
            const int genderQuestionId = 1;
            const int ageQuestionId = 2;
            const int stateQuestionId = 3;
            const int bankQuestionId = 7;
            const int newspaperQuestionId = 8;

            List<ListItem> genderItem = new List<ListItem>();
            genderItem = optionList(genderQuestionId);
            foreach (ListItem item in genderItem)
            {
                genderList.Items.Add(item);
            }

        }
        public List<ListItem> optionList(int questionID)
        {
            List<ListItem> items = new List<ListItem>();
            //getting connection from database helper class
            SqlConnection connection = DatabaseHelper.GetConnection();

            SqlCommand optionItemsCommand = new SqlCommand("SELECT * FROM [option] WHERE questionID = " + questionID, connection);
            SqlDataReader optionItemsReader = optionItemsCommand.ExecuteReader();
            while (optionItemsReader.Read())
            {
                ListItem optionItem = new ListItem(optionItemsReader["value"].ToString(), optionItemsReader["optionID"].ToString());
                items.Add(optionItem);
            }
            connection.Close();
            return items;
        }
    }
}
