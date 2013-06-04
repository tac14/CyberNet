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
			locList.Columns.Add(new DataColumn("Column8", typeof(String)));
			locList.Columns.Add(new DataColumn("Column9", typeof(bool)));
			locList.Columns.Add(new DataColumn("Column10", typeof(bool)));


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
			dr[7] = "";

			int locIsInStock = Convert.ToInt32(((MySqlDataReader)argReader)["IsInStock"]);
			if (locIsInStock == 1)
			{
				dr[8] = true;
				dr[9] = false;
			}
			else
			{
				dr[8] = false;
				dr[9] = true;
			}
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

			dr[7] = "";
			dr[8] = false;
			dr[9] = false;

			locList.Rows.Add(dr);

		}

		public DataTable GetAllMarket()
		{
			return GetInstance().GetAllMarketInner();
		}

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
			locList.Columns.Add(new DataColumn("Column9", typeof(bool)));
			locList.Columns.Add(new DataColumn("Column10", typeof(bool)));


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
			dr[7] = ((MySqlDataReader)argReader)["AgentName"].ToString();

			int locIsInStock = Convert.ToInt32(((MySqlDataReader)argReader)["IsInStock"]);
			if (locIsInStock == 1)
			{
				dr[8] = true;
				dr[9] = false;
			}
			else
			{
				dr[8] = false;
				dr[9] = true;
			}
			locList.Rows.Add(dr);

			Database locDatabase = new Database();
			locDatabase.ConectDB("GetExchangeOptions (" + id + ")", Reader2);


		}

		public DataTable GetExchangeVariant()
		{
			return GetInstance().GetExchangeVariantInner();
		}

		private DataTable GetExchangeVariantInner()
		{

			locList = new DataTable();

			locList.Columns.Add(new DataColumn("Column1", typeof(String)));
			locList.Columns.Add(new DataColumn("Column2", typeof(String)));
			locList.Columns.Add(new DataColumn("Column3", typeof(String)));
			locList.Columns.Add(new DataColumn("Column4", typeof(double)));
			locList.Columns.Add(new DataColumn("Column5", typeof(double)));
			locList.Columns.Add(new DataColumn("Column6", typeof(String)));
			locList.Columns.Add(new DataColumn("Column7", typeof(String)));
			locList.Columns.Add(new DataColumn("Column8", typeof(String)));
			locList.Columns.Add(new DataColumn("Column9", typeof(double)));
			locList.Columns.Add(new DataColumn("Column10", typeof(double)));
			locList.Columns.Add(new DataColumn("eID2", typeof(String)));


			Database locDatabase = new Database();
			locDatabase.ConectDB("CalcMarket ('" + AgentName + "')", ReaderExchange);

			return locList;
		}

		public void ReaderExchange(object argReader, EventArgs e)
		{

			DataRow dr = locList.NewRow();
			dr[0] = ((MySqlDataReader)argReader)["ID"].ToString(); 
			dr[1] = ((MySqlDataReader)argReader)["AgentName"].ToString(); 
			dr[2] = ((MySqlDataReader)argReader)["ProductName"].ToString(); 
			dr[3] = Convert.ToDouble(((MySqlDataReader)argReader)["Count"]).ToString("F4") ;
			dr[4] = Convert.ToDouble(((MySqlDataReader)argReader)["Quality"]).ToString("F2");

			dr[5] = ((MySqlDataReader)argReader)["ID2"].ToString(); 
			dr[6] = ((MySqlDataReader)argReader)["AgentName2"].ToString(); 
			dr[7] = ((MySqlDataReader)argReader)["ProductName2"].ToString(); 
			dr[8] = Convert.ToDouble(((MySqlDataReader)argReader)["Count2"]).ToString("F4");
			dr[9] = Convert.ToDouble(((MySqlDataReader)argReader)["Quality2"]).ToString("F2");
			
			dr[10] = ((MySqlDataReader)argReader)["eID2"].ToString();

			locList.Rows.Add(dr);

		}


	}
}