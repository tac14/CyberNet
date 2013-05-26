using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


namespace CyberNet
{


	public partial class SiteMaster : System.Web.UI.MasterPage
	{
		public void SetUser(string argName)
		{
			AgentState locAgent = new AgentState(argName);
			AgentState.GetInstance(argName).Name = argName;
			DataBind();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AgentState.GetInstance((string)Session["UserName"]);

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
			locDB.Exec("CalcStep('" + AgentState.GetInstance((string)Session["UserName"]).Name + "')");
			DataBind();
		}
		public void Reset(Object sender, EventArgs e)
		{
			Database locDB = new Database();
			locDB.Exec("Reset('" + AgentState.GetInstance((string)Session["UserName"]).Name + "')");
			DataBind();
		}
		public void LoggedIn(Object sender, EventArgs e)
		{
			Response.Redirect("~/Account/Login.aspx");
		}
		public void LoggedOut(Object sender, EventArgs e)
		{
			// Пользователь выполнил вход, поэтому необходимо выполнить выход.
				SetUser("гость");
				Session["UserName"] = "гость";
				FormsAuthentication.SignOut();
				Response.Redirect("~/");
		}



	}
}
