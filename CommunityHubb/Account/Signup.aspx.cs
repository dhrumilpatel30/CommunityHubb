using CommunityHubb.Models;
using System;

namespace CommunityHubb.Account
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
            User user = new User
            {
                Email = email,
                Password = password,
                Name = name,
                Phone = phone,
                Dob = dateofbirth,
                About = about,
            };
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            db.Users.Add(user);
            db.SaveChanges();
            Session["smsg"] = "you are successfully signed up for CommunityHubb, you are also logged in";
            Session["UserId"] = user.Id;
            Session["UserName"] = user.Name;
            Response.Redirect("/");
        }
    }
}