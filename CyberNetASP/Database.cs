using System;
using System.Collections.Generic;
using System.Web;
using MySql.Data.MySqlClient;

namespace CyberNet
{
	public class Database
	{
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
				while (reader.Read())
				{
					argReader(reader, null);
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
	}
}