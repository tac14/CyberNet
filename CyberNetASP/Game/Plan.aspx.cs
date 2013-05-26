using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CyberNet;

namespace CyberNet.Game
{
	public partial class PlanLayout : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			Product locProduct = new Product(1);

			if (Plan.GetInstance() == null)
			{
				Plan locPlan = new Plan(1);
			}

			int OldIndex = ProductList.SelectedIndex;
			ProductList.DataSource = CreateProductDataSource();
			ProductList.DataTextField = "TextField";
			ProductList.DataValueField = "ValueField";

			ProductList.DataBind();

			ProductList.SelectedIndex = OldIndex;
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

			// Вначале получим все варианты получения товара, чтобы заполнить OptionsList
			Product.SetOptionID = 0;
			Product.GetInstance().GetProductVariant();

			OptionsList.DataSource = CreateOptionsDataSource();
			OptionsList.DataTextField = "TextField";
			OptionsList.DataValueField = "ValueField";
			OptionsList.DataBind();

			// Теперь получим, то что относится к первому варианту получения товара (настоящий OptionID, которого не известен)
			Product.SetOptionID = -1;

			DataBind();
		}
		public void OptionsChange(Object sender, EventArgs e)
		{
			Product.SetProductID = ProductList.SelectedItem.Value;
			// Получим, то что относится к выбраному варианту получения товара (настоящий OptionID известен - привязан как Value)
			Product.SetOptionID = Convert.ToInt32(OptionsList.SelectedItem.Value);
			DataBind();
		}

		public void AddPlan(Object sender, EventArgs e)
		{
			if (ProductList.SelectedItem != null && OptionsList.SelectedItem != null)
			{
				string locProductID = ProductList.SelectedItem.Value;
				string locOptionsID = OptionsList.SelectedItem.Value;

				if (locProductID != "" && locOptionsID != "")
				{
					Database locDB = new Database();
					locDB.Exec("AddPlan( -1, -1, " + locProductID + ", " + locOptionsID + ", '" + AgentState.GetInstance((string)Session["UserName"]).Name + "')");
				}
				DataBind();
			}
		}

		public void ClearPlan(Object sender, EventArgs e)
		{
			for (int i = 0; i < ReorderList1.Items.Count; i++)
			{
				CheckBox locCheckBox = ReorderList1.Items[i].FindControl("checkbox1") as CheckBox;
				if (locCheckBox.Checked == true)
				{
					Database locDB = new Database();
					locDB.Exec("AddPlan( -1, " + (i + 1).ToString() + ", NULL, NULL, '" + AgentState.GetInstance((string)Session["UserName"]).Name + "')");
				}
			}
			DataBind();
		}

	}
}