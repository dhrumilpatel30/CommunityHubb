using CommunityHubb.Models;
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
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            Community community = db.Communities.Where(c => c.Id == communityId).FirstOrDefault();
            if (null == community)
            {
                Session["fmsg"] = "Community not found, please try again";
                Response.Redirect("~/");
            }
            int userId;
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to manage community";
                Response.Redirect("~/");
            }
            userId = Convert.ToInt32(Session["UserId"]);
            User user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user.Communities.Count(c => c.Id == communityId) == 0 && 
                user.CommunityUsers.Where(cu => cu.IsAdmin && cu.CommunityId == communityId).Count() == 0)
            {
                Session["fmsg"] = "You are not admin to manage this community";
                Response.Redirect("~/");
            }
            List<Post> posts = community.Posts.OrderByDescending(post => post.CreatedAt).ToList();
            postsOfCommunity.DataSource = posts;
            postsOfCommunity.DataBind();
            titlebox.InnerText = community.Name;
            titlebox.HRef = "~/ManageCommunity/Home?id=" + community.Id;
            commdesc.Text = community.Description;
            List<User> usersNonAdmin = community.CommunityUsers.Where(cu => !cu.IsAdmin).Select(cu => cu.User).ToList();
            List<User> userAdmin = community.CommunityUsers.Where(cu => cu.IsAdmin).Select(cu => cu.User).ToList();
            List<User> allUsers = db.Users.ToList();
            foreach (User u in userAdmin)
            {
                allUsers.Remove(u);
            }
            foreach (User u in usersNonAdmin)
            {
                allUsers.Remove(u);
            }
            userAdmin.Remove(community.User);
            if(user != community.User)
            {
                userAdmin.Remove(user);
            }
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

        protected void DeletePost_Click(object sender, EventArgs e)
        {
            int postId = Convert.ToInt32(((Button)sender).CommandArgument);
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            Post post = db.Posts.Where(p => p.Id == postId).FirstOrDefault();
            foreach (ReactionPost reaction in post.ReactionPosts.ToList())
            {
                db.ReactionPosts.Remove(reaction);
            }
            foreach (Reply reply in post.Replies.ToList())
            {
                foreach (ReactionReply reaction in reply.ReactionReplies.ToList())
                {
                    db.ReactionReplies.Remove(reaction);
                }
                db.Replies.Remove(reply);
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            Session["smsg"] = "Post deleted successfully";
            Response.Redirect("~/ManageCommunity/Manage?id=" + post.CommunityId);
        }

        protected void DeleteUser_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(((Button)sender).CommandArgument);
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            CommunityUser communityUser = entities.CommunityUsers.Where(cu => cu.UserId == userId).FirstOrDefault();
            entities.CommunityUsers.Remove(communityUser);
            entities.SaveChanges();
            Session["smsg"] = "User removed successfully";
            Response.Redirect("~/ManageCommunity/Manage?id=" + communityUser.CommunityId);

        }
        protected void RemoveAdmin_Click(object sender, EventArgs e)
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

        protected void AddAdmin_Click(object sender, EventArgs e)
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

        protected void AddUser_Click(object sender, EventArgs e)
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

        protected void NavPost_Click(object sender, EventArgs e)
        {
            navPost.CssClass = "btn btn-dark fs-5 w-100 m-2";
            navUser.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            userView.Visible = false;
            postsView.Visible = true;
        }

        protected void NavUser_Click(object sender, EventArgs e)
        {
            navPost.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            navUser.CssClass = "btn btn-dark fs-5 w-100 m-2";
            userView.Visible = true;
            postsView.Visible = false;
        }
    }
}