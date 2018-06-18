using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//ADDED
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace AIT_research
{
    public partial class searchPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //displaying search options only first time page loads
            if (!IsPostBack)
            {
                //creating a constant variable to read the id of question from database
                const int genderQuestionId = 1;
                const int ageQuestionId = 2;
                const int stateQuestionId = 3;
                const int bankQuestionId = 7;
                const int newspaperQuestionId = 8;
                const int sportQuestionId = 9;
                const int travelQuestionId = 10;
                const int bankserviceQuestionId = 11;
                const int interestsQuestionId = 12;

                //creating a list of every option item from each question in database

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

                //BANK SERVICE
                List<ListItem> bankserviceItem = new List<ListItem>();
                bankserviceItem = optionList(bankserviceQuestionId);
                foreach (ListItem item in bankserviceItem)
                {
                    bankserviceList.Items.Add(item);
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

                //ADDITIONAL INTERESTS
                List<ListItem> interestItem = new List<ListItem>();
                interestItem = optionList(interestsQuestionId);
                foreach (ListItem item in interestItem)
                {
                    interestList.Items.Add(item);
                }
                try
                {
                    //Creating a connection
                    SqlConnection connection = DatabaseHelper.GetConnection();

                    //Creating a command to get correct data from DB
                    SqlCommand command = new SqlCommand("SELECT * FROM respondents", connection);

                    //Creating a reader to go through result
                    SqlDataReader reader = command.ExecuteReader();

                    //Creting a datatable to show data
                    DataTable datatable = new DataTable();

                    //creating columns for datatable
                    datatable.Columns.Add("First Name", typeof(String));
                    datatable.Columns.Add("Last Name", typeof(String));
                    datatable.Columns.Add("Date of Birth", typeof(DateTime));
                    datatable.Columns.Add("Last active", typeof(DateTime));
                    datatable.Columns.Add("Phone number", typeof(String));

                    //cycle through the result, row by row
                    while (reader.Read())
                    {
                        //Generating empty row for table
                        DataRow row = datatable.NewRow();
                        //Filling out the empty row with data result
                        row["First Name"] = reader["firstName"];
                        row["Last Name"] = reader["lastName"];
                        row["Date of Birth"] = reader["dob"];
                        row["Last Active"] = reader["date"];
                        row["Phone Number"] = reader["phone"];
                        //adding row to datatable
                        datatable.Rows.Add(row);
                    }
                    //showing data result in gridview
                    allRespGridView.DataSource = datatable;
                    allRespGridView.DataBind();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could list all respondents");
                }
                }
        }

        //function to handle the searches of repsondents
        protected void search_Click(object sender, EventArgs e)
        {
            try
            {
                //Creating a connection
                SqlConnection conn = DatabaseHelper.GetConnection();

                //how it should look = 
                //SqlCommand command = new SqlCommand("SELECT * FROM respondents WHERE respondentID IN(select respondentID from answer WHERE ");
                String start = "SELECT * FROM respondents ";
                String where = "WHERE respondentID IN(select respondentID from answer WHERE ";
                String queryOptions = "";
                bool firstCondition = true;
                int optionID;

                //cycling through every option user could have marked
                foreach (ListItem item in genderList.Items)
                {
                    if (item.Selected)
                    {
                        //creating a varibale to take care of optionID from db and to prevent SQL injections
                        optionID = Int32.Parse(item.Value);
                        //coming here if its not the first time and I need to add OR in sql command
                        if (!firstCondition)
                        {
                            queryOptions = queryOptions + " OR optionID = " + optionID;
                        }
                        //coming here first time
                        else
                        {
                            firstCondition = false;
                            queryOptions = "optionID = " + optionID;
                        }
                    }
                }

                foreach (ListItem item in ageList.Items)
                {
                    if (item.Selected)
                    {
                        //creating a varibale to take care of optionID from db and to prevent SQL injections
                        optionID = Int32.Parse(item.Value);
                        //coming here second time and after
                        if (!firstCondition)
                        {
                            queryOptions = queryOptions + " OR optionID = " + optionID;
                        }
                        //coming here first time
                        else
                        {
                            firstCondition = false;
                            queryOptions = "optionID = " + optionID;
                        }
                    }
                }

                foreach (ListItem item in banksList.Items)
                {
                    if (item.Selected)
                    {
                        //creating a varibale to take care of optionID from db and to prevent SQL injections
                        optionID = Int32.Parse(item.Value);
                        //coming here second time and after
                        if (!firstCondition)
                        {
                            queryOptions = queryOptions + " OR optionID = " + optionID;
                        }
                        //coming here first time
                        else
                        {
                            firstCondition = false;
                            queryOptions = "optionID = " + optionID;
                        }
                    }
                }

                foreach (ListItem item in stateList.Items)
                {
                    if (item.Selected)
                    {
                        //creating a varibale to take care of optionID from db and to prevent SQL injections
                        optionID = Int32.Parse(item.Value);
                        //coming here second time and after
                        if (!firstCondition)
                        {
                            queryOptions = queryOptions + " OR optionID = " + optionID;
                        }
                        //coming here first time
                        else
                        {
                            firstCondition = false;
                            queryOptions = "optionID = " + optionID;
                        }
                    }
                }

                foreach (ListItem item in bankserviceList.Items)
                {
                    if (item.Selected)
                    {
                        //creating a varibale to take care of optionID from db and to prevent SQL injections
                        optionID = Int32.Parse(item.Value);
                        //coming here second time and after
                        if (!firstCondition)
                        {
                            queryOptions = queryOptions + " OR optionID = " + optionID;
                            Console.WriteLine(queryOptions);
                        }
                        //coming here first time
                        else
                        {
                            firstCondition = false;
                            queryOptions = "optionID = " + optionID;
                        }
                    }
                }

                foreach (ListItem item in newspaperList.Items)
                {
                    if (item.Selected)
                    {
                        //creating a varibale to take care of optionID from db and to prevent SQL injections
                        optionID = Int32.Parse(item.Value);
                        //coming here second time and after
                        if (!firstCondition)
                        {
                            queryOptions = queryOptions + " OR optionID = " + optionID;
                            Console.WriteLine(queryOptions);
                        }
                        //coming here first time
                        else
                        {
                            firstCondition = false;
                            queryOptions = "optionID = " + optionID;
                        }
                    }
                }

                foreach (ListItem item in travelList.Items)
                {
                    if (item.Selected)
                    {
                        //creating a varibale to take care of optionID from db and to prevent SQL injections
                        optionID = Int32.Parse(item.Value);
                        //coming here second time and after
                        if (!firstCondition)
                        {
                            queryOptions = queryOptions + " OR optionID = " + optionID;
                            Console.WriteLine(queryOptions);
                        }
                        //coming here first time
                        else
                        {
                            firstCondition = false;
                            queryOptions = "optionID = " + optionID;
                        }
                    }
                }

                foreach (ListItem item in interestList.Items)
                {
                    if (item.Selected)
                    {
                        //creating a varibale to take care of optionID from db and to prevent SQL injections
                        optionID = Int32.Parse(item.Value);
                        //coming here second time and after
                        if (!firstCondition)
                        {
                            queryOptions = queryOptions + " OR optionID = " + optionID;
                            Console.WriteLine(queryOptions);
                        }
                        //coming here first time
                        else
                        {
                            firstCondition = false;
                            queryOptions = "optionID = " + optionID;
                        }
                    }
                }

                foreach (ListItem item in sportList.Items)
                {
                    if (item.Selected)
                    {
                        //creating a varibale to take care of optionID from db and to prevent SQL injections
                        optionID = Int32.Parse(item.Value);
                        //coming here second time and after
                        if (!firstCondition)
                        {
                            queryOptions = queryOptions + " OR optionID = " + optionID;
                            Console.WriteLine(queryOptions);
                        }
                        //coming here first time
                        else
                        {
                            firstCondition = false;
                            queryOptions = "optionID = " + optionID;
                        }
                    }
                }

                //creating the end of command
                string end = ");";
                //converting the command to a string
                string fullCommand = start + where + queryOptions + end;
                //calling the string as a command
                SqlCommand command = new SqlCommand(fullCommand, conn);
                SqlDataReader reader = command.ExecuteReader();
                //searchResultGridView = gridview ID

                //Creting a datatable to show data
                DataTable datatable = new DataTable();

                //creating columns for datatable
                datatable.Columns.Add("First Name", typeof(String));
                datatable.Columns.Add("Last Name", typeof(String));
                datatable.Columns.Add("Date of Birth", typeof(DateTime));
                datatable.Columns.Add("Last active", typeof(DateTime));
                datatable.Columns.Add("Phone number", typeof(String));

                //cycle through the result, row by row
                while (reader.Read())
                {
                    //Generating empty row for table
                    DataRow row = datatable.NewRow();
                    //Filling out the empty row with data result
                    row["First Name"] = reader["firstName"];
                    row["Last Name"] = reader["lastName"];
                    row["Date of Birth"] = reader["dob"];
                    row["Last Active"] = reader["date"];
                    row["Phone Number"] = reader["phone"];
                    //adding row to datatable
                    datatable.Rows.Add(row);
                }
                //showing data result in gridview
                searchResultGridView.DataSource = datatable;
                searchResultGridView.DataBind();

                //closing connection
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not search for respondents");
            }
        }
        //to publish all options from db
        public List<ListItem> optionList(int questionID)
        {
            try
            {
                //creating a list to put all option items in
                List<ListItem> items = new List<ListItem>();
                //getting connection from database helper class
                SqlConnection connection = DatabaseHelper.GetConnection();

                //selecting all options from database
                SqlCommand optionItemsCommand = new SqlCommand("SELECT * FROM [option] WHERE questionID = " + questionID, connection);
                
                //reading all options
                SqlDataReader optionItemsReader = optionItemsCommand.ExecuteReader();
                //cycle though option items
                while (optionItemsReader.Read())
                {
                    //creating a new list of optionitems with the values from questions in database
                    ListItem optionItem = new ListItem(optionItemsReader["value"].ToString(), optionItemsReader["optionID"].ToString());
                    //adding each option item to list
                    items.Add(optionItem);
                }
                //closing connection
                connection.Close();
                //returning all items
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
