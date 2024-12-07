using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Final
{
    public partial class Page2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = Session["Username"] as string;

                if (!String.IsNullOrEmpty(username))
                {
                    HtmlGenericControl hdgWelcome = this.Master.FindControl("hdgWelcome") as HtmlGenericControl;
                    if (hdgWelcome != null)
                    {
                        hdgWelcome.InnerText = "Welcome, " + username;
                    }
                }
            }
        }
    }
}