﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;

namespace CyberNet.Account
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);


			// Пользователь выполнил вход, поэтому необходимо выполнить выход.
			if (User.Identity.IsAuthenticated)
			{
				Master.SetUser("гость");
				FormsAuthentication.SignOut();
				Response.Redirect("~/");
				//Session["UserName"] = LoginUser.UserName;
			}
		}

		protected void LoginUser_LoggedIn(object sender, EventArgs e)
		{
			Session["UserName"] = LoginUser.UserName;

			Master.SetUser(LoginUser.UserName);


		}
	}
}
