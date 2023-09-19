using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace CommunityHubb.UserAccount
{
    public partial class UserHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = 0;
            if (null == Request.QueryString["id"])
            {
                Session["fmsg"] = "Invalid User Id";
                Response.Redirect("/");
            }
            else
            {
                userId = Convert.ToInt32(Request.QueryString["id"]);
                if(null != Session["userId"])
                {
                    if (Convert.ToInt32(Session["userId"]) == userId)
                    {
                        Response.Redirect("/UserAccount/HomeAccount.aspx");
                    }
                }
            }
            CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubb.CommunityHubbDBEntities();
            User user = communityHubbDBEntities.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null)
            {
                Session["fmsg"] = "User not found";
                Response.Redirect("/");
            }
            namebox.Text = user.Name;
            userAbout.Text = user.About;
            List<Community> communities = user.CommunityUsers.Where(cu => !cu.Community.isPrivate).Select(cu => cu.Community).ToList();
            commlistforuser.DataSource = communities;
            commlistforuser.DataBind();
            List<Post> posts = user.Posts.Where(post => !post.Community.isPrivate).ToList();
            postsOfUser.DataSource = posts;
            postsOfUser.DataBind();
            List<Reply> replies = user.Replies.Where(reply => !reply.Post.Community.isPrivate).ToList();
            replyForUser.DataSource = replies;
            replyForUser.DataBind();
            navPost.CssClass = "btn btn-dark fs-5 w-100 m-2";
            navReply.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            replyForUser.Visible = false;
        }
        protected void navPost_Click(object sender, EventArgs e)
        {
            postsOfUser.Visible = true;
            replyForUser.Visible = false;
            navPost.CssClass = "btn btn-dark fs-5 w-100 m-2";
            navReply.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
        }

        protected void navReply_Click(object sender, EventArgs e)
        {
            postsOfUser.Visible = false;
            replyForUser.Visible = true;
            navPost.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            navReply.CssClass = "btn btn-dark fs-5 w-100 m-2";
        }
    }
}