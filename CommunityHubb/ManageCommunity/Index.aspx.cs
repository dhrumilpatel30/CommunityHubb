using CommunityHubb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace CommunityHubb.ManageCommunity
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            List<Community> communities = db.Communities.Where((cu) => cu.IsPublic).ToList();
            if (null != Session["UserId"])
            {
                int userId = (int)Session["UserId"];
                List<CommunityUser> userCommunities = db.CommunityUsers.Where((uc) => uc.UserId == userId).ToList();
                foreach (CommunityUser cu in userCommunities)
                {
                    if (!cu.Community.IsPublic)
                    {
                        communities.Add(cu.Community);
                    }
                }
            }
            communities = communities.OrderByDescending(c => c.CreatedAt).ToList();
            communityListView.DataSource = communities;
            communityListView.DataBind();
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
                loginError.Text = "You must be logged in for all communities";
            }
        }
        protected void applyFiltersButton_Click(object sender, EventArgs e)
        {
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            List<Community> communities = new List<Community>();
            if(null != Session["UserId"])
            {
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
                        communities.AddRange(user.CommunityUsers.Where(cu => cu.Community.IsPublic).Select(cu => cu.Community).ToList());
                    }
                    else if (unfollowedRadio.Checked)
                    {
                        communities.AddRange(db.Communities.Where(c => c.IsPublic).ToList());
                        foreach (Community c in user.CommunityUsers.Where(cu => cu.Community.IsPublic).Select(cu => cu.Community).ToList())
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
            }
            else
            {
                communities.AddRange(db.Communities.Where(c => c.IsPublic).ToList());
            }
            if (recentRadio.Checked)
            {
                communities = communities.OrderByDescending(c => c.CreatedAt).ToList();
            }
            else if (alphabeticalRadio.Checked)
            {
                communities = communities.OrderBy(c => c.Name).ToList();
            }
            else if (popularRadio.Checked)
            {
                communities = communities.OrderByDescending(c => c.CommunityUsers.Count).ToList();
            }
            else
            {
                communities = communities.OrderByDescending(c => c.Name).ToList();
            }
            communityListView.DataSource = communities;
            communityListView.DataBind();
        }
    }
}