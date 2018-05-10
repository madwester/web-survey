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
            RadioType radioTypeQuestion = (RadioType)questionPlaceholder.FindControl("radioTypeQuestion");
            if (radioTypeQuestion != null)
            {
                //then its a radio question

            //empty list 
            }
        }
    }
}