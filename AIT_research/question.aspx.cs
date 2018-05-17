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

                if (questionType.Equals("textbox"))
                {
                    //its a textbox question
                    TextType textTypeQuestion = (TextType)LoadControl("~/TextType.ascx");
                    textTypeQuestion.ID = "textboxQuestionControl";
                    textTypeQuestion.QuestionLabel.Text = questionText;

                    //adding it to the placeholder in my webpage
                    questionPlaceholder.Controls.Add(textTypeQuestion);
                }
                else if (questionType.Equals("checkbox"))
                {
                    //its a checkbox question
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
                else if (questionType.Equals("radio"))
                {
                    //its a radio button question
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
            //lets try find checkbox question contorl in web page
            CheckType checkTypeQuestion = (CheckType)questionPlaceholder.FindControl("checkTypeQuestion");
            if (checkTypeQuestion != null)
            {
                //then its a checkbox question

                //empty list of shown answers in bullet list
                selectedAnswerBulletedList.Items.Clear();
                foreach (ListItem item in checkTypeQuestion.CheckList.Items)
                {
                    if (item.Selected)
                    {
                        //TODO add answer to session or DB
                        //TODO check if selected answers lead onto FOLLOW UP questions, if so, add to the list

                        selectedAnswerBulletedList.Items.Add(item);
                    }
                }
            }

            //ok lets now try if its the radiotype question
            RadioType radioTypeQuestion = (RadioType)questionPlaceholder.FindControl("radioTypeQuestion");
            if (radioTypeQuestion != null)
            {
                //then its a radio question

                //empty list of shown answers in bullet list
                selectedAnswerBulletedList.Items.Clear();
                foreach (ListItem item in radioTypeQuestion.RadioList.Items)
                {
                    if (item.Selected)
                    {
                        //TODO add answer to session or DB
                        //TODO check if selected answers lead onto FOLLOW UP questions, if so, add to the list

                        selectedAnswerBulletedList.Items.Add(item);
                    }
                }
            //empty list 
            }
            //ok lets now try if its the texttype question
            TextType textTypeQuestion = (TextType)questionPlaceholder.FindControl("textboxQuestionControl");
            if (textTypeQuestion != null)
            {
                //to do add this answer to db or session

                //empty list of shown answers in bullet list
                selectedAnswerBulletedList.Items.Clear();

                //add 1 item to show what we typed in
                selectedAnswerBulletedList.Items.Add(new ListItem(textTypeQuestion.TextboxInputQuestion.Text, ""));
            }

            //creating a variable to check what current question is
            int currentQuestion = GetCurrentQuestionNumber();

            //getting connection from database helper class
            SqlConnection connection = DatabaseHelper.GetConnection();

            //TODO check for follow up questions by checking selected answers against
            //db to see if there are any follow up questions setup or load up a list
            //of follow up questions we need to complete
            List<Int32> followUpQuestions = new List<int>();

            //if list of follow up questions exists then use it instead of the empty list 
            if (HttpContext.Current.Session["followUpQuestions"] != null)
            {
                followUpQuestions = (List<Int32>)HttpContext.Current.Session["followUpQuestions"];
            }

            //FOR TESTING ONLY, faking adding follow up qustions. aka dont do this in final assignment
            if (currentQuestion == 1) //hardcoded checks loses marks in assignment
            {
                //add your follow up question ids to the list
                followUpQuestions.Add(3);
                followUpQuestions.Add(4);
            }

            //find out what the next questions should be:
            //get current question from DB, there is a column on this table with a foreign key to the next question
            SqlCommand command = new SqlCommand("SELECT * FROM question where questionID = " + currentQuestion, connection);
            SqlDataReader reader = command.ExecuteReader();

            //loop through reasults. should only be 1
            while (reader.Read())
            {
                //if at the end of the survey, next question foreign key column will be NULL, so I'm checking for NULLS first
                //first, get index of nextQuestion column
                int nextQuestionColumnIndex = reader.GetOrdinal("nextQuestionID");
                //use this index to check if column is null
                if (reader.IsDBNull(nextQuestionColumnIndex))
                {
                    //if it comes here its the end of survey
                    //to do redirect to registrations page or something finalising the survey
                }
                else
                {
                    //not end of survey!
                    int nextQuestion = (int)reader["nextQuestionID"];

                    //set this as the current question in the session
                    HttpContext.Current.Session["questionNumber"] = nextQuestion;

                    //IF THERE IS FOLLOW UP QUESTIONS THOUGH, do them first.
                    if (followUpQuestions.Count > 0)
                    {
                        //set current question to first follow up question, then remove from follow up question list
                        // - so it doesnt repeat for next time
                        HttpContext.Current.Session["questionID"] = followUpQuestions[0];
                        followUpQuestions.RemoveAt(0);
                    }
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