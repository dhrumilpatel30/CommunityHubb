using CommunityHubb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace CommunityHubb.Account
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = 0;
            if (null == Request.QueryString["id"])
            {
                Session["fmsg"] = "Invalid user id, please try again";
                Response.Redirect("~/");
            }
            else
            {
                userId = Convert.ToInt32(Request.QueryString["id"]);
                if(null != Session["UserId"])
                {
                    if (Convert.ToInt32(Session["UserId"]) == userId)
                    {   
                        Response.Redirect("/Account/Profile.aspx");
                    }
                }
            }
            CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubbDBEntities();
            User user = communityHubbDBEntities.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null)
            {
                Session["fmsg"] = "User not found, please try again";
                Response.Redirect("~/");
            }
            namebox.Text = user.Name;
            userAbout.Text = user.About;
            List<Community> communities = user.CommunityUsers.Where(cu => cu.Community.IsPublic).Select(cu => cu.Community).ToList();
            commlistforuser.DataSource = communities;
            commlistforuser.DataBind();
            List<Post> posts = user.Posts.Where(post => post.Community.IsPublic).ToList();
            postsOfUser.DataSource = posts;
            postsOfUser.DataBind();
            List<Reply> replies = user.Replies.Where(reply => reply.Post.Community.IsPublic).ToList();
            replyForUser.DataSource = replies;
            replyForUser.DataBind();
            navPost.CssClass = "btn btn-dark fs-5 w-100 m-2";
            navReply.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            replyForUser.Visible = false;
        }
        protected void NavPost_Click(object sender, EventArgs e)
        {
            postsOfUser.Visible = true;
            replyForUser.Visible = false;
            navPost.CssClass = "btn btn-dark fs-5 w-100 m-2";
            navReply.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
        }

        protected void NavReply_Click(object sender, EventArgs e)
        {
            postsOfUser.Visible = false;
            replyForUser.Visible = true;
            navPost.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            navReply.CssClass = "btn btn-dark fs-5 w-100 m-2";
        }
    }
}