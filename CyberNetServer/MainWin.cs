using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Runtime.InteropServices;

namespace CyberNet
{
	internal delegate bool SignalHandler(ConsoleSignal consoleSignal);

	internal enum ConsoleSignal
	{
		CtrlC = 0,
		CtrlBreak = 1,
		Close = 2,
		LogOff = 5,
		Shutdown = 6
	}

	internal static class ConsoleHelper
	{
		[DllImport("Kernel32", EntryPoint = "SetConsoleCtrlHandler")]
		public static extern bool SetSignalHandler(SignalHandler handler, bool add);
	}

	public partial class MainWin : Form
	{
		/// <summary>
		/// Внешняя функция подчиняющая главное окно определенного процесса другому главному окну
		/// </summary>
		/// <param name="hChildWnd">Указатель на окно которое хотим подчинить </param>
		/// <param name="hWnd">Указатель на окно, которое будет родительским</param>
		/// <returns>Код ошибки</returns>
		[DllImport("user32.dll")]
		static extern int SetParent(int hChildWnd, int hWnd);

		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);
		[DllImport("user32.dll")]
		static extern int GetMenuItemCount(IntPtr hMenu);
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool DrawMenuBar(IntPtr hWnd);
		[DllImport("user32.dll")]
		[return : MarshalAs(UnmanagedType.Bool)]
		static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

		static IntPtr ConsoleHandle ;

		private static SignalHandler signalHandler;
		private const Int32 MF_BYPOSITION = 0x400;
		private const Int32 MF_REMOVE = 0x1000;

		public MainWin()
		{
			InitializeComponent();
			this.Visible = false;


			ConsoleAPI c = new ConsoleAPI();
			Console.Title = "CyberNetServer Console";

			ConsoleHandle = GetConsoleWindow();

			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();

			signalHandler += HandleConsoleSignal;
			ConsoleHelper.SetSignalHandler(signalHandler, true);

			RemoveCloseButton();

			MyHttpServer httpServer = new MyHttpServer(8081);

			//Console.CursorVisible = false;
			//Console.CancelKeyPress += Console_CancelKeyPress;
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;

			//httpServer.ConectDB();
			//httpServer.JSONTest();
			httpServer.SaveCities();

			/*
			Thread thread = new Thread(new ThreadStart(httpServer.listen));
			thread.Start();

			Thread locTimeThread = new Thread(new ThreadStart(httpServer.NextTime));
			locTimeThread.Start();*/


		}
		public static void RemoveCloseButton()
		{
			IntPtr hWin = ConsoleHandle; //Process.GetCurrentProcess().MainWindowHandle;
			if (hWin != IntPtr.Zero)
			{
				IntPtr hMenu = GetSystemMenu(hWin, false);
				if (hMenu != IntPtr.Zero)
				{
					int n = GetMenuItemCount(hMenu);
					int max = 20;
					while (n > 0 && max > 0)
					{
						RemoveMenu(hMenu, (uint)(n - 1), MF_BYPOSITION | MF_REMOVE);
						n = GetMenuItemCount(hMenu);
						--max;
					}
					DrawMenuBar(hWin);
				}
			}
		}

		private static bool HandleConsoleSignal(ConsoleSignal consoleSignal)
		{
			return false;
		}
	}
}
