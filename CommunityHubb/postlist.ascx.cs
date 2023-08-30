using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunityHubb
{
    public partial class postlist : System.Web.UI.UserControl
    {
        public String DataSourceID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource sqlDataSource = this.Parent.FindControl(DataSourceID) as SqlDataSource;
            if (sqlDataSource != null)
            {
                postlistinchild.DataSource = sqlDataSource;
                postlistinchild.DataBind();
            }
        }
    }
}