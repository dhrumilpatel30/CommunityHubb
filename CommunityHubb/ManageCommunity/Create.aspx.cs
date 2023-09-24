using CommunityHubb.Models;
using System;

namespace CommunityHubb.ManageCommunity
{
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Session["fmsg"] = "Please login to create community";
                Response.Redirect("~/");
            }
        }

        protected void CreateClick(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            String name = namebox.Text;
            String description = aboutbox.Text;
            String visibility = visibilityChoice.SelectedValue;
            Community community = new Community();
            community.Name = name;
            community.Description = description;
            community.CreatedAt = DateTime.Now;
            community.IsPublic = visibility.Equals("Public");
            community.OwerId = userId;
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            db.Communities.Add(community);
            db.SaveChanges();
            CommunityUser communityUser = new CommunityUser();
            communityUser.CommunityId = community.Id;
            communityUser.UserId = userId;
            communityUser.IsAdmin = true;
            db.CommunityUsers.Add(communityUser);
            db.SaveChanges();
            Response.Redirect("~/ManageCommunity/Home.aspx?id=" + community.Id);
        }
    }
}