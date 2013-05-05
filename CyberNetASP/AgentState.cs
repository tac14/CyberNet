using System;
using System.Collections.Generic;
using System.Web;
using MySql.Data.MySqlClient;

namespace CyberNet
{
	public class AgentState
	{

        private static AgentState thisInstance;

		public AgentState()
		{
			thisInstance = this;
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
			set { name = value; }
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
			energy = ((MySqlDataReader)argReader)["Energy"].ToString();
			health = ((MySqlDataReader)argReader)["Health"].ToString();
			force = ((MySqlDataReader)argReader)["Force"].ToString();
			intelligence = ((MySqlDataReader)argReader)["Intelligence"].ToString();
		}

	}
}