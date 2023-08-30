using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunityHubb.ManagePost
{
    public partial class PostHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            CommunityHubbDBEntities entities = new CommunityHubbDBEntities();
            Post post = entities.Posts.Where(x => x.Id == postId).FirstOrDefault();
            if (null == post)
            {
                Session["fmsg"] = "Post not found";
                Response.Redirect("~/");
            }

            Community community = entities.Communities.Where(x => x.Id == post.CommunityId).FirstOrDefault();

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
            
            
        }

    }
}