using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunityHubb.UserAccount
{
    public partial class HomeAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(null == Session["userId"])
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
            List<Community> communities = user.CommunityUsers.Where(cu => !cu.Community.isPrivate).Select(cu => cu.Community).ToList();
            commlistforuser.DataSource = communities;
            commlistforuser.DataBind();
            List<Post> posts = user.Posts.OrderByDescending(p => p.Date).ToList();
            postsOfUser.DataSource = posts;
            postsOfUser.DataBind();
        }
        protected void deleteButton_Click(object sender, EventArgs e)
        {
            int postId = Convert.ToInt32(((Button)sender).CommandArgument);
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            Post post = db.Posts.Where(p => p.Id == postId).FirstOrDefault();
            if (post != null)
            {
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

    }
}