using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CyberNet;

namespace CyberNet.Game
{
	public partial class StockLayout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if (Stock.GetInstance() == null)
			{
				Stock locStock = new Stock(1);
			}
            
        }
    }
}