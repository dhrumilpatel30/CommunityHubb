using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunityHubb
{
    public partial class userslist : System.Web.UI.UserControl
    {
        public String DataSourceID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Parent.FindControl(DataSourceID) is SqlDataSource sqlDataSource)
            {
                userlistinchild.DataSource = sqlDataSource;
                userlistinchild.DataBind();
            }

        }
    }
}