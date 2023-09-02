using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
                if(null != Session["UserId"])
                {
                    userId = Convert.ToInt32(Session["UserId"]);
                }
                else
                {
                    Session["fmsg"] = "Invalid User Id";
                    Response.Redirect("~/Default");
                }
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
                Response.Redirect("~/Default");
            }
            titlebox.Text = user.Name;
            commdesc.Text = user.About;

        }
    }
}