using CommunityHubb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommunityHubb.ManagePost
{
    public partial class SelectCommunity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Session["fmsg"] = "Please login to create a post";
                Response.Redirect("/");
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            User user = db.Users.Find(userId);
            if(user == null)
            {
                Session["fmsg"] = "User not found, please try again";
                Response.Redirect("/");
            }
            userCommunities.DataSource = user.CommunityUsers.Select(cu => cu.Community).ToList();
            userCommunities.DataBind();
        }

    }
}