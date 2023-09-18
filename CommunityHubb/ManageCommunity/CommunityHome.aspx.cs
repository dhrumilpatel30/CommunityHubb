using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace CommunityHubb.ManageCommunity
{
    public partial class CommunityHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Session["fmsg"] = "Invalid Community Id";
                Response.Redirect("/");
            }
            int communityId = Convert.ToInt32(Request.QueryString["id"]);
            CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubb.CommunityHubbDBEntities();
            Community community = communityHubbDBEntities.Communities.Where(c => c.Id == communityId).FirstOrDefault();
            if (community == null)
            {
                Session["fmsg"] = "Community not found";
                Response.Redirect("/");
            }
            if (community.isPrivate)
            {
                List<User> users = community.CommunityUsers.Select(cu => cu.User).ToList();
                int userId = Convert.ToInt32(Session["UserId"]);
                if (!users.Any(u => u.Id == userId))
                {
                    Session["fmsg"] = "You are not a member of this private community";
                    Response.Redirect("/");
                }
            }
            titlebox.Text = community.Name;
            commdesc.Text = community.Description;
            if (null == Session["UserId"])
            {
                followbtn.Text = "Login to follow";
            }
            else
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                CommunityUser communityUser = community.CommunityUsers.Where(cu => cu.UserId == userId).FirstOrDefault();
                if (communityUser != null)
                {
                    followbtn.Text = "Unfollow Community";
                }
                else
                {
                    followbtn.Text = "Follow Community";
                }
            }
            userlist.DataSource = community.CommunityUsers.Select(cu => cu.User).ToList();
            userlist.DataBind();
            postlist.DataSource = community.Posts.ToList();
            postlist.DataBind();
            createButton.HRef = "~/ManagePost/PostCreate.aspx?id=" + communityId;
        }

        protected void ToggleFollow(object sender, EventArgs e)
        {
            if (null == Session["UserId"])
            {
                Response.Redirect("~/UserAccount/Login.aspx");
            }
            else
            {
                int comId = Convert.ToInt32(Request.QueryString["id"]);
                Community community = new CommunityHubbDBEntities().Communities.Where(c => c.Id == comId).FirstOrDefault();
                CommunityHubbDBEntities communityHubbDB = new CommunityHubbDBEntities();

                int userId = Convert.ToInt32(Session["UserId"]);
                CommunityUser communityUser = community.CommunityUsers.Where(cu => cu.UserId == userId).FirstOrDefault();
                if (communityUser != null)
                {
                    community.CommunityUsers.Remove(communityUser);
                    //communityHubbDB.Entry(community).State = System.Data.Entity.EntityState.Modified;
                    CommunityUser communityUserInDB = communityHubbDB.CommunityUsers.Find(communityUser.Id);
                    communityHubbDB.CommunityUsers.Remove(communityUserInDB);
                    communityHubbDB.SaveChanges();

                    followbtn.Text = "Follow Community";
                }
                else
                {
                    CommunityUser newCommunityUser = new CommunityUser
                    {
                        UserId = userId,
                        CommunityId = community.Id,
                        IsAdmin = false
                    };
                    communityHubbDB.CommunityUsers.Add(newCommunityUser);
                    communityHubbDB.SaveChanges();
                    User user = communityHubbDB.Users.Find(userId);
                    newCommunityUser.User = user;
                    community.CommunityUsers.Add(newCommunityUser);
                    followbtn.Text = "Unfollow Community";
                }
                generateMembersList(community);
            }


        }
        protected void generateMembersList(Community community)
        {
            userlist.DataSource = community.CommunityUsers.Select(cu => cu.User).ToList();
            userlist.DataBind();
        }
    }
}