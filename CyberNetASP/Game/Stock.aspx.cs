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
    public partial class Stock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataView dv = getStock();
            GridView1.DataSource = dv;
            GridView1.DataBind();
        }

        DataTable dt = new DataTable();
        public DataView getStock()
        {
            
            Database db = new Database();

            dt.Columns.Add(new DataColumn("Название", typeof(String)));
            dt.Columns.Add(new DataColumn("Количество", typeof(String)));
            dt.Columns.Add(new DataColumn("Качество", typeof(String)));

            db.ConectDB("GetStock('" + AgentState.SetName + "')", reader);

            DataBind();
            
            return new DataView(dt);
        }


        protected void reader(object argReader, EventArgs e)
        {
            string name = ((MySqlDataReader)argReader)["name"].ToString();
            string count = ((MySqlDataReader)argReader)["count"].ToString();
            string quality = ((MySqlDataReader)argReader)["quality"].ToString();
            dt.Rows.Add(CreateRow(name, count, quality, dt));
        }

        DataRow CreateRow(String name, String count, String quality, DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr[0] = name;
            dr[1] = count;
            dr[2] = quality;
            return dr;
        }
    }
}