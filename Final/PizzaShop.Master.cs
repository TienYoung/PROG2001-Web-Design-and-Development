using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Final
{
    public partial class PizzaShop : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            string username = Session["Username"] as string;
            if (String.IsNullOrEmpty(username)) return;

            string action = Request.QueryString["action"];
            if (String.IsNullOrEmpty(action))
            {
                hdgGreeting.InnerText = "Ciao " + username.Split(' ')[0];
            }
            else
            {
                hdgGreeting.InnerText = "Thank you, " + username + ". Your order has been " + action + "ed!";
            }
        }
    }
}