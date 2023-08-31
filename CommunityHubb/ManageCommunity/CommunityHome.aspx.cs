using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunityHubb.ManageCommunity
{
    public partial class CommunityHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Session["fmsg"] = "Invalid Community Id";
                Response.Redirect("~/Default");
            }
            int communityId = Convert.ToInt32(Request.QueryString["id"]);
            CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubb.CommunityHubbDBEntities();
            Community community = communityHubbDBEntities.Communities.Where(c => c.Id == communityId).FirstOrDefault();
            if (community == null)
            {
                Session["fmsg"] = "Community not found";
                Response.Redirect("~/Default");
            }
            if (community.isPrivate)
            {
                List<User> users = new List<User>();
                users = community.CommunityUsers.Select(cu => cu.User).ToList();
                if (!users.Contains((User)Session["UserId"]))
                {
                    Session["fmsg"] = "You are not a member of this private community";
                    Response.Redirect("~/Default");
                }
            }
            titlebox.Text = community.Name;
            commdesc.Text = community.Description;
            if(null == Session["UserId"])
            {
                followbtn.Text = "Login to follow";
            }
            else
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                CommunityUser communityUser = community.CommunityUsers.Where(cu => cu.UserId == userId).FirstOrDefault();
                if (communityUser != null)
                {
                    followbtn.Text = "Unfollow Community";
                }
                else
                {
                    followbtn.Text = "Follow Community";
                }
            }
        }
    }
}