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
        private class Item
        {
            public string Name { get; set; }
            public string Price { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            float totalPrice = 0.0f;
            string[] itemStrings = Request.QueryString.GetValues("item");
            List<Item> items = new List<Item>();
            foreach (string topping in itemStrings)
            {
                string name = topping.Split(',')[0];
                string price = topping.Split(',')[1];
                items.Add(new Item { Name = name, Price = price });

                totalPrice += Convert.ToSingle(price);
            }
            rptItemList.DataSource = items;
            rptItemList.DataBind();

            lblTotalPrice.Text = "TOTAL: $ " + totalPrice.ToString("F2");
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