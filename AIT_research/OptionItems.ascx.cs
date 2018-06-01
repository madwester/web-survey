using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIT_research
{
    public partial class OptionItems : System.Web.UI.UserControl
    {
        public RadioButtonList OptionList
        {
            get { return optionItemListID; }
            set { optionItemListID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}