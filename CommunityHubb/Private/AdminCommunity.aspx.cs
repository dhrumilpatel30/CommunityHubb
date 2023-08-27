using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunityHubb.Private
{
    public partial class AdminCommunity : System.Web.UI.Page
    {
            protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Session["fmsg"] = "You are Not Logged In";
                Response.Redirect("~/Default");
            }
        }
    }
}