﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIT_research
{
    public partial class RadioType : System.Web.UI.UserControl
    {
        //function to make public properties to access private attributes

        //preparing to get question label from db
        public Label QuestionLabel
        {
            get { return questionLabel; }
            set { questionLabel = value; }
        }
        //preparing to get question options from db
        public RadioButtonList RadioList
        {
            get { return questionRadioList; }
            set { questionRadioList = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}