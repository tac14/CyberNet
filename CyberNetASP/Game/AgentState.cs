using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using MySql.Data.MySqlClient;

namespace CyberNet
{
	public class AgentState
	{
        private static AgentState CurrentInstance;

		private static ArrayList AllAgent = new ArrayList();

		public AgentState(string argUserName)
		{
			int Found = -1;
			for (int i = 0; i < AllAgent.Count; i++)
			{
				AgentState locAgentState = AllAgent[i] as AgentState;
				if (locAgentState.Name == argUserName)
				{
					Found = i;
					break;
				}
			}
			if (Found == -1)
			{
				AllAgent.Add(this);
				CurrentInstance = this;
			}
			else
			{
				CurrentInstance = AllAgent[Found] as AgentState;
			}

		}
		public AgentState()
		{
			int a = 1;
		}
		public static AgentState GetInstance(string argUserName)
		{
			int Found = -1;
			for (int i = 0; i < AllAgent.Count; i++)
			{
				AgentState locAgentState = AllAgent[i] as AgentState;
				if (locAgentState.Name == argUserName)
				{
					Found = i;
					break;
				}
			}
			if (Found != -1)
			{
				CurrentInstance = AllAgent[Found] as AgentState;
			}
			else
			{
				CurrentInstance = new AgentState(argUserName);
				//AllAgent.Add(CurrentInstance);
			}

			Plan.AgentName = CurrentInstance.Name;
			Market.AgentName = CurrentInstance.Name;

			return CurrentInstance;
		}
		public List<AgentState> GetState()
		{
			return CurrentInstance.GetStateInner();
		}

		private string name = "гость";
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		private string cityName;
		public string CityName
		{
			get { return cityName; }
		}
		private bool run = false;
		public bool NotRun
		{
			get { return !CurrentInstance.run; }
		}
		public bool Run
		{
			get { return CurrentInstance.run; }
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
		private string force = "1";
		public string Force
		{
			get { return force; }
		}
		private string intelligence = "1";
		public string Intelligence
		{
			get { return intelligence; }
		}
		private string currentDate;
		public string CurrentDate
		{
			get { return currentDate; }
		}

		private List<AgentState> GetStateInner()
		{
			List<AgentState> locList = new List<AgentState>();

			AgentState locAgentState = new AgentState();
			locAgentState.name = "Привет, " + Name + "!";

			if (Name != "гость")
			{
				Database locDatabase = new Database();
				locDatabase.ConectDB("GetAgentState ('" + Name + "')", Reader);
			}

			locList.Add(this);

			return locList;
		}

		public void Reader(object argReader, EventArgs e)
		{
			CurrentInstance.run = true;
			CurrentInstance.cityName = ((MySqlDataReader)argReader)["CityName"].ToString();
			CurrentInstance.energy = Convert.ToInt32(((MySqlDataReader)argReader)["Energy"]).ToString();
			CurrentInstance.health = Convert.ToInt32(((MySqlDataReader)argReader)["Health"]).ToString();
			CurrentInstance.cheerfulness = Convert.ToInt32(((MySqlDataReader)argReader)["Cheerfulness"]).ToString();
			CurrentInstance.force = Convert.ToInt32(((MySqlDataReader)argReader)["Force"]).ToString();
			CurrentInstance.intelligence = Convert.ToInt32(((MySqlDataReader)argReader)["Intelligence"]).ToString();
			CurrentInstance.currentDate = ((MySqlDataReader)argReader)["CurrentDate"].ToString();
		}

	}
}