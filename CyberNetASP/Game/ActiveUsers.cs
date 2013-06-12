using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using MySql.Data.MySqlClient;

namespace CyberNet
{
	public class ActiveUsers
	{
		public ActiveUsers()
		{
		}

		public DataTable GetActiveUsers()
		{
			return ActiveUsersInner();
		}

		DataTable locList;

		private DataTable ActiveUsersInner()
		{
			
			locList = new DataTable();

			locList.Columns.Add(new DataColumn("AgentName", typeof(String)));
			locList.Columns.Add(new DataColumn("LastActionDate", typeof(String)));
			locList.Columns.Add(new DataColumn("CityName", typeof(String)));
			locList.Columns.Add(new DataColumn("CountryName", typeof(String)));


			Database locDatabase = new Database();
			locDatabase.ConectDB("GetAgentStatistic ()", Reader);

			return locList;
		}

		public void Reader(object argReader, EventArgs e)
		{
			
			DataRow dr = locList.NewRow();
			dr[0] = ((MySqlDataReader)argReader)["AgentName"].ToString();
			dr[1] = ((MySqlDataReader)argReader)["LastActionDate"].ToString();
			dr[2] = ((MySqlDataReader)argReader)["CityName"].ToString();
			dr[3] = ((MySqlDataReader)argReader)["CountryName"].ToString();
			locList.Rows.Add(dr);
		}

	}
}