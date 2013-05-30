using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CyberNet;

namespace CyberNet.Game
{
	public partial class MarketLayout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Market.GetInstance() == null)
			{
				Market locStock = new Market(1);
			}
		}

		public void ProductFromStockChange(Object sender, EventArgs e)
		{
		}

		public void AddLot(Object sender, EventArgs e)
		{
		}

		public void LotChange(Object sender, EventArgs e)
		{
		}

		public void ProductChange(Object sender, EventArgs e)
		{
		}

		public void AddLotVariant(Object sender, EventArgs e)
		{
		}
	}
}