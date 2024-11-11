using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace a04
{
    public partial class Win : System.Web.UI.Page
    {
        protected string UserName => Session["UserName"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.DataBind();
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Session.Remove("RandomNumber");
            Session.Remove("MinNumber");
            Session.Remove("MaxNumber");
            Response.Redirect("SetMax.aspx");
        }
    }
}