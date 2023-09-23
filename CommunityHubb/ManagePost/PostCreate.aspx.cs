﻿using System;
using System.Linq;

namespace CommunityHubb.ManagePost
{
    public partial class PostCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Session["fmsg"] = "Please login to create a post";
                Response.Redirect("/");
            }
            String communityId = Request.QueryString["id"];
            if (communityId == null)
            {
                Session["fmsg"] = "Community Not Found";
                Response.Redirect("/");
            }
            Session["CommunityId"] = communityId;
            CommunityHubbDBEntities communityHubbDB = new CommunityHubbDBEntities();
            int userId = int.Parse(Session["UserId"].ToString());
            int communityIdInt = int.Parse(communityId);
            Community community = communityHubbDB.Communities.Where(c => c.Id == communityIdInt).FirstOrDefault();
            if (community == null)
            {
                Session["fmsg"] = "Community not found, please try again";
                Response.Redirect("/");
            }
            int count = communityHubbDB.CommunityUsers.Where(c => c.CommunityId == communityIdInt && c.UserId == userId).Count();
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
            CommunityHubbDBEntities communityHubbDB = new CommunityHubbDBEntities();

            Post post = new Post();
            post.Title = titleinput.Text;
            post.Content = postcontent.Text;

            post.AutorId = int.Parse(Session["UserId"].ToString());
            post.CommunityId = int.Parse(Session["CommunityId"].ToString());
            post.Date = DateTime.Now;
            post.Author = Session["UserName"].ToString();
            post.CommunityName = communityname.Text;

            communityHubbDB.Posts.Add(post);
            communityHubbDB.SaveChanges();
            Session["smsg"] = "Post created successfully";

            Response.Redirect("~/ManagePost/PostHome?id=" + post.Id.ToString());

        }
    }
}