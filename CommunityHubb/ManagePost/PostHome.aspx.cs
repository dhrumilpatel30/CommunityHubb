using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI.WebControls;

namespace CommunityHubb.ManagePost
{
    class ReplyView : Reply
    {
        public String authorName { get; set;}
        public int likeCount { get; set; }
        public int dislikeCount { get; set; }
        public string likeCss { get; set; }
        public string dislikeCss { get; set; }
    }
    public partial class PostHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //int postId = 14;
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            Post postData = entities.Posts.Where(x => x.Id == postId).FirstOrDefault();
            if (null == postData)
            {
                Session["fmsg"] = "Post not found";
                Response.Redirect("~/");
            }

            Community community = entities.Communities.Where(x => x.Id == postData.CommunityId).FirstOrDefault();

            if (community.isPrivate)
            {
                if (null == Session["UserId"])
                {
                    Session["fmsg"] = "that is a private post, please login to continue";
                    Response.Redirect("~/");
                }
                int userId = Convert.ToInt32(Session["UserId"]);
                CommunityUser communityUser = entities.CommunityUsers.Where(x => x.CommunityId == community.Id && x.UserId == userId).FirstOrDefault();
                if (null == communityUser)
                {
                    Session["fmsg"] = "You are not a member of private community to view this post";
                    Response.Redirect("~/");
                }
            }
            generatePostData(postData);
            genreateReplyList(postData);
            generatePostLike(postData);
        }
        protected void generatePostData(Post postData)
        {
            titlebox.Text = postData.Title;
            postcontent.Text = postData.Content;
            dateofpost.Text = postData.Date.ToString();
            communityLink.HRef = "~/ManageCommunity/CommunityHome?id=" + postData.CommunityId;
            authorLink.HRef = "~/UserAccount/UserHome?id=" + postData.AutorId;
            communityname.Text = postData.Community.Name;
            authorName.Text = postData.User.Name;
        }
        protected void generatePostLike(Post postData)
        {
            postLikeCount.Text = postData.Reactions.Where(rect => rect.isLike && !rect.isReply).Count().ToString();
            postDislikeCount.Text = postData.Reactions.Where(rect => !rect.isLike && !rect.isReply).Count().ToString();

            int userId = Convert.ToInt32(Session["UserId"]);
            if(null == Session["UserId"])
            {
                PostLike.CssClass = "btn btn-outline-dark";
                PostDislike.CssClass = "btn btn-outline-dark";
                return;
            }
            else
            {
                Reaction reaction = postData.Reactions.Where(rect => rect.userId == userId && !rect.isReply).FirstOrDefault();
                
                if(null != reaction)
                {
                    if (reaction.isLike)
                    {
                        PostLike.CssClass = "btn btn-dark";
                        PostDislike.CssClass = "btn btn-outline-dark";
                    }
                    else
                    {
                        PostLike.CssClass = "btn btn-outline-dark";
                        PostDislike.CssClass = "btn btn-dark";
                    }
                }
                else
                {
                    PostLike.CssClass = "btn btn-outline-dark";
                    PostDislike.CssClass = "btn btn-outline-dark";
                }
            }
        }
        protected void genreateReplyList(Post postData1)
        {
            List<ReplyView> replyViews = new List<ReplyView>();
            int userId = Convert.ToInt32(Session["UserId"]);
            foreach (Reply reply in postData1.Replies)
            {
                ReplyView replyView = new ReplyView();
                replyView.Id = reply.Id;
                replyView.PostId = reply.PostId;
                replyView.Content = reply.Content;
                replyView.UserId = reply.UserId;
                replyView.Created = reply.Created;
                replyView.likeCount = reply.Reactions.Where(rect => rect.isLike).Count();
                replyView.dislikeCount = reply.Reactions.Where(rect => !rect.isLike).Count();
                replyView.authorName = reply.User.Name;
                Reaction reaction = reply.Reactions.Where(rect =>rect.userId == userId).FirstOrDefault();
                if(null != reaction)
                {
                    if (reaction.isLike)
                    {
                        replyView.likeCss = "btn btn-dark";
                        replyView.dislikeCss = "btn btn-outline-dark";
                    }
                    else
                    {
                        replyView.likeCss = "btn btn-outline-dark";
                        replyView.dislikeCss = "btn btn-dark";
                    }
                }
                else
                {
                    replyView.likeCss = "btn btn-outline-dark";
                    replyView.dislikeCss = "btn btn-outline-dark";
                }
                replyViews.Add(replyView);
            }
            repliesListView.DataSource = replyViews;
            repliesListView.DataBind();
        }
        protected void LikeClick_Click(object sender, EventArgs e)
        {
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            Post postData = entities.Posts.Where(x => x.Id == postId).FirstOrDefault();
            string commandArgument = ((Button)sender).CommandArgument;
            int replyId = Convert.ToInt32(commandArgument);
            if(null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to like this post";
                Response.Redirect("~/ManagePost/PostHome?id=" + postData.Id);
            }

            Reply reply = postData.Replies.Where(x => x.Id == replyId).FirstOrDefault();
            if (null == reply)
            {
                Session["fmsg"] = "Reply not found";
                Response.Redirect("~/ManagePost/PostHome?id="+postData.Id);
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            Reaction reaction = reply.Reactions.Where(x => x.userId == userId).FirstOrDefault();
            if(null == reaction)
            {
                reaction = new Reaction
                {
                    isLike = true,
                    userId = userId,
                    replyId = replyId,
                    isReply = true,
                    postId = postData.Id,
                };
                entities.Reactions.Add(reaction);
            }
            else
            {
                if (!reaction.isLike)
                {
                    reaction.isLike = true;
                }
                else
                {
                    entities.Reactions.Remove(reaction);
                    entities.SaveChanges();
                    Response.Redirect("~/ManagePost/PostHome?id=" + postData.Id);
                }
            }
            entities.Reactions.AddOrUpdate(reaction);
            entities.SaveChanges();
            genreateReplyList(postData);

        }

        protected void Dislike_Click(object sender, EventArgs e)
        {
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            Post postData = entities.Posts.Where(x => x.Id == postId).FirstOrDefault();
            string commandArgument = ((Button)sender).CommandArgument;
            int replyId = Convert.ToInt32(commandArgument);
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to dislike this post";
                Response.Redirect("~/ManagePost/PostHome?id="+ postData.Id);
            }

            Reply reply = postData.Replies.Where(x => x.Id == replyId).FirstOrDefault();
            if (null == reply)
            {
                Session["fmsg"] = "Reply not found";
                Response.Redirect("~/ManagePost/PostHome?id="+ postData.Id);
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            Reaction reaction = reply.Reactions.Where(x => x.userId == userId).FirstOrDefault();
            if (null == reaction)
            {
                reaction = new Reaction
                {
                    isLike = false,
                    userId = userId,
                    replyId = replyId,
                    isReply = true,
                    postId = postData.Id,
                };
                entities.Reactions.Add(reaction);
            }
            else
            {
                if(reaction.isLike)
                {
                    reaction.isLike = false;
                }
                else
                {
                    entities.Reactions.Remove(reaction);
                    entities.SaveChanges();
                    Response.Redirect("~/ManagePost/PostHome?id=" + postData.Id);
                }
            }
            entities.Reactions.AddOrUpdate(reaction);
            entities.SaveChanges();
            genreateReplyList(postData);
        }

        protected void PostLike_Click(object sender, EventArgs e)
        {
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            Post postData = entities.Posts.Where(x => x.Id == postId).FirstOrDefault();
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to like this post";
                Response.Redirect("~/ManagePost/PostHome?id=" + postData.Id);
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            Reaction reaction = postData.Reactions.Where(x => x.userId == userId && !x.isReply).FirstOrDefault();
            if (null == reaction)
            {
                reaction = new Reaction
                {
                    isLike = true,
                    userId = userId,
                    postId = postData.Id,
                    isReply = false,
                };
                entities.Reactions.Add(reaction);
            }
            else
            {
                if (!reaction.isLike)
                {
                    reaction.isLike = true;
                }
                else
                {
                    entities.Reactions.Remove(reaction);
                    entities.SaveChanges();
                    Response.Redirect("~/ManagePost/PostHome?id=" + postData.Id);
                }
            }
            entities.Reactions.AddOrUpdate(reaction);
            entities.SaveChanges();
            generatePostLike(postData);
        }

        protected void PostDislike_Click(object sender, EventArgs e)
        {
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            Post postData = entities.Posts.Where(x => x.Id == postId).FirstOrDefault();
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to dislike this post";
                Response.Redirect("~/ManagePost/PostHome?id=" + postData.Id);
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            Reaction reaction = postData.Reactions.Where(x => x.userId == userId && !x.isReply).FirstOrDefault();
            if (null == reaction)
            {
                reaction = new Reaction
                {
                    isLike = false,
                    userId = userId,
                    postId = postData.Id,
                    isReply = false,
                };
                entities.Reactions.Add(reaction);
            }
            else
            {
                if (reaction.isLike)
                {
                    reaction.isLike = false;
                }
                else
                {
                    entities.Reactions.Remove(reaction);
                    entities.SaveChanges();
                    Response.Redirect("~/ManagePost/PostHome?id=" + postData.Id);
                }
            }
            entities.Reactions.AddOrUpdate(reaction);
            entities.SaveChanges();
            generatePostLike(postData);

        }
    }
}