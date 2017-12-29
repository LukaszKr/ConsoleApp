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

		#region DLL Imports
		private const int SWP_NOSIZE = 0x0001; //ignore pixewidth/height params
		private const int SWP_NOZORDER = 0x4; //don't change order of window
		private const int SWP_NOACTIVATE = 0x10;

		[DllImport("user32")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
			int x, int y, int pixelWidth, int pixelHeight, int flags);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetConsoleWindow();
		#endregion
	}
}