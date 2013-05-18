using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

using System.Data;

namespace CyberNet.Game
{
    public partial class StockLogic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Database db = new Database();

            db.ConectDB("GetStock('" + AgentState.SetName + "')", reader);

            Response.Write(str);

        }

        string str = "";

        protected void reader(object argReader, EventArgs e) {
            //Как здесь получить данные?
            MySqlDataReader r = (MySqlDataReader)argReader;
            str += r["name"].ToString();
            str += r["id"].ToString();
            str += r["quality"].ToString();
        }
    }
}