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
			if (Context.User.Identity.Name != "")
			{
				Session["UserName"] = Context.User.Identity.Name;

				AgentState locState = AgentState.GetInstance((string)Session["UserName"]);

				// Это случай когда регистрация уже произошла
				if (locState.Name != Context.User.Identity.Name)
				{
					locState.Name = Context.User.Identity.Name;
					DataBind();
				}
				if (locState.BonusStepCountInt > 0)
				{
					RptArt.Items[0].FindControl("BonusButton").Visible = true;
				}
				else
				{
					RptArt.Items[0].FindControl("BonusButton").Visible = false;
				}
				if (locState.Dead == "1")
				{
					RptArt.Items[0].FindControl("Dead").Visible = true;
				}

			}
			else
			{ 
			
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
			locDB.Exec("CalcStep('" + AgentState.GetInstance((string)Session["UserName"]).Name + "', true)");
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
