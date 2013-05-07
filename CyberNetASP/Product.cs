using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using MySql.Data.MySqlClient;

namespace CyberNet
{
	public class ProductOperation
	{
		private int id;
		public int ID
		{
			get { return id; }
			set { id = value; }
		}
		private int position;
		public int Position
		{
			get { return position; }
			set { position = value; }
		}
		private string operationID;
		public string OperationID
		{
			get { return operationID; }
			set { operationID = value; }
		}
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		private string actionName;
		public string ActionName
		{
			get { return actionName; }
			set { actionName = value; }
		}
	}


	public class Product
	{
        private static Product thisInstance;

		public Product(int a)
		{
			thisInstance = this;
		}
		public Product()
		{
			int a = 1;
		}
		public static Product GetInstance()
		{
			return thisInstance;
		}

		public static string SetProductID = "гость";




		public DataTable GetProductVariant()
		{
			return GetInstance().GetProductVariantInner();
		}

		DataTable locList = new DataTable();
		public ArrayList VariantList;


		private DataTable GetProductVariantInner()
		{

			locList.Columns.Add(new DataColumn("ID", typeof(int)));
			locList.Columns.Add(new DataColumn("Position", typeof(int)));
			locList.Columns.Add(new DataColumn("Name", typeof(String)));


			VariantList = new ArrayList();
			Database locDatabase = new Database();
			locDatabase.ConectDB("GetGraph (" + SetProductID + ")", Reader);


			return locList;
		}


		public void Reader(object argReader, EventArgs e)
		{
			ProductOperation locProductOperation = new ProductOperation();

			int locVariant = Convert.ToInt32(((MySqlDataReader)argReader)["OptionsID"].ToString());

			bool IsFound = false;
			for (int i = 0; i < VariantList.Count; i++)
			{
				if ((int)VariantList[i] == locVariant)
				{
					IsFound = true;
					break;
				}
			}
			if (IsFound == false)
			{
				VariantList.Add(locVariant);
			}

			locProductOperation.ID = locList.Rows.Count + 1;
			locProductOperation.Position = locList.Rows.Count + 1;
			locProductOperation.OperationID = GetOperation(locVariant);
			locProductOperation.Name = ((MySqlDataReader)argReader)["Name"].ToString();
			locProductOperation.ActionName = ((MySqlDataReader)argReader)["ActionName"].ToString();

			DataRow dr = locList.NewRow();
			dr[0] = locProductOperation.ID;
			dr[1] = locProductOperation.Position;
			dr[2] = locProductOperation.Name;

			locList.Rows.Add(dr);

		}

		public string GetOperation(int argVariantID)
		{
			string locOperation = "";
			for (int i = 0; i < VariantList.Count; i++)
			{
				if ((int)VariantList[i] == argVariantID)
				{
					locOperation = (i+1).ToString() + "-й способ";
					break;
				}
			}
			return locOperation;		
		}

		public bool SaveList(int ID, int Position, string Name)
		{
			return true;
		}

	}
}