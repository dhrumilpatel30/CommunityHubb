using System;
using System.Linq;

namespace CommunityHubb.UserAccount
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginPost(object sender, EventArgs e)
        {
            String email = emailbox.Text;
            String password = passwordbox.Text;
            CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubbDBEntities();
            var user = communityHubbDBEntities.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                if (user.Password == password)
                {
                    Session["UserId"] = user.Id;
                    Session["UserName"] = user.Name;
                    Session["smsg"] = "Welcome " + user.Name + ", Login successful";
                    //Response.Redirect("~/UserAccount/Profile.aspx");
                    Response.Redirect("~/");
                }
                else
                {
                    Session["fmsg"] = "Invalid Password, please retry by entering correct password";
                }
            }
            else
            {
                Session["fmsg"] = "Invalid Email, please create an account to continue";
            }
            Response.Redirect("~/UserAccount/Login.aspx");
        }
    }
}