using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIT_research
{
    //this is the structure of an answer before saves in session/db
    public class Answer
    {
        public int questionID;
        public string answerText;
        public int optionID;
    }
}