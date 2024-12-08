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

        }

        private float price = 10.0f; // The base price for the pizza (no toppings) is $ 10.00
        private string toppings = "item=pizza,10.0";

        protected void btnMakeIt_Click(object sender, EventArgs e)
        {
            Response.Redirect("Page3.aspx?" + toppings);
        }

        protected void cblTopping_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in cblTopping.Items)
            {
                if (item.Selected)
                {
                    toppings += "&item=" + item.Text + "," + item.Value;
                    price += Convert.ToSingle(item.Value);
                }
            }
            lblPrice.Text = "Price: " + price;
        }
    }
}