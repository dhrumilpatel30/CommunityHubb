using CommunityHubb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI.WebControls;

namespace CommunityHubb.ManagePost
{
    class ReplyView : Reply
    {
        public String authorName { get; set; }
        public int likeCount { get; set; }
        public int dislikeCount { get; set; }
        public string likeCss { get; set; }
        public string dislikeCss { get; set; }
    }
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (null == Request.QueryString["id"])
            {
                Session["fmsg"] = "Post not found, please try again";
                Response.Redirect("~/");
            }
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            Post postData = db.Posts.Where(x => x.Id == postId).FirstOrDefault();
            if (null == postData)
            {
                Session["fmsg"] = "Post not found, please try again";
                Response.Redirect("~/");
            }
            Community community = postData.Community;
            if (!community.IsPublic)
            {
                if (null == Session["UserId"])
                {
                    Session["fmsg"] = "Please login to view this private post";
                    Response.Redirect("~/");
                }
                int userId = Convert.ToInt32(Session["UserId"]);
                CommunityUser communityUser = db.CommunityUsers.Where(x => x.CommunityId == community.Id && x.UserId == userId).FirstOrDefault();
                if (null == communityUser)
                {
                    Session["fmsg"] = "You are not a member of this private community to view post";
                    Response.Redirect("~/");
                }
            }
            GeneratePostData(postData);
            GenreateReplyList(postData);
            GeneratePostLike(postData);
            replyBox.Visible = false;
        }
        protected void GeneratePostData(Post postData)
        {
            titlebox.Text = postData.Title;
            postcontent.Text = postData.Content;
            dateofpost.Text = postData.CreatedAt.ToString();
            communityLink.HRef = "~/ManageCommunity/Home?id=" + postData.CommunityId;
            authorLink.HRef = "~/Account/Home?id=" + postData.User.Id;
            communityname.Text = postData.Community.Name;
            authorName.Text = postData.User.Name;
        }
        protected void GeneratePostLike(Post postData)
        {
            postLikeCount.Text = postData.ReactionPosts.Where(rect => rect.IsLike).Count().ToString();
            postDislikeCount.Text = postData.ReactionPosts.Where(rect => !rect.IsLike).Count().ToString();

            if (null == Session["UserId"])
            {
                PostLike.CssClass = "btn btn-outline-dark";
                PostDislike.CssClass = "btn btn-outline-dark";
                return;
            }
            else
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                ReactionPost reaction = postData.ReactionPosts.Where(rect => rect.UserId == userId).FirstOrDefault();

                if (null != reaction)
                {
                    if (reaction.IsLike)
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
        protected void GenreateReplyList(Post postData)
        {
            List<ReplyView> replyViews = new List<ReplyView>();
            int userId = Convert.ToInt32(Session["UserId"]);
            foreach (Reply reply in postData.Replies)
            {
                ReplyView replyView = new ReplyView
                {
                    Id = reply.Id,
                    PostId = reply.PostId,
                    Content = reply.Content,
                    CreatedAt = reply.CreatedAt,
                    likeCount = reply.ReactionReplies.Where(rect => rect.IsLike).Count(),
                    dislikeCount = reply.ReactionReplies.Where(rect => !rect.IsLike).Count(),
                    User = reply.User,
                };
                ReactionReply reaction = reply.ReactionReplies.Where(rect => rect.UserId == userId).FirstOrDefault();
                if (null != reaction)
                {
                    if (reaction.IsLike)
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
            replyViews = replyViews.OrderByDescending(x => x.CreatedAt).ToList();
            repliesListView.DataSource = replyViews;
            repliesListView.DataBind();
        }
        protected void LikeClick_Click(object sender, EventArgs e)
        {
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            Post postData = db.Posts.Where(x => x.Id == postId).FirstOrDefault();
            string commandArgument = ((Button)sender).CommandArgument;
            int replyId = Convert.ToInt32(commandArgument);
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to like this reply";
                Response.Redirect("~/ManagePost/Home?id=" + postData.Id);
            }
            Reply reply = postData.Replies.Where(x => x.Id == replyId).FirstOrDefault();
            if (null == reply)
            {
                Session["fmsg"] = "Reply not found";
                Response.Redirect("~/ManagePost/Home?id=" + postData.Id);
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            ReactionReply reaction = reply.ReactionReplies.Where(x => x.UserId == userId).FirstOrDefault();
            if (null == reaction)
            {
                reaction = new ReactionReply
                {
                    IsLike = true,
                    UserId = userId,
                    ReplyId = replyId,
                };
                db.ReactionReplies.Add(reaction);
            }
            else
            {
                if (!reaction.IsLike)
                {
                    reaction.IsLike = true;
                    db.ReactionReplies.AddOrUpdate(reaction);
                }
                else
                {
                    db.ReactionReplies.Remove(reaction);
                }
            }
            db.SaveChanges();
            GenreateReplyList(postData);

        }

        protected void Dislike_Click(object sender, EventArgs e)
        {
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            Post postData = db.Posts.Where(x => x.Id == postId).FirstOrDefault();
            string commandArgument = ((Button)sender).CommandArgument;
            int replyId = Convert.ToInt32(commandArgument);
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to dislike this reply";
                Response.Redirect("~/ManagePost/Home?id=" + postData.Id);
            }

            Reply reply = postData.Replies.Where(x => x.Id == replyId).FirstOrDefault();
            if (null == reply)
            {
                Session["fmsg"] = "Reply not found";
                Response.Redirect("~/ManagePost/Home?id=" + postData.Id);
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            ReactionReply reaction = reply.ReactionReplies.Where(x => x.UserId == userId).FirstOrDefault();
            if (null == reaction)
            {
                reaction = new ReactionReply
                {
                    IsLike = false,
                    UserId = userId,
                    ReplyId = replyId,
                };
                db.ReactionReplies.Add(reaction);
            }
            else
            {
                if (reaction.IsLike)
                {
                    reaction.IsLike = false;
                    db.ReactionReplies.AddOrUpdate(reaction);
                }
                else
                {
                    db.ReactionReplies.Remove(reaction);
                }
            }
            db.SaveChanges();
            GenreateReplyList(postData);
        }

        protected void PostLike_Click(object sender, EventArgs e)
        {
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            Post postData = db.Posts.Where(x => x.Id == postId).FirstOrDefault();
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to like this post";
                Response.Redirect("~/ManagePost/Home?id=" + postData.Id);
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            ReactionPost reaction = postData.ReactionPosts.Where(x => x.UserId == userId).FirstOrDefault();
            if (null == reaction)
            {
                reaction = new ReactionPost
                {
                    IsLike = true,
                    UserId = userId,
                    PostId = postData.Id,
                };
                db.ReactionPosts.Add(reaction);
            }
            else
            {
                if (!reaction.IsLike)
                {
                    reaction.IsLike = true;
                    db.ReactionPosts.AddOrUpdate(reaction);
                }
                else
                {
                    db.ReactionPosts.Remove(reaction);
                }
            }
            db.SaveChanges();
            GeneratePostLike(postData);
        }

        protected void PostDislike_Click(object sender, EventArgs e)
        {
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            Post postData = db.Posts.Where(x => x.Id == postId).FirstOrDefault();
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to dislike this post";
                Response.Redirect("~/ManagePost/Home?id=" + postData.Id);
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            ReactionPost reaction = postData.ReactionPosts.Where(x => x.UserId == userId).FirstOrDefault();
            if (null == reaction)
            {
                reaction = new ReactionPost
                {
                    IsLike = false,
                    UserId = userId,
                    PostId = postData.Id,
                };
                db.ReactionPosts.Add(reaction);
            }
            else
            {
                if (reaction.IsLike)
                {
                    reaction.IsLike = false;
                    db.ReactionPosts.AddOrUpdate(reaction);
                }
                else
                {
                    db.ReactionPosts.Remove(reaction);
                }
            }
            db.SaveChanges();
            GeneratePostLike(postData);
        }

        protected void SaveReply_Click(object sender, EventArgs e)
        {
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to reply to this post";
                Response.Redirect("~/ManagePost/Home?id=" + Request.QueryString["id"]);
            }
            string replyDataValue = replyData.Text;
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            Post postData = db.Posts.Where(x => x.Id == postId).FirstOrDefault();
            Reply reply = new Reply
            {
                UserId = Convert.ToInt32(Session["UserId"]),
                PostId = postData.Id,
                Content = replyDataValue,
                CreatedAt = DateTime.Now,
            };
            db.Replies.Add(reply);
            db.SaveChanges();
            Session["smsg"] = "Reply added successfully";
            Response.Redirect("~/ManagePost/Home?id=" + postData.Id);
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            replyBox.Visible = false;
        }

        protected void AddReplyBtn_Click(object sender, EventArgs e)
        {
            if (null == Session["UserId"])
            {
                Session["fmsg"] = "Please login to reply to this post";
                Response.Redirect("~/ManagePost/Home?id=" + Request.QueryString["id"]);
            }
            replyBox.Visible = true;
        }
    }
}