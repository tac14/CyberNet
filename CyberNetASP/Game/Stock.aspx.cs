using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CyberNet;
using System.Data;

namespace CyberNet.Game
{
	public partial class StockLayout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Stock locStock = new Stock();
            DataTable dv = locStock.GetStock(AgentState.GetInstance((string)Session["UserName"]).Name);
            StockList.DataSource = dv;
            StockList.DataBind();
            
        }
    }
}