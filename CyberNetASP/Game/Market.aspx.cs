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
	public partial class MarketLayout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Market.GetInstance() == null)
			{
				Market locMarket = new Market(1);
			}

			int OldIndex = ProductList.SelectedIndex;
			ProductList.DataSource = CreateProductDataSource();
			ProductList.DataTextField = "TextField";
			ProductList.DataValueField = "ValueField";
			ProductList.DataBind();
			ProductList.SelectedIndex = OldIndex;

			int OldIndex2 = ProductListFromStock.SelectedIndex;
			ProductListFromStock.DataSource = CreateProductListFromStockDataSource();
			ProductListFromStock.DataTextField = "TextField";
			ProductListFromStock.DataValueField = "ValueField";
			ProductListFromStock.DataBind();
			ProductListFromStock.SelectedIndex = OldIndex2;

			int OldIndex3 = LotList.SelectedIndex;
			LotList.DataSource = CreateLotListDataSource();
			LotList.DataTextField = "TextField";
			LotList.DataValueField = "ValueField";
			LotList.DataBind();
			LotList.SelectedIndex = OldIndex3;

		}

		DataView CreateProductDataSource()
		{
			Database locDB = new Database();
			return locDB.GetDataSource("GetAllProduct");
		}

		DataView CreateLotListDataSource()
		{
			Database locDB = new Database();
			return locDB.GetDataSource("GetLotList ('" + (string)Session["UserName"] + "')");
		}

		DataView CreateProductListFromStockDataSource()
		{
			Database locDB = new Database();
			return locDB.GetDataSource("GetStockList ('" + (string)Session["UserName"] + "')");
		}

		public void ProductFromStockChange(Object sender, EventArgs e)
		{
		}

		public void AddLot(Object sender, EventArgs e)
		{
			if (ProductListFromStock.SelectedItem != null)
			{
				string locProductListFromStockID = ProductListFromStock.SelectedItem.Value;

				if (locProductListFromStockID != "")
				{
					Database locDB = new Database();
					locDB.Exec("AddLot( " + locProductListFromStockID + ", " + ProductCount.Text + ")");
				}
				DataBind();
				LotList.DataSource = CreateLotListDataSource();
				LotList.DataBind();
			}

		}

		public void LotChange(Object sender, EventArgs e)
		{
		}

		public void ProductChange(Object sender, EventArgs e)
		{
		}

		public void AddLotVariant(Object sender, EventArgs e)
		{
			if (LotList.SelectedItem != null && ProductList.SelectedItem != null)
			{
				string locLotListID = LotList.SelectedItem.Value;
				string locProductListID = ProductList.SelectedItem.Value;

				if (locLotListID != "" && locProductListID != "")
				{
					Database locDB = new Database();
					locDB.Exec("AddLotVariant( " + locLotListID + ", " + locProductListID + ", " +
													ProductMinCount.Text + ", "  + ProductMinQuality.Text + ")");
				}
				DataBind();
			}
		}

		public void Delete(Object sender, EventArgs e)
		{
			for (int i = 0; i < MarketList.Items.Count; i++)
			{
				CheckBox locCheckBox = MarketList.Items[i].FindControl("CheckLot") as CheckBox;
				if (locCheckBox.Checked == true)
				{
					Label locLabelN1 = MarketList.Items[i].FindControl("N1") as Label;

					Database locDB = new Database();
					locDB.Exec("DeleteLot(" + locLabelN1.Text + ")");
				}
				CheckBox locCheckBox2 = MarketList.Items[i].FindControl("CheckLotVariant") as CheckBox;
				if (locCheckBox2.Checked == true)
				{
					Label locLabelN2 = MarketList.Items[i].FindControl("N2") as Label;

					Database locDB = new Database();
					locDB.Exec("DeleteLotVariant(" + locLabelN2.Text + ")");
				}
			}
			DataBind();
			LotList.DataSource = CreateLotListDataSource();
			LotList.DataBind();
		}

		public void Exchange(Object sender, EventArgs e)
		{
			for (int i = 0; i < ExchangeList.Items.Count; i++)
			{
				CheckBox locCheckBox = ExchangeList.Items[i].FindControl("CheckExchange") as CheckBox;
				if (locCheckBox.Checked == true)
				{
					Label locID1 = ExchangeList.Items[i].FindControl("ID1") as Label;
					Label locID2 = ExchangeList.Items[i].FindControl("ID2") as Label;
					Label locEID2 = ExchangeList.Items[i].FindControl("eID2") as Label;

					Database locDB = new Database();
					locDB.Exec("CalcExchange(" + locID1.Text + ", " + locID2.Text + ", " + locEID2.Text + ")");
				}
			}
			DataBind();
		}

	}
}