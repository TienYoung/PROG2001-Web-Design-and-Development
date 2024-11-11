using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace a04
{
    public partial class SetGame : System.Web.UI.Page
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
            if (Page.IsValid)
            {
                int max = Convert.ToInt32(txtMaxNumber.Text);
                Session["MinNumber"] = 1;
                Session["MaxNumber"] = max;
                Random rand = new Random();
                Session["RandomNumber"] = rand.Next(1, max);
                Response.Redirect("Guess.aspx");
            }
        }
    }
}