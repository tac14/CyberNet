using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using MySql.Data.MySqlClient;

namespace CyberNet
{
	public class Database
	{
		public void Exec(string argCommand)
		{
			ConectDB(argCommand, null);
		}

		public void ConectDB(string argCommand, EventHandler argReader)
		{
			MySqlCommand command = new MySqlCommand(); ;
			string connectionString, commandString;
			connectionString = "Data source=localhost;UserId=root;Password=nt[yj14;database=CyberNetDB;";
			MySqlConnection connection = new MySqlConnection(connectionString);
			commandString = "call " + argCommand + ";";
			command.CommandText = commandString;
			command.Connection = connection;
			MySqlDataReader reader;
			try
			{
				command.Connection.Open();
				reader = command.ExecuteReader();

				if (argReader != null)
				{
					while (reader.Read())
					{
						argReader(reader, null);
					}
				}

				reader.Close();
			}
			catch (MySqlException ex)
			{
				Console.WriteLine("Error: \r\n{0}", ex.ToString());
			}
			finally
			{
				command.Connection.Close();
			}
		}




		DataTable dt = new DataTable();
		public DataView GetDataSource(string argCommand)
		{
			dt.Columns.Add(new DataColumn("TextField", typeof(String)));
			dt.Columns.Add(new DataColumn("ValueField", typeof(String)));

			ConectDB(argCommand, DataViewReader);

			DataView dv = new DataView(dt);
			return dv;
		
		}
		public void DataViewReader(object argReader, EventArgs e)
		{
			string locText = ((MySqlDataReader)argReader)["Name"].ToString();
			string locValue = ((MySqlDataReader)argReader)["ID"].ToString();
			dt.Rows.Add(CreateRow(locText, locValue, dt));
		}

		DataRow CreateRow(String Text, String Value, DataTable dt)
		{
			DataRow dr = dt.NewRow();
			dr[0] = Text;
			dr[1] = Value;
			return dr;
		}

	}
}