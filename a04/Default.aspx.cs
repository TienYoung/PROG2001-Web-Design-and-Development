/*
 * FILE          : Default.aspx.cs
 * PROJECT       : PROG2001 - Assignment #4
 * PROGRAMMER    : Tian Yang
 * FIRST VERSION : 2024-11-05
 * DESCRIPTION   : 
 *   This file serves as the code-behind for the Default.aspx page. It handles user input to 
 *   capture the username and validates the form before redirecting to SetMax.aspx.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace a04
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Session["UserName"] = txtUserName.Text;
                Response.Redirect("SetMax.aspx");
            }
        }
    }
}