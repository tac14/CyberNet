﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CyberNet;

namespace WebTest
{


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
