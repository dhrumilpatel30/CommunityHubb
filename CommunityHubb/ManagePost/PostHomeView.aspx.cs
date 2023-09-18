using System;
using System.Collections.Generic;
using System.Linq;
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
            if (null != Session["UserId"])
            {
                int userId = (int)Session["UserId"];

                communityUsers.AddRange(communityHubbDBEntities.CommunityUsers.Where(c => c.UserId == userId).ToList());

                foreach (CommunityUser communityUser in communityUsers)
                {
                    if (communityUser.Community.isPrivate)
                    {
                        posts.AddRange(communityUser.Community.Posts);
                    }
                }
            }
            posts = posts.OrderByDescending(p => p.Date).ToList();
            postlistview.DataSource = posts;
            postlistview.DataBind();
            genrateFilters();
        }
        private void genrateFilters()
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

        protected void ApplyFilters(object sender, EventArgs e)
        {
            List<Post> filteredPosts = new List<Post>();
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            if(null != Session["UserId"])
            {
                List<Community> communities = new List<Community>();
                int userId = (int)Session["UserId"];
                User user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (privateRadio.Checked)
                {
                    if (followedRadio.Checked || bothViewRadio.Checked)
                    {
                        communities.AddRange(user.CommunityUsers.Where(cu => cu.Community.isPrivate).Select(cu => cu.Community).ToList());
                    }
                }
                else if (publicRadio.Checked)
                {
                    if (followedRadio.Checked)
                    {
                        communities.AddRange(user.CommunityUsers.Where(cu => !cu.Community.isPrivate).Select(cu => cu.Community).ToList());
                    }
                    else if (unfollowedRadio.Checked)
                    {
                        communities.AddRange(db.Communities.Where(c => !c.isPrivate).ToList());
                        foreach (Community c in user.CommunityUsers.Where(cu => !cu.Community.isPrivate).Select(cu => cu.Community).ToList())
                        {
                            communities.Remove(c);
                        }
                    }
                    else
                    {
                        communities.AddRange(db.Communities.Where(c => !c.isPrivate).ToList());
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
                filteredPosts = db.Posts.Where(p => !p.Community.isPrivate).ToList();
            }

            //sort type
            if (recentRadio.Checked)
            {
                filteredPosts = filteredPosts.OrderByDescending(p => p.Date).ToList();
            }
            else if (alphabeticalRadio.Checked)
            {
                filteredPosts = filteredPosts.OrderBy(p => p.Title).ToList();
            }
            else if (popularRadio.Checked)
            {
                filteredPosts = filteredPosts.OrderByDescending(p => p.Likes + p.Replies.Count).ToList();
            }

            postlistview.DataSource = filteredPosts;
            postlistview.DataBind();

        }
    }
}