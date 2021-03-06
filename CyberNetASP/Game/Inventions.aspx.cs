﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace CyberNet.Game
{
	public partial class InventionsLayout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			int OldIndex = CategoryType.SelectedIndex;
			CategoryType.DataSource = CreateCategoryTypeSource();
			CategoryType.DataTextField = "TextField";
			CategoryType.DataValueField = "ValueField";
			CategoryType.DataBind();
			CategoryType.SelectedIndex = OldIndex;

			int OldIndex2 = ActionList.SelectedIndex;
			ActionList.DataSource = CreateActionListDataSource();
			ActionList.DataTextField = "TextField";
			ActionList.DataValueField = "ValueField";
			ActionList.DataBind();
			ActionList.SelectedIndex = OldIndex2;


		}

		DataView CreateActionListDataSource()
		{
			Database locDB = new Database();
			return locDB.GetDataSource("GetAllActions ('" + (string)Session["UserName"] + "')");
		}

		DataView CreateCategoryTypeSource()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("TextField", typeof(String)));
			dt.Columns.Add(new DataColumn("ValueField", typeof(String)));

			dt.Rows.Add(CreateRow("", "-", dt));
			dt.Rows.Add(CreateRow("Растение", "Plants", dt));
			dt.Rows.Add(CreateRow("Животное", "Animals", dt));
			dt.Rows.Add(CreateRow("Ископаемое", "Minerals", dt));

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

		public void ActionListChange(Object sender, EventArgs e)
		{
		}

		public void AddAction(Object sender, EventArgs e)
		{
			if (ActionList.SelectedItem != null && ActionName.Text != "")
			{
				string locActionListID = ActionList.SelectedItem.Value;
				
				if (locActionListID == "-")
				{
					locActionListID = "0";
				}

				if (locActionListID != "")
				{
					Database locDB = new Database();
					locDB.ConectDB("AddAction( '" + (string)Session["UserName"] + "', '" + ActionName.Text + "', " +
													locActionListID + ")", ReaderAddAction);
				}

				if (IsOk1 == "1")
				{
					Label locError = NewAction.FindControl("Error1") as Label;
					locError.Visible = false;
					DataBind();
					ActionList.DataSource = CreateActionListDataSource();
					ActionList.DataBind();
					/*ActionList2.DataSource = CreateActionListDataSource();
					ActionList2.DataBind();
					ActionList3.DataSource = CreateActionListDataSource();
					ActionList3.DataBind();*/
				}
				else
				{
					Label locError = NewAction.FindControl("Error1") as Label;
					locError.Visible = true;
					DataBind();
				}

			}
		}

		string IsOk1 = "1";
		public void ReaderAddAction(object argReader, EventArgs e)
		{

			IsOk1 = ((MySqlDataReader)argReader)["IsOk"].ToString();
		}

		public void CategoryTypeChange(Object sender, EventArgs e)
		{
		}

		public void AddRaw(Object sender, EventArgs e)
		{
			if (RawName.Text != "" && CategoryType.SelectedItem != null && RawMeasure.Text != "" && RawNorm.Text != "")
			{
				string locCategoryTypeID = CategoryType.SelectedItem.Value;

				if (locCategoryTypeID != "")
				{
					Database locDB = new Database();
					locDB.ConectDB("AddRaw( '" + (string)Session["UserName"] + "', '" + RawName.Text + "', '" +
													locCategoryTypeID + "', '" + RawMeasure.Text + "', " + RawNorm.Text + ")", ReaderAddAction);
				}

				if (IsOk1 == "1")
				{
					Label locError = NewRaw.FindControl("Error2") as Label;
					locError.Visible = false;
					DataBind();
					/*ProductList.DataSource = CreateProductListDataSource();
					ProductList.DataBind();
					ProductList2.DataSource = CreateProductListDataSource2();
					ProductList2.DataBind();
					ProductList3.DataSource = CreateProductListDataSource2();
					ProductList3.DataBind();*/
				}
				else
				{
					Label locError = NewRaw.FindControl("Error2") as Label;
					locError.Visible = true;
					DataBind();
				}

			}

		}

		public void AddProduct(Object sender, EventArgs e)
		{
			if (ProductName.Text != "" && ProductMeasure.Text != "" && ProductNorm.Text != "")
			{

				CheckBox locIsFood = NewProduct.FindControl("IsFood") as CheckBox;
	
				Database locDB = new Database();
				locDB.ConectDB("AddProduct( '" + (string)Session["UserName"] + "', '" + ProductName.Text + "', " +
												locIsFood.Checked.ToString() + ", '" + ProductMeasure.Text + "', " + ProductNorm.Text + ")", ReaderAddAction);

				if (IsOk1 == "1")
				{
					Label locError = NewProduct.FindControl("Error3") as Label;
					locError.Visible = false;
					DataBind();
					/*ProductList.DataSource = CreateProductListDataSource();
					ProductList.DataBind();
					ProductList2.DataSource = CreateProductListDataSource2();
					ProductList2.DataBind();
					ProductList3.DataSource = CreateProductListDataSource2();
					ProductList3.DataBind();*/
				}
				else
				{
					Label locError = NewProduct.FindControl("Error3") as Label;
					locError.Visible = true;
					DataBind();
				}

			}
		}


	}
}