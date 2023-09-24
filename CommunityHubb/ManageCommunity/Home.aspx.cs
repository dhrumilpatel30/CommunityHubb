using CommunityHubb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace CommunityHubb.ManageCommunity
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Session["fmsg"] = "Invalid community id, please try again";
                Response.Redirect("~/");
            }
            int communityId = Convert.ToInt32(Request.QueryString["id"]);
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            Community community = db.Communities.Where(c => c.Id == communityId).FirstOrDefault();
            if (community == null)
            {
                Session["fmsg"] = "Community not found, please try again";
                Response.Redirect("~/");
            }
            if (!community.IsPublic)
            {
                List<User> users = community.CommunityUsers.Select(cu => cu.User).ToList();
                int userId = Convert.ToInt32(Session["UserId"]);
                if (!users.Any(u => u.Id == userId))
                {
                    Session["fmsg"] = "You are not a member of this private community, ask admin to add you";
                    Response.Redirect("~/");
                }
            }
            titlebox.Text = community.Name;
            commdesc.Text = community.Description;
            if (null == Session["UserId"])
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
            generateMembersList(community);
            postlist.DataSource = community.Posts.ToList();
            postlist.DataBind();
            createButton.HRef = "~/ManagePost/Create.aspx?id=" + communityId;
        }

        protected void ToggleFollow(object sender, EventArgs e)
        {
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to follow community";
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                int comId = Convert.ToInt32(Request.QueryString["id"]);
                CommunityHubbDBEntities db = new CommunityHubbDBEntities();
                Community community = db.Communities.Where(c => c.Id == comId).FirstOrDefault();

                int userId = Convert.ToInt32(Session["UserId"]);
                CommunityUser communityUser = community.CommunityUsers.Where(cu => cu.UserId == userId).FirstOrDefault();
                if (communityUser != null)
                {
                    community.CommunityUsers.Remove(communityUser);
                    CommunityUser communityUserInDB = db.CommunityUsers.Find(communityUser.Id);
                    db.CommunityUsers.Remove(communityUserInDB);
                    db.SaveChanges();
                    followbtn.Text = "Follow Community";
                }
                else
                {
                    CommunityUser newCommunityUser = new CommunityUser
                    {
                        UserId = userId,
                        CommunityId = community.Id,
                        IsAdmin = false
                    };
                    db.CommunityUsers.Add(newCommunityUser);
                    db.SaveChanges();
                    User user = db.Users.Find(userId);
                    newCommunityUser.User = user;
                    community.CommunityUsers.Add(newCommunityUser);
                    followbtn.Text = "Unfollow Community";
                }
                generateMembersList(community);
            }
        }
        protected void generateMembersList(Community community)
        {
            List<User> members = new List<User>();
            foreach (CommunityUser communityUser in community.CommunityUsers)
            {
                User user = communityUser.User;
                if (communityUser.User == community.User)
                {
                    user.Name = user.Name + " (Owner)";
                }
                else if (communityUser.IsAdmin)
                {
                    user.Name = user.Name + " (Admin)";
                }
                members.Add(user);
            }
            userlist.DataSource = members;
            userlist.DataBind();
        }
    }
}