using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CyberNet;

namespace WebTest.Game
{
	public partial class Plan : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Product locProduct = new Product(1);

			int OldIndex = ProductList.SelectedIndex;
			ProductList.DataSource = CreateProductDataSource();
			ProductList.DataTextField = "TextField";
			ProductList.DataValueField = "ValueField";

			ProductList.DataBind();

			//ReorderList1.DataBind();
			/*
			int OldIndex2 = OptionsList.SelectedIndex;
			OptionsList.DataSource = CreateOptionsDataSource();
			OptionsList.DataTextField = "TextField";
			OptionsList.DataValueField = "ValueField";

			OptionsList.DataBind();*/

			ProductList.SelectedIndex = OldIndex;
			//OptionsList.SelectedIndex = OldIndex2;

		}
		DataView CreateProductDataSource()
		{
			Database locDB = new Database();
			return locDB.GetDataSource("GetAllProduct");
		}
		DataView CreateOptionsDataSource()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("TextField", typeof(String)));
			dt.Columns.Add(new DataColumn("ValueField", typeof(String)));

			Product locProduct = Product.GetInstance();

			if (locProduct.VariantList != null)
			{
				for (int i = 0; i < locProduct.VariantList.Count; i++)
				{
					string locText = (i + 1).ToString() + "-й способ";
					string locValue = locProduct.VariantList[i].ToString();
					dt.Rows.Add(CreateRow(locText, locValue, dt));
				}
			}
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

		public void ProductChange(Object sender, EventArgs e)
		{
			Product.SetProductID = ProductList.SelectedItem.Value;
			DataBind();

			OptionsList.DataSource = CreateOptionsDataSource();
			OptionsList.DataTextField = "TextField";
			OptionsList.DataValueField = "ValueField";

			OptionsList.DataBind();

			//OptionsList.SelectedIndex = -1;


		}
		public void OptionsChange(Object sender, EventArgs e)
		{
			//Product.SetProductID = ProductList.SelectedItem.Value;
			//DataBind();
		}


	}
}