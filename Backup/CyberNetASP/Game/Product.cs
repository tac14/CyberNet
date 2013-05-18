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
		public static int SetOptionID = 0;




		public DataTable GetProductVariant()
		{
			return GetInstance().GetProductVariantInner();
		}

		DataTable locList;
		public ArrayList VariantList;


		private DataTable GetProductVariantInner()
		{
			locList = new DataTable();
			locList.Columns.Add(new DataColumn("OperationID", typeof(String)));
			locList.Columns.Add(new DataColumn("Name", typeof(String)));
			locList.Columns.Add(new DataColumn("ActionName", typeof(String)));


			VariantList = new ArrayList();
			Database locDatabase = new Database();
			locDatabase.ConectDB("GetGraph (" + SetProductID + ", " + SetOptionID.ToString() + ")", Reader);


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
			dr[0] = locProductOperation.OperationID;
			dr[1] = locProductOperation.Name;
			dr[2] = locProductOperation.ActionName;

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

		//public bool SaveList(int ID, int Position, string Name)
		//{
		//    return true;
		//}

	}
}