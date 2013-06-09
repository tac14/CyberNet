using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace CyberNet.Game
{
	public partial class Inventions2 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Inventions.GetInstance() == null)
			{
				Inventions locInventions = new Inventions(1);
			}

			int OldIndex3 = ActionList2.SelectedIndex;
			ActionList2.DataSource = CreateActionListDataSource();
			ActionList2.DataTextField = "TextField";
			ActionList2.DataValueField = "ValueField";
			ActionList2.DataBind();
			ActionList2.SelectedIndex = OldIndex3;

			int OldIndex5 = ProductList.SelectedIndex;
			ProductList.DataSource = CreateProductListDataSource();
			ProductList.DataTextField = "TextField";
			ProductList.DataValueField = "ValueField";
			ProductList.DataBind();
			ProductList.SelectedIndex = OldIndex5;
			int OldIndex6 = ProductList2.SelectedIndex;
			ProductList2.DataSource = CreateProductListDataSource2();
			ProductList2.DataTextField = "TextField";
			ProductList2.DataValueField = "ValueField";
			ProductList2.DataBind();
			ProductList2.SelectedIndex = OldIndex6;
			int OldIndex7 = ProductList3.SelectedIndex;
			ProductList3.DataSource = CreateProductListDataSource2();
			ProductList3.DataTextField = "TextField";
			ProductList3.DataValueField = "ValueField";
			ProductList3.DataBind();
			ProductList3.SelectedIndex = OldIndex7;

			int OldIndex8 = IfActionsNumberList1.SelectedIndex;
			IfActionsNumberList1.DataSource = CreateIfActionsNumberListDataSource2();
			IfActionsNumberList1.DataTextField = "TextField";
			IfActionsNumberList1.DataValueField = "ValueField";
			IfActionsNumberList1.DataBind();
			IfActionsNumberList1.SelectedIndex = OldIndex8;

			int OldIndex9 = LicenseTypeList.SelectedIndex;
			LicenseTypeList.DataSource = CreateLicenseTypeSource();
			LicenseTypeList.DataTextField = "TextField";
			LicenseTypeList.DataValueField = "ValueField";
			LicenseTypeList.DataBind();
			LicenseTypeList.SelectedIndex = OldIndex9;


		}

		DataView CreateLicenseTypeSource()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("TextField", typeof(String)));
			dt.Columns.Add(new DataColumn("ValueField", typeof(String)));

			dt.Rows.Add(CreateRow("Общественное достояние", "0", dt));
			dt.Rows.Add(CreateRow("Эксклюзивное право на 1 год", "1", dt));
			dt.Rows.Add(CreateRow("Эксклюзивное право на 2 года", "2", dt));
			dt.Rows.Add(CreateRow("Эксклюзивное право на 3 года", "3", dt));
			dt.Rows.Add(CreateRow("Эксклюзивное право на 4 года", "4", dt));
			dt.Rows.Add(CreateRow("Эксклюзивное право на 5 лет", "5", dt));

			DataView dv = new DataView(dt);
			return dv;
		}

		DataRow CreateRow(String Text, String Value, DataTable dt)
		{
			DataRow dr = dt.NewRow();
			dr[0] = Text;
			dr[1] = Value;
			return dr;
		}

		DataView CreateActionListDataSource()
		{
			Database locDB = new Database();
			return locDB.GetDataSource("GetAllActions ('" + (string)Session["UserName"] + "')");
		}
		DataView CreateProductListDataSource()
		{
			Database locDB = new Database();
			return locDB.GetDataSource("GetAgentProduct ('" + (string)Session["UserName"] + "')");
		}
		DataView CreateProductListDataSource2()
		{
			Database locDB = new Database();
			return locDB.GetDataSource("GetAgentProduct2 ('" + (string)Session["UserName"] + "')");
		}
		DataView CreateIfActionsNumberListDataSource2()
		{
			Database locDB = new Database();
			return locDB.GetDataSource("GetIfActionsNumber ('" + (string)Session["UserName"] + "', " + 
											Inventions.GetInstance().CurrentProductID + ")");
		}

		public void ActionListChange(Object sender, EventArgs e)
		{
		}
		public void OnCheckFastAction(Object sender, EventArgs e)
		{

			if (IsIfAction.Checked == true)
			{
				ProductList2.Enabled = false;
				CountIndex1.Enabled = false;
				ProductList2.SelectedIndex = -1;
				CountIndex1.Text = "0.00";
			}
			else
			{
				ProductList2.Enabled = true;
				CountIndex1.Enabled = true;
				CountIndex1.Text = "1.00";
			}

		}

		public void ProductListChange(Object sender, EventArgs e)
		{
			if (ProductList.SelectedItem != null)
			{
				string locProductListID = ProductList.SelectedItem.Value;

				Inventions.GetInstance().CurrentProductID = locProductListID;
				Inventions.GetInstance().GetFastAction();
				Database locDB = new Database();
				locDB.ConectDB("CalcLicenseCost( '" + (string)Session["UserName"] + "', " + Inventions.GetInstance().CurrentProductID +
													", " + Inventions.GetInstance().LicenseType + ")", ReaderCost);
				DataBind();
			}
			IfActionsNumberList1.DataSource = CreateIfActionsNumberListDataSource2();
			IfActionsNumberList1.DataBind();
		}

		public void LicenseTypeChange(Object sender, EventArgs e)
		{
			if (LicenseTypeList.SelectedItem != null)
			{
				Inventions.GetInstance().LicenseType = LicenseTypeList.SelectedItem.Value;
				Database locDB = new Database();
				locDB.ConectDB("CalcLicenseCost( '" + (string)Session["UserName"] + "', " + Inventions.GetInstance().CurrentProductID +
													", " + Inventions.GetInstance().LicenseType + ")", ReaderCost);
			}
		}
	
		public void RawListChange1(Object sender, EventArgs e)
		{
		}
		public void RawListChange2(Object sender, EventArgs e)
		{
		}

		public void AddFastAction(Object sender, EventArgs e)
		{
			if ( (IsIfAction.Checked ==false && ProductList.SelectedItem != null && ProductList2.SelectedItem != null &&
				ActionList2.SelectedItem != null && CountIndex1.Text != "") ||
				(IsIfAction.Checked == true && ProductList.SelectedItem != null && ActionList2.SelectedItem != null))
			{

				string locProductListID = ProductList.SelectedItem.Value;
				string locProductList2ID = ProductList2.SelectedItem.Value;
				string locActionList2ID = ActionList2.SelectedItem.Value;

				if (IsIfAction.Checked == true && locProductList2ID == "-")
				{
					locProductList2ID = "0";
				}


				if ( (IsIfAction.Checked ==false && locProductListID != "" && locProductList2ID != "" && locActionList2ID != "") ||
						(IsIfAction.Checked == true && locProductListID != "" && locActionList2ID != ""))
				{

					Database locDB = new Database();
					locDB.ConectDB("AddReceivingProduct( '" + (string)Session["UserName"] + "', " + locProductListID + ", " +
													locActionList2ID + ", " + locProductList2ID + ", " + CountIndex1.Text + ", " +
													IsIfAction.Checked + ")", null);

					locDB.ConectDB("CalcLicenseCost( '" + (string)Session["UserName"] + "', " + Inventions.GetInstance().CurrentProductID +
														", " + Inventions.GetInstance().LicenseType + ")", ReaderCost);
					
					Inventions.GetInstance().GetFastAction();
					DataBind();
					IfActionsNumberList1.DataSource = CreateIfActionsNumberListDataSource2();
					IfActionsNumberList1.DataBind();
				}
			}

		}

		public void ReaderCost(object argReader, EventArgs e)
		{
			Cost.Text = ((MySqlDataReader)argReader)["Cost"].ToString();

			// Status, LicenseType, GivingDate, ExpiresDate ;
			string locStatus = ((MySqlDataReader)argReader)["Status"].ToString();
			if (locStatus != "")
			{
				string locLicenseType = ((MySqlDataReader)argReader)["LicenseType"].ToString();
				string locGivingDate = ((MySqlDataReader)argReader)["GivingDate"].ToString();
				string locExpiresDate = ((MySqlDataReader)argReader)["ExpiresDate"].ToString();

				LicenseStatus.Visible = true;

				if (locLicenseType == "0")
				{
					LicenseStatus.Text = "Ваша лицензия " + locStatus;
				}
				else
				{
					LicenseStatus.Text = "Ваша лицензия " + locStatus + " (получена " + locGivingDate + "/истекает)" + locExpiresDate;
				
				}

			}

		}

		public void DeleteFastActions(Object sender, EventArgs e)
		{
			for (int i = 0; i < FastActionList1.Items.Count; i++)
			{
				CheckBox locCheckBox = FastActionList1.Items[i].FindControl("CheckFastAction") as CheckBox;
				if (locCheckBox.Checked == true)
				{
					Label locLabelN1 = FastActionList1.Items[i].FindControl("N1") as Label;

					Database locDB = new Database();
					locDB.Exec("DeleteFastAction (" + locLabelN1.Text + ")");
					locDB.ConectDB("CalcLicenseCost( '" + (string)Session["UserName"] + "', " + Inventions.GetInstance().CurrentProductID +
														", " + Inventions.GetInstance().LicenseType + ")", ReaderCost);
				}
			}
			Inventions.GetInstance().GetFastAction();
			DataBind();
			IfActionsNumberList1.DataSource = CreateIfActionsNumberListDataSource2();
			IfActionsNumberList1.DataBind();
		}

		public void ActionIFListChange(Object sender, EventArgs e)
		{
		}

		public void AddIFAction(Object sender, EventArgs e)
		{
			if (IfActionsNumberList1.SelectedItem != null && ProductList3.SelectedItem != null &&
				 CountIndex2.Text != "" )
			{

				string locIfActionsNumber = IfActionsNumberList1.SelectedItem.Value;
				string locProductList3ID = ProductList3.SelectedItem.Value;


				if (locIfActionsNumber != "" && locProductList3ID != "")
				{

					Database locDB = new Database();
					locDB.ConectDB("AddIfAction( '" + (string)Session["UserName"] + "', " + Inventions.GetInstance().CurrentProductID + ", " +
													locIfActionsNumber + ", " + locProductList3ID + ", " + CountIndex2.Text + ")", null);
					locDB.ConectDB("CalcLicenseCost( '" + (string)Session["UserName"] + "', " + Inventions.GetInstance().CurrentProductID +
														", " + Inventions.GetInstance().LicenseType + ")", ReaderCost);

					Inventions.GetInstance().GetIfAction();
					DataBind();
				}
			}
		}

		public void DeleteIfActions(Object sender, EventArgs e)
		{
			for (int i = 0; i < IfActionList1.Items.Count; i++)
			{
				CheckBox locCheckBox = IfActionList1.Items[i].FindControl("CheckIfAction") as CheckBox;
				if (locCheckBox.Checked == true)
				{
					Label locLabelN1 = IfActionList1.Items[i].FindControl("N1") as Label;
					Label locLabelN2 = IfActionList1.Items[i].FindControl("N2") as Label;
					//Label locLabelN3 = IfActionList1.Items[i].FindControl("N3") as Label;

					Database locDB = new Database();
					locDB.Exec("DeleteIfAction (" + locLabelN1.Text + ", " + locLabelN2.Text + ")");
					locDB.ConectDB("CalcLicenseCost( '" + (string)Session["UserName"] + "', " + Inventions.GetInstance().CurrentProductID +
														", " + Inventions.GetInstance().LicenseType + ")", ReaderCost);
				}
			}
			Inventions.GetInstance().GetIfAction();
			DataBind();
		}

		public void RequestLicense(Object sender, EventArgs e)
		{
			if (ProductList.SelectedItem != null && LicenseTypeList.SelectedItem != null && Cost.Text != "0")
			{
				string locProductListID = ProductList.SelectedItem.Value;
				string locLicenseType = LicenseTypeList.SelectedItem.Value;
				if (locProductListID != "" && locLicenseType != "")
				{

					Database locDB = new Database();
					locDB.ConectDB("SendRequestLicense( '" + (string)Session["UserName"] + "', " + Inventions.GetInstance().CurrentProductID +
														", " + Inventions.GetInstance().LicenseType + ")", ReaderRequestLicense);

					if (IsOk1 == "1")
					{
						Error1.Visible = false;
						Error2.Visible = false;
					}
					if (IsOk1 == "-1")
					{
						Error1.Visible = true;
						Error2.Visible = false;
					}
					if (IsOk1 == "-2")
					{
						Error1.Visible = false;
						Error2.Visible = true;
					}

					DataBind();
				}

			}

		}

		string IsOk1 = "1";
		public void ReaderRequestLicense(object argReader, EventArgs e)
		{

			IsOk1 = ((MySqlDataReader)argReader)["IsOk"].ToString();
		}

	}
}