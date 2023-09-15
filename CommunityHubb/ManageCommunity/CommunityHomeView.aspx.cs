using CommunityHubb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunityHubb.ManageCommunity
{
    public partial class CommunityHomeView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CommunityHubb.CommunityHubbDBEntities db = new CommunityHubb.CommunityHubbDBEntities();

            //public community
            List<Community> communities = db.Communities.Where((cu) => !cu.isPrivate).ToList();
            if(null != Session["UserId"])
            {
                int userId = (int)Session["UserId"];
                List<CommunityUser> userCommunities = db.CommunityUsers.Where((uc) => uc.UserId == userId).ToList();
                foreach (CommunityUser cu in userCommunities)
                {
                    if(cu.Community.isPrivate)
                    {
                        communities.Add(cu.Community);
                    }
                }
            }
            communityList.DataSource = communities;
            communityList.DataBind();
        }
    }
}