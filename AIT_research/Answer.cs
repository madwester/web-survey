using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIT_research
{
    //structure of an answer before saves in session/db
    public class Answer
    {
        //public int answerID;
        public int questionID;
        //public int respondentID;
        public string answerText;
        public int optionID;
    }
}