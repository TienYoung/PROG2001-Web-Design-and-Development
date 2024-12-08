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
        private float totalPrice = 10.0f; // The base price for the pizza (no toppings) is $ 10.00
        private string toppings = "item=Pizza (with sauce and cheese),10.0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            ViewState["toppings"] = toppings;
            lblPrice.Text = "TOTAL: $ " + totalPrice.ToString("F2");
        }

        protected void btnMakeIt_Click(object sender, EventArgs e)
        {
            Response.Redirect("Page3.aspx?" + ViewState["toppings"]);
        }

        protected void cblTopping_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["toppings"] = toppings;
            foreach (ListItem item in cblTopping.Items)
            {
                if (item.Selected)
                {
                    float price = 0.0f;
                    switch (item.Value)
                    {
                        case "Pepperoni":
                            price = 1.5f;
                            break;
                        case "Mushrooms":
                            price = 1.0f;
                            break;
                        case "Green Olives":
                            price = 1.0f;
                            break;
                        case "Green Peppers":
                            price = 1.0f;
                            break;
                        case "Double Cheese":
                            price = 2.25f;
                            break;
                    }

                    ViewState["toppings"] += "&item=" + item.Value + "," + price.ToString("F2");
                    totalPrice += price;
                }
            }
            lblPrice.Text = "TOTAL: $ " + totalPrice.ToString("F2");
        }
    }
}