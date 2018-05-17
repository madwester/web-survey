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
    public partial class question : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //creating a variable to check what current question is
            int currentQuestion = GetCurrentQuestionNumber();

            //getting connection from database helper class
            SqlConnection connection = DatabaseHelper.GetConnection();

            //building a sql command to get current question
            SqlCommand command = new SqlCommand("SELECT * FROM question, type WHERE question.typeID = type.typeID AND question.questionID = " + currentQuestion, connection);

            //run command, store results in SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            //loop through rows of results
            while (reader.Read())
            {
                //get question text and type from this row of results
                string questionText = reader["text"].ToString();
                string questionType = reader["name"].ToString().ToLower(); //checkbox, textbox, radio

                //If its a textbox question
                if (questionType.Equals("textbox"))
                {
                    TextType textTypeQuestion = (TextType)LoadControl("~/TextType.ascx");
                    textTypeQuestion.ID = "textboxQuestionControl";
                    textTypeQuestion.QuestionLabel.Text = questionText;

                    //adding it to the placeholder in my webpage
                    questionPlaceholder.Controls.Add(textTypeQuestion);
                }
                //If its a checkbox question
                else if (questionType.Equals("checkbox"))
                {
                    CheckType checkTypeQuestion = (CheckType)LoadControl("~/CheckType.ascx");
                    checkTypeQuestion.ID = "checkboxQuestionControl";
                    checkTypeQuestion.QuestionLabel.Text = questionText;

                    //need a new command to get all options belonging to question
                    SqlCommand optionCommand = new SqlCommand(
                        "SELECT * FROM [option] WHERE questionID = " + currentQuestion, connection);
                    SqlDataReader optionReader = optionCommand.ExecuteReader();

                    //cycle through rows of options
                    while (optionReader.Read())
                    {
                        //for every row, I am building a list item representing it
                        ListItem item = new ListItem(optionReader["value"].ToString(), optionReader["optionID"].ToString());

                        //add item to my checkbox list
                        checkTypeQuestion.CheckList.Items.Add(item);
                    }
                    questionPlaceholder.Controls.Add(checkTypeQuestion);
                }
                //If its a radio button question
                else if (questionType.Equals("radio"))
                {
                    RadioType radioTypeQuestion = (RadioType)LoadControl("~/RadioType.ascx");
                    radioTypeQuestion.ID = "radioboxQuestionControl";
                    radioTypeQuestion.QuestionLabel.Text = questionText;

                    //need a new command to get all options belonging to question
                    SqlCommand optionCommand = new SqlCommand(
                        "SELECT * FROM [option] WHERE questionID = " + currentQuestion, connection);
                    SqlDataReader optionReader = optionCommand.ExecuteReader();

                    while (optionReader.Read())
                    {
                        ListItem item = new ListItem(optionReader["value"].ToString(), optionReader["optionID"].ToString());

                        //add item to my radio list
                        radioTypeQuestion.RadioList.Items.Add(item);
                    }
                    questionPlaceholder.Controls.Add(radioTypeQuestion);
                }
            }
            connection.Close();
        }
        private static int GetCurrentQuestionNumber() {
            //ask db for lowest number instead of default
            int currentQuestion = 1; //to do dont use default values, ask DB for first question
            if (HttpContext.Current.Session["questionID"] != null)
            {
                currentQuestion = (int)HttpContext.Current.Session["questionID"];
            }
            else
            {
                HttpContext.Current.Session["questionID"] = currentQuestion;
            }
            return currentQuestion;
        }
        protected void nextBtn_Click(object sender, EventArgs e)
        {
            //Creating a new list to store follow up questions
            List<Int32> followUpQuestions = new List<int>();

            //If list of follow up questions exists then use it instead of the empty list 
            if (HttpContext.Current.Session["followUpQuestions"] != null)
            {
                followUpQuestions = (List<Int32>)HttpContext.Current.Session["followUpQuestions"];
            }

            //Getting connection from database helper class
            SqlConnection connection = DatabaseHelper.GetConnection();

            //lets try find checkbox question contol in web page
            CheckType checkTypeQuestion = (CheckType)questionPlaceholder.FindControl("checkboxQuestionControl");
            if (checkTypeQuestion != null)
            {
                //then its a checkbox question
         
                foreach (ListItem item in checkTypeQuestion.CheckList.Items)
                {
                    if (item.Selected)
                    {
                        int optionID = Int32.Parse(item.Value);
                        //TODO add answer to session or DB

                        //Checking what the next question depending on answer is
                        SqlCommand nextQuestionCommand = new SqlCommand("SELECT * FROM [option] WHERE optionID = "
                            + optionID + " AND nextQuestion IS NOT NULL ", connection);
                        SqlDataReader nextQuestionReader = nextQuestionCommand.ExecuteReader();
                        while (nextQuestionReader.Read())
                        {
                            int nextQuestionItem = (int)nextQuestionReader["nextQuestion"];
                            followUpQuestions.Add(nextQuestionItem);
                        }
                    }
                }
            }

            //ok lets now try if its the radiotype question
            RadioType radioTypeQuestion = (RadioType)questionPlaceholder.FindControl("radioTypeQuestion");
            if (radioTypeQuestion != null)
            {
                //then its a radio question

                foreach (ListItem item in radioTypeQuestion.RadioList.Items)
                {
                    if (item.Selected)
                    {
                        //TODO add answer to session or DB
                        //TODO check if selected answers lead onto FOLLOW UP questions, if so, add to the list

                    }
                }
            //empty list 
            }
            //ok lets now try if its the texttype question
            TextType textTypeQuestion = (TextType)questionPlaceholder.FindControl("textboxQuestionControl");
            if (textTypeQuestion != null)
            {
                //to do add this answer to db or session
            }

            //creating a variable to check what current question is
            int currentQuestion = GetCurrentQuestionNumber();
            
            //FOR TESTING ONLY, faking adding follow up qustions. aka dont do this in final assignment
            if (currentQuestion == 1) //hardcoded checks loses marks in assignment
            {
                //add your follow up question ids to the list
                //followUpQuestions.Add(3);
                //followUpQuestions.Add(4);
            }

            //Finding out what the next question is
            SqlCommand command = new SqlCommand("SELECT * FROM question where questionID = " + currentQuestion, connection);
            SqlDataReader reader = command.ExecuteReader();

            //Looping through result
            while (reader.Read())
            {
                //if at the end of the survey, next question foreign key column will be NULL, so I'm checking for NULLS first
                //first, get index of nextQuestion column
                int nextQuestionColumnIndex = reader.GetOrdinal("nextQuestionID");
                //use this index to check if column is null

                //IF THERE IS FOLLOW UP QUESTIONS THOUGH, do them first.
                if (followUpQuestions.Count > 0)
                {
                    //set current question to first follow up question, then remove from follow up question list
                    // - so it doesnt repeat for next time
                    HttpContext.Current.Session["questionID"] = followUpQuestions[0];
                    followUpQuestions.RemoveAt(0);
                    HttpContext.Current.Session["followUpQuestions"] = followUpQuestions;

                    //redirect to same page to load up the next question as current question (aka, run pageLoad again)
                    Response.Redirect("question.aspx");
                }

                //IF ITS THE END OF SURVEY
                else if (reader.IsDBNull(nextQuestionColumnIndex))
                {
                    //redirect to register page
                    Response.Redirect("Register.aspx");
                }
                
                //If there are not follow questions
                else
                {
                    //not end of survey!
                    int nextQuestion = (int)reader["nextQuestionID"];

                    //set this as the current question in the session
                    HttpContext.Current.Session["questionID"] = nextQuestion;
                    
                    //store the follow up questions list in session (just in case it changed)
                    HttpContext.Current.Session["followUpQuestions"] = followUpQuestions;

                    //redirect to same page to load up the next question as current question (aka, run pageLoad again)
                    Response.Redirect("question.aspx");
                }
            }
            connection.Close();
        }
    }
}