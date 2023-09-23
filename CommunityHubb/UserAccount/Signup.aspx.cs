using System;

namespace CommunityHubb.UserAccount
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SignupUser(object sender, EventArgs e)
        {

            String email = emailbox.Text;
            String password = passwordbox.Text;
            String name = namebox.Text;
            String phone = numberbox.Text;
            DateTime? dateofbirth = string.IsNullOrEmpty(dobbox.Text) ? (DateTime?)null : DateTime.Parse(dobbox.Text);
            String about = aboutbox.Text;

            CommunityHubb.User user = new CommunityHubb.User();
            user.Email = email;
            user.Password = password;
            user.Name = name;
            user.Phone = phone;
            user.Dob = dateofbirth;
            user.About = about;
            //are used to verify the user with email but not implemented yet
            user.isValidated = true;
            user.UniqueCode = "1234";

            //save to database
            CommunityHubb.CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubb.CommunityHubbDBEntities();
            communityHubbDBEntities.Users.Add(user);
            communityHubbDBEntities.SaveChanges();
            Session["smsg"] = "you are successfully signed up for CommunityHubb, you are also logged in";
            Session["UserId"] = user.Id;
            Session["UserName"] = user.Name;
            Response.Redirect("/");
        }
    }
}