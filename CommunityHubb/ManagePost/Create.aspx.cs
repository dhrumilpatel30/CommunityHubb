using CommunityHubb.Models;
using System;
using System.Linq;

namespace CommunityHubb.ManagePost
{
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Session["fmsg"] = "Please login to create a post";
                Response.Redirect("/");
            }
            if (null == Request.QueryString["id"])
            {
                Session["fmsg"] = "Community Not Found";
                Response.Redirect("/");
            }
            int communityId = Convert.ToInt32(Request.QueryString["id"]);
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            int userId = int.Parse(Session["UserId"].ToString());
            Community community = db.Communities.Where(c => c.Id == communityId).FirstOrDefault();
            if (community == null)
            {
                Session["fmsg"] = "Community not found, please try again";
                Response.Redirect("/");
            }
            int count = db.CommunityUsers.Where(c => c.CommunityId == communityId && c.UserId == userId).Count();
            if (count == 0)
            {
                Session["fmsg"] = "You are not a member of this community, please follow community to post";
                Response.Redirect("/");

            }
            communityname.Text = community.Name;
            authorname.Text = Session["UserName"].ToString();
        }

        protected void CreatePost(object sender, EventArgs e)
        {
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            Post post = new Post
            {
                Title = titleinput.Text,
                Content = postcontent.Text,
                UserId = int.Parse(Session["UserId"].ToString()),
                CommunityId = Convert.ToInt32(Request.QueryString["id"]),
                CreatedAt = DateTime.Now
            };
            db.Posts.Add(post);
            db.SaveChanges();
            Session["smsg"] = "Post created successfully";
            Response.Redirect("~/ManagePost/Home?id=" + post.Id.ToString());

        }
    }
}