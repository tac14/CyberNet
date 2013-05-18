using System;
using System.Collections.Generic;
using System.Web;
using MySql.Data.MySqlClient;

namespace CyberNet
{
	public class AgentState
	{
        private static AgentState thisInstance;

		public AgentState(int a)
		{
			name = "Привет, " + SetName + "!";
			thisInstance = this;
		}
		public AgentState()
		{
			int a = 1;
		}
		public static AgentState GetInstance()
		{
			return thisInstance;
		}
		public List<AgentState> GetState()
		{
			return GetInstance().GetStateInner();
		}

		public static string SetName = "гость";

		private string name;
		public string Name
		{
			get { return name; }
		}
		private string cityName;
		public string CityName
		{
			get { return cityName; }
		}
		private bool run = false;
		public bool NotRun
		{
			get { return !run; }
		}
		public bool Run
		{
			get { return run; }
		}

		private string energy = "100";
		public string Energy
		{
			get { return energy; }
		}
		private string health = "100";
		public string Health
		{
			get { return health; }
		}
		private string cheerfulness = "100";
		public string Cheerfulness
		{
			get { return cheerfulness; }
		}
		private string force = "0";
		public string Force
		{
			get { return force; }
		}
		private string intelligence = "0";
		public string Intelligence
		{
			get { return intelligence; }
		}

		private List<AgentState> GetStateInner()
		{
			List<AgentState> locList = new List<AgentState>();

			AgentState locAgentState = new AgentState();
			locAgentState.name = "Привет, " + SetName + "!";

			if (SetName != "гость")
			{
				Database locDatabase = new Database();
				locDatabase.ConectDB("GetAgentState ('" + SetName + "')", Reader);
			}

			locList.Add(this);

			return locList;
		}

		public void Reader(object argReader, EventArgs e)
		{
			run = true;
			cityName = ((MySqlDataReader)argReader)["CityName"].ToString();
			energy = Convert.ToInt32(((MySqlDataReader)argReader)["Energy"]).ToString();
			health = Convert.ToInt32(((MySqlDataReader)argReader)["Health"]).ToString();
			cheerfulness = Convert.ToInt32(((MySqlDataReader)argReader)["Cheerfulness"]).ToString();
			force = Convert.ToInt32(((MySqlDataReader)argReader)["Force"]).ToString();
			intelligence = Convert.ToInt32(((MySqlDataReader)argReader)["Intelligence"]).ToString();
		}

	}
}