using CommunityHubb.Models;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CommunityHubb.Account
{
    public partial class Update : System.Web.UI.Page
    {
        User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Session["fmsg"] = "Please login to update profile";
                Response.Redirect("~/");
            }
            else
            {
                int userId = (int)Session["UserId"];
                CommunityHubbDBEntities db = new CommunityHubbDBEntities();
                user = db.Users.Where(u => u.Id == userId).FirstOrDefault();

                if (!IsPostBack)
                {
                    numberbox.Text = user.Phone;
                    dobbox.Text = user.Dob.GetValueOrDefault().ToString("yyyy-MM-dd");
                    aboutbox.Text = user.About;
                }
            }

        }

        protected void UpdateDetails(object sender, EventArgs e)
        {
            String about = aboutbox.Text;
            String mobilenumber = numberbox.Text;
            DateTime? dateofbirth = string.IsNullOrEmpty(dobbox.Text) ? (DateTime?)null : DateTime.Parse(dobbox.Text);
            user.About = about;
            user.Phone = mobilenumber;
            user.Dob = dateofbirth;
            CommunityHubbDBEntities db = new CommunityHubbDBEntities();
            db.Users.AddOrUpdate(user);
            db.SaveChanges();
            Session["smsg"] = "Details updated successfully";
            Response.Redirect("/Account/Profile");
        }
    }
}