using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberNet.Game
{
	public partial class Inventions : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int OldIndex = CategoryType.SelectedIndex;
			CategoryType.DataSource = CreateCategoryTypeSource();
			CategoryType.DataTextField = "TextField";
			CategoryType.DataValueField = "ValueField";
			CategoryType.DataBind();
			CategoryType.SelectedIndex = OldIndex;

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
		}

		public void CategoryTypeChange(Object sender, EventArgs e)
		{
		}

		public void AddRaw(Object sender, EventArgs e)
		{
		}

		public void AddProduct(Object sender, EventArgs e)
		{
		}

		public void ProductListChange(Object sender, EventArgs e)
		{
		}
		public void RawListChange1(Object sender, EventArgs e)
		{
		}
		public void RawListChange2(Object sender, EventArgs e)
		{
		}

		public void AddFastAction(Object sender, EventArgs e)
		{
		}

		public void ActionIFListChange(Object sender, EventArgs e)
		{
		}

		public void AddIFAction(Object sender, EventArgs e)
		{
		}
		public void AddIFAction2(Object sender, EventArgs e)
		{
		}

	}
}