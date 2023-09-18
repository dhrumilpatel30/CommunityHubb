using System;
using System.Web.UI;

namespace CommunityHubb
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["smsg"] != null)
            {
                successlabel.Visible = true;
                successlabel.Text = Session["smsg"].ToString();
                Session["smsg"] = null;
            }
            if (Session["fmsg"] != null)
            {
                FailLabel.Text = Session["fmsg"].ToString();
                FailLabel.Visible = true;
                Session["fmsg"] = null;
            }

            //Session["UserId"] = 1
        }
        protected void logout(object sender, EventArgs e)
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            Session["smsg"] = "You have successfully logged out";
            Response.Redirect("~/");
        }
    }
}