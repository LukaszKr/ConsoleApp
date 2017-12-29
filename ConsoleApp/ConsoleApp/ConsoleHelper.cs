using ProceduralLevel.ConsoleApp.Import;
using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	public static class ConsoleHelper
	{
		public static void SetWindowPosition(int px, int py)
		{
			IntPtr handle = GetConsoleWindow();
			SetWindowPos(handle, IntPtr.Zero, px, py, 0, 0, SWP_NOACTIVATE | SWP_NOACTIVATE | SWP_NOSIZE);
		}

		public static bool WriteOutput(CharInfo[] chars, Coord bufferSize, Coord bufferCoord)
		{
			SmallRect writeRegion = new SmallRect(bufferCoord.X, bufferCoord.Y, bufferSize.X, bufferSize.Y);
			IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
			return WriteConsoleOutput(handle, chars, bufferSize, bufferCoord, ref writeRegion);
		}

		public static string GetError()
		{
			uint errorCode = GetLastError();
			return errorCode.ToString();
		}

		#region DLL Imports
		private const int SWP_NOSIZE = 0x0001; //ignore pixewidth/height params
		private const int SWP_NOZORDER = 0x4; //don't change order of window
		private const int SWP_NOACTIVATE = 0x10;

		private const int STD_OUTPUT_HANDLE = -11;

		[DllImport("user32")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
			int x, int y, int pixelWidth, int pixelHeight, int flags);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool WriteConsoleOutput(IntPtr hWnd, CharInfo[] chars, 
			Coord bufferSize, Coord bufferCoord, ref SmallRect writeRegion);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetStdHandle(int stdHandler);

		[DllImport("kernel32.dll")]
		private static extern uint GetLastError();
		#endregion
	}
}