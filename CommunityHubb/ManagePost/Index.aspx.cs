using CommunityHubb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace CommunityHubb.ManagePost
{
    public class PostView : Post
    {
        public string LikeCount { get; set; }
        public string DislikeCount { get; set; }
        public string ReplyCount { get; set; }
    }
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Post> posts;
            List<CommunityUser> communityUsers;
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            posts = db.Posts.Where(p => p.Community.IsPublic).ToList();
            communityUsers = new List<CommunityUser>();
            if (null != Session["UserId"])
            {
                int userId = (int)Session["UserId"];
                communityUsers.AddRange(db.CommunityUsers.Where(c => c.UserId == userId).ToList());
                foreach (CommunityUser communityUser in communityUsers)
                {
                    if (!communityUser.Community.IsPublic)
                    {
                        posts.AddRange(communityUser.Community.Posts);
                    }
                }
            }
            posts = posts.OrderByDescending(p => p.CreatedAt).ToList();
            BindPostView(posts);
            GenrateFilters();
        }
        private void GenrateFilters()
        {
            if (null == Session["UserId"])
            {
                privateRadio.Enabled = false;
                unfollowedRadio.Enabled = false;
                followedRadio.Enabled = false;
                publicRadio.Enabled = false;
                privateRadio.CssClass = "text-black-50";
                followedRadio.CssClass = "text-black-50";
                unfollowedRadio.CssClass = "text-black-50";
                publicRadio.CssClass = "text-black-50";
                loginError.Text = "*You must be logged in for all posts";
            }
        }
        protected void BindPostView(List<Post> posts)
        {
            List<PostView> postViews = new List<PostView>();
            foreach (Post post in posts)
            {
                PostView postView = new PostView();
                postView.Id = post.Id;
                postView.Title = post.Title;
                postView.Content = post.Content;
                postView.CreatedAt = post.CreatedAt;
                postView.User = post.User;
                postView.Community = post.Community;
                int temp = post.ReactionPosts.Where(rp => rp.IsLike).Count();
                if(2 > temp)
                {
                    postView.LikeCount = temp.ToString() + " Like";
                }
                else
                {
                    postView.LikeCount = temp.ToString() + " Likes";
                }
                temp = post.ReactionPosts.Where(rp => !rp.IsLike).Count();
                if (2 > temp)
                {
                    postView.DislikeCount = temp.ToString() + " Dislike";
                }
                else
                {
                    postView.DislikeCount = temp.ToString() + " Dislikes";
                }
                temp = post.Replies.Count;
                if (2 > temp)
                {
                    postView.ReplyCount = temp.ToString() + " Reply";
                }
                else
                {
                    postView.ReplyCount = temp.ToString() + " Replies";
                }
                postViews.Add(postView);
            }
            postlistview.DataSource = postViews;
            postlistview.DataBind();

        }
        protected void ApplyFilters(object sender, EventArgs e)
        {
            List<Post> filteredPosts = new List<Post>();
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            if (null != Session["UserId"])
            {
                List<Community> communities = new List<Community>();
                int userId = (int)Session["UserId"];
                User user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (privateRadio.Checked)
                {
                    if (followedRadio.Checked || bothViewRadio.Checked)
                    {
                        communities.AddRange(user.CommunityUsers.Where(cu => !cu.Community.IsPublic).Select(cu => cu.Community).ToList());
                    }
                }
                else if (publicRadio.Checked)
                {
                    if (followedRadio.Checked)
                    {
                        communities.AddRange(user.CommunityUsers.Where(cu => !!cu.Community.IsPublic).Select(cu => cu.Community).ToList());
                    }
                    else if (unfollowedRadio.Checked)
                    {
                        communities.AddRange(db.Communities.Where(c => c.IsPublic).ToList());
                        foreach (Community c in user.CommunityUsers.Where(cu => !!cu.Community.IsPublic).Select(cu => cu.Community).ToList())
                        {
                            communities.Remove(c);
                        }
                    }
                    else
                    {
                        communities.AddRange(db.Communities.Where(c => c.IsPublic).ToList());
                    }
                }
                else
                {
                    if (followedRadio.Checked)
                    {
                        communities.AddRange(user.CommunityUsers.Select(cu => cu.Community).ToList());
                    }
                    else if (unfollowedRadio.Checked)
                    {
                        communities.AddRange(db.Communities.ToList());
                        foreach (Community c in user.CommunityUsers.Select(cu => cu.Community).ToList())
                        {
                            communities.Remove(c);
                        }
                    }
                    else
                    {
                        communities.AddRange(db.Communities.ToList());
                    }
                }
                foreach (Community community in communities)
                {
                    filteredPosts.AddRange(community.Posts);
                }
            }
            else
            {
                filteredPosts = db.Posts.Where(p => !!p.Community.IsPublic).ToList();
            }
            if (recentRadio.Checked)
            {
                filteredPosts = filteredPosts.OrderByDescending(p => p.CreatedAt).ToList();
            }
            else if (alphabeticalRadio.Checked)
            {
                filteredPosts = filteredPosts.OrderBy(p => p.Title).ToList();
            }
            else if (popularRadio.Checked)
            {
                filteredPosts = filteredPosts.OrderByDescending(p => p.ReactionPosts.Count + p.Replies.Count).ToList();
            }
            BindPostView(filteredPosts);
        }
    }
}