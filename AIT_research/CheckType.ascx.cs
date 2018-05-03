using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIT_research
{
    public partial class CheckType : System.Web.UI.UserControl
    {
        //making public properties to access private attributes
        public Label QuestionLabel
        {
            get { return questionLabel; }
            set { questionLabel = value; }
        }

        public CheckBoxList CheckList
        {
            get { return checkList; }
            set { checkList = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}