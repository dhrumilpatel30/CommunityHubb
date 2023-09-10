using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunityHubb.ManagePost
{
    public partial class PostHomeView : System.Web.UI.Page
    {
        protected List<Post> posts;
        protected List<Post> publicPosts;
        protected List<Post> privatePosts;
        protected List<CommunityUser> communityUsers;
        protected void Page_Load(object sender, EventArgs e)
        {
            CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubb.CommunityHubbDBEntities();
            publicPosts = communityHubbDBEntities.Posts.Where(p => !p.Community.isPrivate).ToList();
            posts = publicPosts;
            privatePosts = new List<Post>();
            communityUsers = new List<CommunityUser>();
            if(null != Session["UserId"])
            {
                int userId = (int)Session["UserId"];

                communityUsers.AddRange(communityHubbDBEntities.CommunityUsers.Where(c => c.UserId == userId).ToList());

                foreach(CommunityUser communityUser in communityUsers)
                {
                    if(communityUser.Community.isPrivate)
                    {
                        privatePosts.AddRange(communityUser.Community.Posts);
                    }
                }
                posts.AddRange(privatePosts);
            }
            postlistview.DataSource = posts;
            postlistview.DataBind();
            Session["publicPosts"] = publicPosts;
            Session["privatePosts"] = privatePosts;
            Session["communityUsers"] = communityUsers;
            genrateFilters();
        }
        private void genrateFilters()
        {
            if(null == Session["UserId"])
            {
                
                privateRadio.Enabled = false;
                bothRadio.Enabled = false;
                followedRadio.Enabled = false;
                bothViewRadio.Enabled = false;
                unfollowedRadio.Checked = true;
                privateRadio.CssClass = "text-black-50";
                bothRadio.CssClass = "text-black-50";
                followedRadio.CssClass = "text-black-50";
                bothViewRadio.CssClass = "text-black-50";
                Session["fmsg"] = "Please login to view private and followed posts";
            }
        }

        protected void ApplyFilters(object sender, EventArgs e)
        {
            List<Post> filteredPosts = Session["publicPosts"] as List<Post>;
            //post type
            if (privateRadio.Checked)
            {
                filteredPosts = Session["privatePosts"] as List<Post>;
            }
            else if (bothRadio.Checked)
            {
                filteredPosts.AddRange(Session["privatePosts"] as List<Post>);
            }
            communityUsers = Session["communityUsers"] as List<CommunityUser>;
            List<Post> rough = filteredPosts;
            //post view
            if (followedRadio.Checked)
            {
                foreach (Post post in rough)
                {
                    if (communityUsers.Any(c => c.CommunityId != post.CommunityId))
                    {
                        filteredPosts.Remove(post);
                    }
                }
            }
            else if (unfollowedRadio.Checked && null != Session["UserId"])
            {
                foreach (Post post in rough)
                {
                    if (communityUsers.Any(c => c.CommunityId == post.CommunityId))
                    {
                        filteredPosts.Remove(post);
                    }
                }
            }

            //sort type
            if(recentRadio.Checked)
            {
                filteredPosts = filteredPosts.OrderByDescending(p => p.Date).ToList();
            }
            else if (alphabeticalRadio.Checked)
            {
                filteredPosts = filteredPosts.OrderBy(p => p.Title).ToList();
            }
            else if (popularRadio.Checked)
            {
                //filteredPosts = filteredPosts.OrderByDescending(p => p.Likes + p.Replies.Count).ToList();
            }

            postlistview.DataSource = filteredPosts;
            postlistview.DataBind();

        }
    }
}