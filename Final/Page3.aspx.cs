using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final
{
    public partial class Page3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] items = Request.QueryString.GetValues("item");
            foreach (string topping in items)
            {
                string name = topping.Split(',')[0];
                string price = topping.Split(',')[1];
                Response.Write($"Name: {name}, Price: {price}");
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Response.Redirect("Page4.aspx?action=confirm");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Page4.aspx?action=cancel");
        }
    }
}