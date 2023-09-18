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
        }
    }
}