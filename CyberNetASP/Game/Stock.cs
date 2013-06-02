using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using MySql.Data.MySqlClient;

namespace CyberNet
{
	public class Stock
	{
		public Stock()
		{
		}

        public DataTable GetStock(string agentName)
		{
            return GetStockInner(agentName);
		}

		DataTable locList;

        private DataTable GetStockInner(string agentName)
		{
			
			locList = new DataTable();
			
			locList.Columns.Add(new DataColumn("ID", typeof(int)));
			locList.Columns.Add(new DataColumn("ProductName", typeof(String)));
			locList.Columns.Add(new DataColumn("Count", typeof(String)));
			locList.Columns.Add(new DataColumn("Quality", typeof(String)));


			Database locDatabase = new Database();
			locDatabase.ConectDB("GetStock ('" + agentName + "')", Reader);

			return locList;
		}

		public void Reader(object argReader, EventArgs e)
		{
			
			DataRow dr = locList.NewRow();
			dr[0] = Convert.ToInt32(((MySqlDataReader)argReader)["ID"]);
			dr[1] = ((MySqlDataReader)argReader)["ProductName"].ToString();
			dr[2] = Convert.ToDouble(((MySqlDataReader)argReader)["Count"]).ToString("F4");
			dr[3] = Convert.ToDouble(((MySqlDataReader)argReader)["Quality"]).ToString("F2");
			locList.Rows.Add(dr);
		}

	}
}