using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CyberNet;

namespace CyberNet.Game
{
	public partial class ActiveUsersLayout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ActiveUsers locActiveUsers = new ActiveUsers();
			ActiveUsersList.DataSource = locActiveUsers.GetActiveUsers();
			ActiveUsersList.DataBind();
		}
	}
}