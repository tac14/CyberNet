using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using MySql.Data.MySqlClient;

namespace CyberNet
{
	public class Market
	{
		private static Market thisInstance;
		public static string AgentName = "";

		public Market(int a)
		{
			thisInstance = this;
		}
		public Market()
		{
			int a = 1;
		}
		public static Market GetInstance()
		{
			return thisInstance;
		}


		public DataTable GetMarket()
		{
			return GetInstance().GetMarketInner();
		}

		DataTable locList;

		private DataTable GetMarketInner()
		{
			
			locList = new DataTable();

			locList.Columns.Add(new DataColumn("Column1", typeof(bool)));
			locList.Columns.Add(new DataColumn("Column2", typeof(bool)));
			locList.Columns.Add(new DataColumn("Column3", typeof(String)));
			locList.Columns.Add(new DataColumn("Column4", typeof(String)));
			locList.Columns.Add(new DataColumn("Column5", typeof(String)));
			locList.Columns.Add(new DataColumn("Column6", typeof(String)));
			locList.Columns.Add(new DataColumn("Column7", typeof(String)));


			Database locDatabase = new Database();
			locDatabase.ConectDB("GetMarket ('" + AgentName + "')", Reader);

			return locList;
		}

		public void Reader(object argReader, EventArgs e)
		{

			string id = ((MySqlDataReader)argReader)["ID"].ToString();

			DataRow dr = locList.NewRow();
			dr[0] = true;
			dr[1] = false;
			dr[2] = "меняю";
			dr[3] = ((MySqlDataReader)argReader)["ProductName"].ToString();
			dr[4] = ((MySqlDataReader)argReader)["Count"].ToString();
			dr[5] = ((MySqlDataReader)argReader)["Quality"].ToString();
			dr[6] = id;
			locList.Rows.Add(dr);

			Database locDatabase = new Database();
			locDatabase.ConectDB("GetExchangeOptions (" + id + ")", Reader2);


		}

		public void Reader2(object argReader, EventArgs e)
		{

			DataRow dr = locList.NewRow();
			dr[0] = false;
			dr[1] = true;
			dr[2] = "или на";
			dr[3] = ((MySqlDataReader)argReader)["ProductName"].ToString();
			dr[4] = ((MySqlDataReader)argReader)["MinCount"].ToString() + " +";
			dr[5] = ((MySqlDataReader)argReader)["MinQuality"].ToString() + " +";
			dr[6] = ((MySqlDataReader)argReader)["ID"].ToString(); 

	
			locList.Rows.Add(dr);

		}

		public DataTable GetAllMarket()
		{
			return GetInstance().GetAllMarketInner();
		}

		//DataTable locAllList;

		private DataTable GetAllMarketInner()
		{

			locList = new DataTable();

			locList.Columns.Add(new DataColumn("Column1", typeof(bool)));
			locList.Columns.Add(new DataColumn("Column2", typeof(bool)));
			locList.Columns.Add(new DataColumn("Column3", typeof(String)));
			locList.Columns.Add(new DataColumn("Column4", typeof(String)));
			locList.Columns.Add(new DataColumn("Column5", typeof(String)));
			locList.Columns.Add(new DataColumn("Column6", typeof(String)));
			locList.Columns.Add(new DataColumn("Column7", typeof(String)));
			locList.Columns.Add(new DataColumn("Column8", typeof(String)));


			Database locDatabase = new Database();
			locDatabase.ConectDB("GetAllMarket ()", ReaderAll);

			return locList;
		}

		public void ReaderAll(object argReader, EventArgs e)
		{

			string id = ((MySqlDataReader)argReader)["ID"].ToString();

			DataRow dr = locList.NewRow();
			dr[0] = true;
			dr[1] = false;
			dr[2] = "меняю";
			dr[3] = ((MySqlDataReader)argReader)["ProductName"].ToString();
			dr[4] = ((MySqlDataReader)argReader)["Count"].ToString();
			dr[5] = ((MySqlDataReader)argReader)["Quality"].ToString();
			dr[6] = id;
			dr[7] = ((MySqlDataReader)argReader)["AgentName"].ToString(); ;
			locList.Rows.Add(dr);

			Database locDatabase = new Database();
			locDatabase.ConectDB("GetExchangeOptions (" + id + ")", Reader2);


		}
	

	}
}