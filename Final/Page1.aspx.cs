using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Final
{
    public partial class Page1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            HtmlGenericControl hdgGreeting = this.Master.FindControl("hdgGreeting") as HtmlGenericControl;
            if (hdgGreeting == null) return;
            hdgGreeting.Visible = false;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Session["UserName"] = txtUserName.Text;
                Response.Redirect("Page2.aspx");
            }
        }
    }
}