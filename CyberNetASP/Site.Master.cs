using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CyberNet
{


	public partial class SiteMaster : System.Web.UI.MasterPage
	{
		public void SetUser(string argName)
		{
			Plan.AgentName = argName;
			AgentState.SetName = argName;
			DataBind();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			//if (AgentState.GetInstance() == null)
			{
				AgentState locAgent = new AgentState(1);
			}

			//if (PreviousPage != null)
			//{
			//    Button button = (Button)PreviousPage.FindControl("Button1");
			//    Label1.Text = "Вызов выполнен кнопкой: " + button.Text;
			//    TextBox textBox = (TextBox)PreviousPage.FindControl("TextBox1");
			//    TextBox1.Text = textBox.Text;
			//}
		}

		public void NextStep(Object sender, EventArgs e)
		{
			Database locDB = new Database();
			locDB.Exec("CalcStep('" + AgentState.SetName + "')");
			DataBind();
		}
		public void Reset(Object sender, EventArgs e)
		{
			Database locDB = new Database();
			locDB.Exec("Reset('" + AgentState.SetName + "')");
			DataBind();
		}

	}
}
