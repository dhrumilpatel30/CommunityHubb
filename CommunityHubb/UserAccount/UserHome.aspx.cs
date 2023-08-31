﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunityHubb.UserAccount
{
    public partial class UserHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (null == Request.QueryString["id"])
            {
                Session["fmsg"] = "Invalid User Id";
                Response.Redirect("~/Default");
            }
            int userId = Convert.ToInt32(Request.QueryString["id"]);
            CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubb.CommunityHubbDBEntities();
            User user = communityHubbDBEntities.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null)
            {
                Session["fmsg"] = "User not found";
                Response.Redirect("~/Default");
            }
            titlebox.Text = user.Name;
            commdesc.Text = user.About;

        }
    }
}