using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace CyberNet
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.WriteLine("Запуск сервера...");
			TcpListener Srv = new TcpListener(IPAddress.Any, 3148);
			try
			{
				Srv.Start();
			}
			catch (Exception e)
			{
				Console.WriteLine("Не удалось запустить сервер: {0}", e.Message);
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Сервер запущен. Ожидание подключения...");

			TcpClient Client = Srv.AcceptTcpClient();

			NetworkStream ClientStream = Client.GetStream();

			Console.WriteLine("Входящее соединение ({0})", Client.Client.RemoteEndPoint.ToString());

			string response = "Ответ сервера клиенту";
			byte[] responseBuffer = Encoding.Default.GetBytes(response);
			ClientStream.Write(responseBuffer, 0, responseBuffer.Length);

			ClientStream.Close();
			Console.WriteLine("Ответ клиенту отправлен. Работа завершена.");
			Console.WriteLine("Нажмите любую клавишу...");
			Console.ReadKey();	
		
		}
	}
}
