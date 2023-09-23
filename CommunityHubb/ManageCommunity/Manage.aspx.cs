using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI.WebControls;

namespace CommunityHubb.ManageCommunity
{
    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (null == Request.QueryString["id"])
            {
                Session["fmsg"] = "Invalid community id, please try again";
                Response.Redirect("~/");
            }
            int communityId = Convert.ToInt32(Request.QueryString["id"]);

            //int communityId = 11;
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            Community community = entities.Communities.Where(c => c.Id == communityId).FirstOrDefault();
            int userId;
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to manage community";
                Response.Redirect("~/");
            }
            userId = Convert.ToInt32(Session["UserId"]);
            if (null == community)
            {
                Session["fmsg"] = "Community not found, please try again";
                Response.Redirect("~/");
            }
            if (!entities.CommunityUsers.Where(cu => cu.CommunityId == communityId && cu.UserId == userId).FirstOrDefault().IsAdmin)
            {
                Session["fmsg"] = "You are not admin to manage this community";
                Response.Redirect("~/");
            }
            List<Post> posts = community.Posts.OrderByDescending(post => post.Date).ToList();
            postsOfCommunity.DataSource = posts;
            postsOfCommunity.DataBind();
            titlebox.InnerText = community.Name;
            titlebox.HRef = "~/ManageCommunity/Communityhome?id=" + community.Id;
            commdesc.Text = community.Description;
            List<User> usersNonAdmin = community.CommunityUsers.Where(cu => !cu.IsAdmin).Select(cu => cu.User).ToList();
            List<User> userAdmin = community.CommunityUsers.Where(cu => cu.IsAdmin).Select(cu => cu.User).ToList();
            User user = entities.Users.Where(u => u.Id == userId).FirstOrDefault();
            List<User> allUsers = entities.Users.ToList();
            foreach (User u in userAdmin)
            {
                allUsers.Remove(u);
            }
            foreach (User u in usersNonAdmin)
            {
                allUsers.Remove(u);
            }
            userAdmin.Remove(user);
            usersOfCommAdmin.DataSource = userAdmin;
            usersOfCommAdmin.DataBind();
            userOfCommNonAdm.DataSource = usersNonAdmin;
            userOfCommNonAdm.DataBind();
            nonUserOfComm.DataSource = allUsers;
            nonUserOfComm.DataBind();
            navUser.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            navPost.CssClass = "btn btn-dark fs-5 w-100 m-2";
            userView.Visible = false;
            postsView.Visible = true;
        }

        protected void deletePost_Click(object sender, EventArgs e)
        {
            int postId = Convert.ToInt32(((Button)sender).CommandArgument);
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            Post post = entities.Posts.Where(p => p.Id == postId).FirstOrDefault();
            foreach (Reaction reaction in post.Reactions.ToList())
            {
                entities.Reactions.Remove(reaction);
            }
            foreach (Reply reply in post.Replies.ToList())
            {
                entities.Replies.Remove(reply);
            }
            entities.Posts.Remove(post);
            entities.SaveChanges();
            Session["smsg"] = "Post deleted successfully";
            Response.Redirect("~/ManageCommunity/Manage?id=" + post.CommunityId);
        }

        protected void deleteUser_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(((Button)sender).CommandArgument);
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            CommunityUser communityUser = entities.CommunityUsers.Where(cu => cu.UserId == userId).FirstOrDefault();
            entities.CommunityUsers.Remove(communityUser);
            entities.SaveChanges();
            Session["smsg"] = "User removed successfully";
            Response.Redirect("~/ManageCommunity/Manage?id=" + communityUser.CommunityId);

        }
        protected void removeAdmin_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(((Button)sender).CommandArgument);
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            CommunityUser communityUser = entities.CommunityUsers.Where(cu => cu.UserId == userId).FirstOrDefault();
            communityUser.IsAdmin = false;
            entities.CommunityUsers.AddOrUpdate(communityUser);
            entities.SaveChanges();
            Session["smsg"] = "Admin removed successfully";
            Response.Redirect("~/ManageCommunity/Manage?id=" + communityUser.CommunityId);
        }

        protected void addAdmin_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(((Button)sender).CommandArgument);
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            CommunityUser communityUser = entities.CommunityUsers.Where(cu => cu.UserId == userId).FirstOrDefault();
            communityUser.IsAdmin = true;
            entities.CommunityUsers.AddOrUpdate(communityUser);
            entities.SaveChanges();
            Session["smsg"] = "Admin added successfully";
            Response.Redirect("~/ManageCommunity/Manage?id=" + communityUser.CommunityId);
        }

        protected void addUser_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(((Button)sender).CommandArgument);
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            CommunityUser communityUser = new CommunityUser();
            communityUser.UserId = userId;
            communityUser.CommunityId = Convert.ToInt32(Request.QueryString["id"]);
            communityUser.IsAdmin = false;
            entities.CommunityUsers.Add(communityUser);
            entities.SaveChanges();
            Session["smsg"] = "User added successfully";
            Response.Redirect("~/ManageCommunity/Manage?id=" + communityUser.CommunityId);

        }

        protected void navPost_Click(object sender, EventArgs e)
        {
            navPost.CssClass = "btn btn-dark fs-5 w-100 m-2";
            navUser.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            userView.Visible = false;
            postsView.Visible = true;
        }

        protected void navUser_Click(object sender, EventArgs e)
        {
            navPost.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            navUser.CssClass = "btn btn-dark fs-5 w-100 m-2";
            userView.Visible = true;
            postsView.Visible = false;
        }
    }
}