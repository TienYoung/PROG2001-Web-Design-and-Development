/*
 * FILE          : Win.aspx.cs
 * PROJECT       : PROG2001 - Assignment #4
 * PROGRAMMER    : Tian Yang
 * FIRST VERSION : 2024-11-05
 * DESCRIPTION   : 
 *   This file is the code-behind for the Win.aspx page, which displays the result when a user
 *   successfully guesses the number. It also resets the session state to allow a new game 
 *   to be started.
 */
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

        // Clear session variables.
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Session.Remove("RandomNumber");
            Session.Remove("MinNumber");
            Session.Remove("MaxNumber");
            Response.Redirect("SetMax.aspx");
        }
    }
}