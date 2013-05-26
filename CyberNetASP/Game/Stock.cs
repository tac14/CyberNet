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
		private static Stock thisInstance;
		public static string AgentName = "";

		public Stock(int a)
		{
			thisInstance = this;
		}
		public Stock()
		{
			int a = 1;
		}
		public static Stock GetInstance()
		{
			return thisInstance;
		}


		public DataTable GetStock()
		{
			return GetInstance().GetStockInner();
		}

		DataTable locList;

		private DataTable GetStockInner()
		{
			
			locList = new DataTable();
			
			locList.Columns.Add(new DataColumn("ID", typeof(int)));
			locList.Columns.Add(new DataColumn("ProductName", typeof(String)));
			locList.Columns.Add(new DataColumn("Count", typeof(String)));
			locList.Columns.Add(new DataColumn("Quality", typeof(String)));


			Database locDatabase = new Database();
			locDatabase.ConectDB("GetStock ('" + AgentName + "')", Reader);

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