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

            try
            {
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
                        //creating an instance of checktype function
                        CheckType checkTypeQuestion = (CheckType)LoadControl("~/CheckType.ascx");
                        //setting the id
                        checkTypeQuestion.ID = "checkboxQuestionControl";
                        //setting the text of label
                        checkTypeQuestion.QuestionLabel.Text = questionText;

                        //need a new command to get all options belonging to question
                        SqlCommand optionCommand = new SqlCommand(
                            "SELECT * FROM [option] WHERE questionID = " + currentQuestion, connection);
                        //reading the command I just created
                        SqlDataReader optionReader = optionCommand.ExecuteReader();

                        //cycle through rows of options
                        while (optionReader.Read())
                        {
                            //for every row, I am building a list item representing it
                            ListItem item = new ListItem(optionReader["value"].ToString(), optionReader["optionID"].ToString());

                            //add item to my checkbox list
                            checkTypeQuestion.CheckList.Items.Add(item);
                        }
                        //adding checktype question to placeholder
                        questionPlaceholder.Controls.Add(checkTypeQuestion);
                    }

                    //If its a radio button question
                    else if (questionType.Equals("radio"))
                    {   
                        //creating an instance of radiotype function
                        RadioType radioTypeQuestion = (RadioType)LoadControl("~/RadioType.ascx");
                        //setting the id
                        radioTypeQuestion.ID = "radioboxQuestionControl";
                        //setting the text of label
                        radioTypeQuestion.QuestionLabel.Text = questionText;

                        //need a new command to get all options belonging to question
                        SqlCommand optionCommand = new SqlCommand(
                            "SELECT * FROM [option] WHERE questionID = " + currentQuestion, connection);
                        //reading the command I just created
                        SqlDataReader optionReader = optionCommand.ExecuteReader();

                        //cycle through rows of options
                        while (optionReader.Read())
                        {
                            //for every row, I am building a list item representing it
                            ListItem item = new ListItem(optionReader["value"].ToString(), optionReader["optionID"].ToString());

                            //add item to my radio list
                            radioTypeQuestion.RadioList.Items.Add(item);
                        }
                        //adding radio type question to placeholder
                        questionPlaceholder.Controls.Add(radioTypeQuestion);
                    }
                }
                //closing connection to db
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not read question");
            }
            
        }

        private static int GetCurrentQuestionNumber() {
            try
            {
                //asking db for the lowest number to set current question
                SqlConnection connection = DatabaseHelper.GetConnection();

                //command to get the first question from database
                SqlCommand getFirstQuestion = new SqlCommand("SELECT min(questionID) FROM question", connection);
            
                //read from the command I just created
                SqlDataReader reader = getFirstQuestion.ExecuteReader();
                reader.Read();
                //passing the current question id to a string
                int currentQuestion = Int32.Parse(reader.GetValue(0).ToString());

                //closing the connection to db
                connection.Close();
                //figuring out if current question exist in session or not
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
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't find current question number");
                return 0;
            }
            
        }

        protected void nextBtn_Click(object sender, EventArgs e)
        {
            //Creating a new list to store follow up questions
            List<Int32> followUpQuestions = new List<int>();

            //Getting the answer list from session
            List<Answer> answers = getListOfAnswerFromSession();

            try
            {
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

                            //creating a new answer based on answer class
                            Answer a = new Answer();
                            a.questionID = GetCurrentQuestionNumber();
                            a.optionID = optionID;

                            //adding answers to session list
                            answers.Add(a);

                            //Creating a command to check what the next question depending on answer is
                            SqlCommand nextQuestionCommand = new SqlCommand("SELECT * FROM [option] WHERE optionID = "
                                + optionID + " AND nextQuestion IS NOT NULL ", connection);
                            //reding the command I just created
                            SqlDataReader nextQuestionReader = nextQuestionCommand.ExecuteReader();
                            //cycle though next questions
                            while (nextQuestionReader.Read())
                            {
                                int nextQuestionItem = (int)nextQuestionReader["nextQuestion"];

                                //if follow up questions doesnt already exist, add it
                                if (!followUpQuestions.Contains(nextQuestionItem))
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

                    //cycle though radiotype questions
                    foreach (ListItem item in radioTypeQuestion.RadioList.Items)
                    {
                        //creating an option id of the value of item
                        int optionID = Int32.Parse(item.Value);

                        if (item.Selected)
                        {
                            //creating a new answer based on answer class
                            Answer a = new Answer();
                            a.questionID = GetCurrentQuestionNumber();
                            a.optionID = optionID;
                            //adding answers to session list
                            answers.Add(a);
                        }
                    }
                }

                //ok lets now try if its the texttype question
                TextType textTypeQuestion = (TextType)questionPlaceholder.FindControl("textboxQuestionControl");
                if (textTypeQuestion != null)
                {
                    //then its a text type question

                    //adding answer to db
                    Answer a = new Answer();
                    a.questionID = GetCurrentQuestionNumber();
                    //getting value from textbox
                    a.answerText = textTypeQuestion.TextboxInputQuestion.Text;
                    answers.Add(a);
                }
                //creating a variable to check what current question is
                int currentQuestion = GetCurrentQuestionNumber();

                //Finding out what the next question is
                SqlCommand command = new SqlCommand("SELECT * FROM question where questionID = " + currentQuestion, connection);
                SqlDataReader reader = command.ExecuteReader();

                //save to session list 
                HttpContext.Current.Session["answers"] = answers;

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

                        //in registration page or end of survey save sesion list to db
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
                //closing the connection to db
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't read next question");
            }
        }

        public static List<Answer> getListOfAnswerFromSession()
        {
            try
            {
                //creating an empty list first
                List<Answer> answers = new List<Answer>();

                //if stored in session, get it
                if (HttpContext.Current.Session["answers"] != null)
                {
                    answers = (List<Answer>)HttpContext.Current.Session["answers"];
                }
                return answers;
            }
            catch (Exception ex)
            {
                Console.WriteLine("System could not get list of answer from session.");
                return null;
            }
        }
    }
}