using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIT_research
{
    public partial class thankyou : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void returnBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}