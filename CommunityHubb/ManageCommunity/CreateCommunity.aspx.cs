using System;

namespace CommunityHubb.ManageCommunity
{
    public partial class CreateCommunity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Session["fmsg"] = "Please login to continue";
                Response.Redirect("~/");
            }
        }

        protected void CreateClick(object sender, EventArgs e)
        {
            String name = namebox.Text;
            String description = TextBox1.Text;
            String visibility = RadioButtonList1.SelectedValue;
            //Response.Write(name + description + visibility);
            Community community = new Community();
            community.Name = name;
            community.Description = description;
            community.CreatedDate = DateTime.Now;
            community.isPrivate = visibility.Equals("Private");
            CommunityHubbDBEntities communityHubbDBEntities = new CommunityHubbDBEntities();
            communityHubbDBEntities.Communities.Add(community);
            communityHubbDBEntities.SaveChanges();

            CommunityUser communityUser = new CommunityUser();
            communityUser.CommunityId = community.Id;
            communityUser.UserId = Convert.ToInt32(Session["UserId"]);
            communityUser.IsAdmin = true;
            communityHubbDBEntities.CommunityUsers.Add(communityUser);
            communityHubbDBEntities.SaveChanges();

            Response.Redirect("~/ManageCommunity/CommunityHome.aspx?id=" + community.Id);
        }
    }
}