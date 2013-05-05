using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTest
{
	public class AgentState
	{
		public static string SetName = "гость";

		private string name;
		public string Name
		{
			get { return name; }
		}
		private string energy;
		public string Energy
		{
			get { return energy; }
		}

		public static List<AgentState> GetState()
		{
			List<AgentState> locList = new List<AgentState>();

			AgentState locAgentState = new AgentState();
			locAgentState.name = "Привет, " + SetName + "!";
			locAgentState.energy = "80";
			locList.Add(locAgentState);

			return locList;
		}	
	}


	public partial class SiteMaster : System.Web.UI.MasterPage
	{
		public void SetUser(string argName)
		{
			AgentState.SetName = argName;
			DataBind();
		}

		protected void Page_Load(object sender, EventArgs e)
		{

			//if (PreviousPage != null)
			//{
			//    Button button = (Button)PreviousPage.FindControl("Button1");
			//    Label1.Text = "Вызов выполнен кнопкой: " + button.Text;
			//    TextBox textBox = (TextBox)PreviousPage.FindControl("TextBox1");
			//    TextBox1.Text = textBox.Text;
			//}
		}
	}
}
