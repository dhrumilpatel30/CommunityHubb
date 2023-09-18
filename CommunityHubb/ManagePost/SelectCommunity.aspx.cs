using System;

namespace CommunityHubb.ManagePost
{
    public partial class SelectCommunity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Session["fmsg"] = "You are Not Logged In";
                Response.Redirect("/");
            }
        }

    }
}