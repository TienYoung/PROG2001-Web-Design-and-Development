using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace a04
{
    public partial class Guess : System.Web.UI.Page
    {
        protected int Min { get => Convert.ToInt32(Session["MinNumber"]); }
        protected int Max { get => Convert.ToInt32(Session["MaxNumber"]); }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int random = Convert.ToInt32(Session["RandomNumber"]);
                int guess = Convert.ToInt32(txtGuessNumber.Text);
                if (guess < random)
                { 
                    Session["MinNumber"] = guess + 1;
                }
                else if (guess > random)
                {
                    Session["MaxNumber"] = guess - 1;
                }
                else
                {
                    Response.Redirect("Win.aspx");
                }
                Response.Redirect("Guess.aspx");
            }
        }
    }
}