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
        protected List<CommunityUser> communityUsers;
        protected void Page_Load(object sender, EventArgs e)
        {
            CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubb.CommunityHubbDBEntities();
            posts = communityHubbDBEntities.Posts.Where(p => !p.Community.isPrivate).ToList();
            communityUsers = new List<CommunityUser>();
            if(null != Session["UserId"])
            {
                int userId = (int)Session["UserId"];

                communityUsers.AddRange(communityHubbDBEntities.CommunityUsers.Where(c => c.UserId == userId).ToList());

                foreach(CommunityUser communityUser in communityUsers)
                {
                    if(communityUser.Community.isPrivate)
                    {
                        posts.AddRange(communityUser.Community.Posts);
                    }
                }
            }
            postlistview.DataSource = posts;
            postlistview.DataBind();
            Session["allPosts"] = posts;
            Session["communityUsers"] = communityUsers;
            genrateFilters();
        }
        private void genrateFilters()
        {
            if(null == Session["UserId"])
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

        protected void ApplyFilters(object sender, EventArgs e)
        {
            List<Post> filteredPosts = Session["allPosts"] as List<Post>;

            if (privateRadio.Checked)
            {
                filteredPosts.RemoveAll(p => !p.Community.isPrivate);
            }
            else if (publicRadio.Checked)
            {
                filteredPosts.RemoveAll(p => p.Community.isPrivate);
            }

            if (followedRadio.Checked)
            {
                communityUsers = Session["communityUsers"] as List<CommunityUser>;
                filteredPosts.RemoveAll(p => !communityUsers.Exists(c => c.CommunityId == p.CommunityId));
            }
            else if (unfollowedRadio.Checked)
            {
                communityUsers = Session["communityUsers"] as List<CommunityUser>;
                filteredPosts.RemoveAll(p => communityUsers.Exists(c => c.CommunityId == p.CommunityId));
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