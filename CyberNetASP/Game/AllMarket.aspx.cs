using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberNet.Game
{
	public partial class AllMarket : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Market.GetInstance() == null)
			{
				Market locMarket = new Market(1);
			}
		}
	}
}