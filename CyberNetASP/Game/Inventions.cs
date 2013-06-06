using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using MySql.Data.MySqlClient;

namespace CyberNet
{
	public class Inventions
	{
		private static Inventions thisInstance;
		public static string AgentName = "";

		public string CurrentProductID;
		public string LicenseType ="0";

		public Inventions(int a)
		{
			thisInstance = this;
		}
		public Inventions()
		{
			int a = 1;
		}
		public static Inventions GetInstance()
		{
			return thisInstance;
		}


		public DataTable GetFastAction()
		{
			return GetInstance().GetFastActionInner();
		}

		DataTable locList;

		private DataTable GetFastActionInner()
		{
			
			locList = new DataTable();

			locList.Columns.Add(new DataColumn("Column1", typeof(String)));
			locList.Columns.Add(new DataColumn("Column2", typeof(String)));
			locList.Columns.Add(new DataColumn("Column3", typeof(String)));
			locList.Columns.Add(new DataColumn("Column4", typeof(String)));
			locList.Columns.Add(new DataColumn("Column5", typeof(String)));


			Database locDatabase = new Database();
			locDatabase.ConectDB("GetReceivingProduct ('" + AgentName + "', " + CurrentProductID + ")", Reader);

			return locList;
		}

		public void Reader(object argReader, EventArgs e)
		{

			DataRow dr = locList.NewRow();
			dr[0] = ((MySqlDataReader)argReader)["ProductName"].ToString();
			dr[1] = ((MySqlDataReader)argReader)["ActionName"].ToString();
			dr[2] = ((MySqlDataReader)argReader)["RawName"].ToString();
			dr[3] = ((MySqlDataReader)argReader)["CountIndex"].ToString();
			dr[4] = ((MySqlDataReader)argReader)["ID"].ToString();

			locList.Rows.Add(dr);

		}

		public DataTable GetIfAction()
		{
			return GetInstance().GetIfActionInner();
		}


		private DataTable GetIfActionInner()
		{

			locList = new DataTable();

			locList.Columns.Add(new DataColumn("Column1", typeof(String)));
			locList.Columns.Add(new DataColumn("Column2", typeof(String)));
			locList.Columns.Add(new DataColumn("Column3", typeof(String)));
			locList.Columns.Add(new DataColumn("Column4", typeof(String)));
			locList.Columns.Add(new DataColumn("Column5", typeof(String)));


			Database locDatabase = new Database();
			locDatabase.ConectDB("GetIfAction ('" + AgentName + "', " + CurrentProductID + ")", ReaderIfAction);

			return locList;
		}

		public void ReaderIfAction(object argReader, EventArgs e)
		{

			DataRow dr = locList.NewRow();
			dr[0] = ((MySqlDataReader)argReader)["ID"].ToString();
			dr[1] = ((MySqlDataReader)argReader)["RawName"].ToString();
			dr[2] = ((MySqlDataReader)argReader)["CountIndex"].ToString();
			dr[3] = ((MySqlDataReader)argReader)["ID2"].ToString();
			dr[4] = ((MySqlDataReader)argReader)["FromOptionsID"].ToString();

			locList.Rows.Add(dr);

		}


	}
}