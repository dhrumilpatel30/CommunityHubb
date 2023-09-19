using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace CommunityHubb.UserAccount
{
    public partial class HomeAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (null == Session["userId"])
            {
                Session["fmsg"] = "Please login to continue";
                Response.Redirect("~/");
            }
            int userId = Convert.ToInt32(Session["userId"]);
            CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubbDBEntities();
            User user = communityHubbDBEntities.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null)
            {
                Session["fmsg"] = "User not found";
                Response.Redirect("/");
            }
            namebox.Text = user.Name;
            userAbout.Text = user.About;
            List<Community> communities = user.CommunityUsers.Select(cu => cu.Community).ToList();
            commlistforuser.DataSource = communities;
            commlistforuser.DataBind();
            List<Post> posts = user.Posts.OrderByDescending(p => p.Date).ToList();
            postsOfUser.DataSource = posts;
            postsOfUser.DataBind();
            List<Community> commList = user.CommunityUsers.Where(cu => cu.IsAdmin).Select(cu => cu.Community).ToList();
            commListOfUser.DataSource = commList;
            commListOfUser.DataBind();
            List<Reply> replies = user.Replies.OrderByDescending(r => r.Created).ToList();
            replyForUser.DataSource = replies;
            replyForUser.DataBind();
            navComm.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            navReply.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            navPost.CssClass = "btn btn-dark fs-5 w-100 m-2";
            replyForUser.Visible = false;
            commListOfUser.Visible = false;
        }


        protected void deletePost_Click(object sender, EventArgs e)
        {
            int postId = Convert.ToInt32(((Button)sender).CommandArgument);
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            Post post = db.Posts.Where(p => p.Id == postId).FirstOrDefault();
            if (post != null)
            {
                foreach (Reaction reaction in post.Reactions.ToList())
                {
                    db.Reactions.Remove(reaction);
                }
                foreach (Reply reply in post.Replies.ToList())
                {
                    db.Replies.Remove(reply);
                }
                db.Posts.Remove(post);
                db.SaveChanges();
                Session["smsg"] = "Post deleted successfully";
                Response.Redirect("~/UserAccount/HomeAccount");
            }
            else
            {
                Session["fmgs"] = "Post not found";
                Response.Redirect("~/");
            }

        }

        protected void deleteReply_Click(object sender, EventArgs e)
        {
            int replyId = Convert.ToInt32(((Button)sender).CommandArgument);
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            Reply reply = db.Replies.Where(r => r.Id == replyId).FirstOrDefault();
            if (reply != null)
            {
                foreach(Reaction reaction in reply.Reactions.ToList())
                {
                    db.Reactions.Remove(reaction);
                }
                db.Replies.Remove(reply);
                db.SaveChanges();
                Session["smsg"] = "Reply deleted successfully";
                Response.Redirect("~/UserAccount/HomeAccount");
            }
            else
            {
                Session["fmgs"] = "Reply not found";
                Response.Redirect("~/");
            }
        }

        protected void deleteCommunity_Click(object sender, EventArgs e)
        {
            int communityId = Convert.ToInt32(((Button)sender).CommandArgument);
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            Community community = db.Communities.Where(c => c.Id == communityId).FirstOrDefault();
            if (community != null)
            {
                foreach (CommunityUser communityUser in community.CommunityUsers.ToList())
                {
                    db.CommunityUsers.Remove(communityUser);
                }
                foreach (Post post in community.Posts.ToList())
                {
                    foreach (Reaction reaction in post.Reactions.ToList())
                    {
                        db.Reactions.Remove(reaction);
                    }
                    foreach (Reply reply in post.Replies.ToList())
                    {
                        db.Replies.Remove(reply);
                    }
                    db.Posts.Remove(post);
                }
                db.Communities.Remove(community);
                db.SaveChanges();
                Session["smsg"] = "Community deleted successfully";
                Response.Redirect("~/UserAccount/HomeAccount");
            }
            else
            {
                Session["fmgs"] = "Community not found";
                Response.Redirect("~/");
            }
        }

        protected void navPost_Click(object sender, EventArgs e)
        {
            postsOfUser.Visible = true;
            replyForUser.Visible = false;
            commListOfUser.Visible = false;
            navPost.CssClass = "btn btn-dark fs-5 w-100 m-2";
            navReply.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            navComm.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
        }

        protected void navReply_Click(object sender, EventArgs e)
        {
            postsOfUser.Visible = false;
            replyForUser.Visible = true;
            commListOfUser.Visible = false;
            navPost.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            navReply.CssClass = "btn btn-dark fs-5 w-100 m-2";
            navComm.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
        }

        protected void navComm_Click(object sender, EventArgs e)
        {
            postsOfUser.Visible = false;
            replyForUser.Visible = false;
            commListOfUser.Visible = true;
            navPost.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            navReply.CssClass = "btn btn-outline-dark fs-5 w-100 m-2";
            navComm.CssClass = "btn btn-dark fs-5 w-100 m-2";
        }
    }
}