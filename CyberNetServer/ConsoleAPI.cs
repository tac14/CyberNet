using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CyberNet
{
	public class ConsoleAPI : IDisposable
	{
		private static IntPtr conOut;
		private static IntPtr oldOut;

		public ConsoleAPI()
		{
			if (oldOut == IntPtr.Zero)
				oldOut = GetStdHandle( -11 );

			if (!AllocConsole())
				throw new Exception("AllocConsole");

			conOut = CreateFile( "CONOUT$", 0x40000000, 2, IntPtr.Zero, 3, 0, IntPtr.Zero );

			if (!SetStdHandle(-11, conOut))
				throw new Exception("SetStdHandle");

			Stream standard = Console.OpenStandardOutput();
			StreamWriter writer = new StreamWriter(standard);
			writer.AutoFlush = true;
			Console.SetOut(writer);
			Console.SetError(writer);
		}

		public void Dispose()
		{
			if (! CloseHandle(conOut))
				throw new Exception("CloseHandle");
			conOut = IntPtr.Zero;
			if (! FreeConsole())
				throw new Exception("FreeConsole");
			if (!SetStdHandle(-11, oldOut))
				throw new Exception("SetStdHandle");
		}

		[DllImport("kernel32.dll", SetLastError=true)]
		protected static extern bool AllocConsole();

		[DllImport("kernel32.dll", SetLastError=false)]
		protected static extern bool FreeConsole();

		[DllImport("kernel32.dll", SetLastError=true)]
		protected static extern IntPtr GetStdHandle( int nStdHandle );

		[DllImport("kernel32.dll", SetLastError=true)]
		protected static extern bool SetStdHandle( int nStdHandle, IntPtr hConsoleOutput );

		[DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		protected static extern IntPtr CreateFile(
			string  fileName,
			int   desiredAccess,
			int   shareMode,
			IntPtr  securityAttributes,
			int   creationDisposition,
			int   flagsAndAttributes,
			IntPtr  templateFile );

		[DllImport("kernel32.dll", ExactSpelling=true, SetLastError=true)]
		protected static extern bool CloseHandle( IntPtr handle );
	}
}
