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
			energy = ((MySqlDataReader)argReader)["Energy"].ToString();
			health = ((MySqlDataReader)argReader)["Health"].ToString();
			force = ((MySqlDataReader)argReader)["Force"].ToString();
			intelligence = ((MySqlDataReader)argReader)["Intelligence"].ToString();
		}

	}
}