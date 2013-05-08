using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using MySql.Data.MySqlClient;

namespace CyberNet
{
	public class Plan
	{
		private int id;
		public int ID
		{
			get { return id; }
			set { id = value; }
		}
		private int seqNumber;
		public int SeqNumber
		{
			get { return seqNumber; }
			set { seqNumber = value; }
		}

		private string productName;
		public string ProductName
		{
			get { return productName; }
			set { productName = value; }
		}
		private string optionsID;
		public string OptionsID
		{
			get { return optionsID; }
			set { optionsID = value; }
		}

		private static Plan thisInstance;
		public static string AgentName = "";

		public Plan(int a)
		{
			thisInstance = this;
		}
		public Plan()
		{
			int a = 1;
		}
		public static Plan GetInstance()
		{
			return thisInstance;
		}


		public DataTable GetPlan()
		{
			return GetInstance().GetPlanInner();
		}

		DataTable locList = new DataTable();

		private DataTable GetPlanInner()
		{

			locList.Columns.Add(new DataColumn("ID", typeof(int)));
			locList.Columns.Add(new DataColumn("SeqNumber", typeof(int)));
			locList.Columns.Add(new DataColumn("ProductName", typeof(String)));
			locList.Columns.Add(new DataColumn("OptionsID", typeof(String)));


			Database locDatabase = new Database();
			locDatabase.ConectDB("GetAgentPlan ('" + AgentName + "')", Reader);

			return locList;
		}

		public void Reader(object argReader, EventArgs e)
		{
			DataRow dr = locList.NewRow();
			dr[0] = Convert.ToInt32(((MySqlDataReader)argReader)["ID"]);
			dr[1] = Convert.ToInt32(((MySqlDataReader)argReader)["SeqNumber"]);
			dr[2] = ((MySqlDataReader)argReader)["ProductName"].ToString();
			dr[3] = ((MySqlDataReader)argReader)["OptionsID"].ToString();

			locList.Rows.Add(dr);
		}

		public bool SaveList(int ID, int SeqNumber, string ProductName, string OptionsID)
		{
			return true;
		}

	}
}