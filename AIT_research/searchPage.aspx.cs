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
            const int sportQuestionId = 9;
            const int travelQuestionId = 10;
            const int bankserviceQuestionId = 11;
            const int interestsQuestionId = 12;

            //GENDER
            List<ListItem> genderItem = new List<ListItem>();
            genderItem = optionList(genderQuestionId);
            foreach (ListItem item in genderItem)
            {
                genderList.Items.Add(item);
            }

            //AGE RANGE
            List<ListItem> ageItem = new List<ListItem>();
            ageItem = optionList(ageQuestionId);
            foreach (ListItem item in ageItem)
            {
                ageList.Items.Add(item);
            }

            //STATE
            List<ListItem> stateItem = new List<ListItem>();
            stateItem = optionList(stateQuestionId);
            foreach (ListItem item in stateItem)
            {
                stateList.Items.Add(item);
            }

            //BANK 
            List<ListItem> bankItem = new List<ListItem>();
            bankItem = optionList(bankQuestionId);
            foreach (ListItem item in bankItem)
            {
                banksList.Items.Add(item);
            }

            //NEWPAPER
            List<ListItem> newspaperItem = new List<ListItem>();
            newspaperItem = optionList(newspaperQuestionId);
            foreach (ListItem item in newspaperItem)
            {
                newspaperList.Items.Add(item);
            }

            //SPORT
            List<ListItem> sportsItem = new List<ListItem>();
            sportsItem = optionList(sportQuestionId);
            foreach (ListItem item in sportsItem)
            {
                sportList.Items.Add(item);
            }

            //TRAVEL
            List<ListItem> travelItem = new List<ListItem>();
            travelItem = optionList(travelQuestionId);
            foreach (ListItem item in travelItem)
            {
                travelList.Items.Add(item);
            }

            //BANK SERVICE
            List<ListItem> bankserviceItem = new List<ListItem>();
            bankserviceItem = optionList(bankserviceQuestionId);
            foreach (ListItem item in bankserviceItem)
            {
                bankserviceList.Items.Add(item);
            }

            //ADDITIONAL INTERESTS
            List<ListItem> interestItem = new List<ListItem>();
            interestItem = optionList(interestsQuestionId);
            foreach (ListItem item in interestItem)
            {
                interestList.Items.Add(item);
            }
        }
        public List<ListItem> optionList(int questionID)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't get options from database");
                return null;
            }
        }
    }
}
