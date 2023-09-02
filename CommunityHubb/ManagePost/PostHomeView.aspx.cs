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
        protected List<Post> customPosts;
        protected void Page_Load(object sender, EventArgs e)
        {
            CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubb.CommunityHubbDBEntities();
            publicPosts = communityHubbDBEntities.Posts.Where(p => !p.Community.isPrivate).ToList();
            posts = publicPosts;
            privatePosts = new List<Post>();
            if(null != Session["UserId"])
            {
                List<Community> privateCommunityList = communityHubbDBEntities.CommunityUsers.
                    Where(cu => cu.UserId == (int)Session["UserId"]).
                    Where(cu => cu.Community.isPrivate).
                    Select(cu => cu.Community).ToList();
                foreach(Community community in privateCommunityList)
                {
                    privatePosts.AddRange(community.Posts);
                }
                posts.AddRange(privatePosts);
            }
            customPosts = posts;
            postlistview.DataSource = posts;
            postlistview.DataBind();
        }
    }
}