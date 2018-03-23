using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	public static partial class ConsoleHelper
	{
		private static readonly IntPtr m_ConsoleHandle;
		private static readonly IntPtr m_StdOutputHandle;
		private static readonly IntPtr m_StdInputHandle;

		static ConsoleHelper()
		{
			m_ConsoleHandle = GetConsoleWindow();
			m_StdOutputHandle = GetStdHandle(STD_OUTPUT_HANDLE);
			m_StdInputHandle = GetStdHandle(STD_INPUT_HANDLE);
		}

		public static void SetWindowPosition(int px, int py)
		{
			CheckError(SetWindowPos(m_ConsoleHandle, IntPtr.Zero, px, py, 0, 0, SWP_NOACTIVATE | SWP_NOACTIVATE | SWP_NOSIZE));
		}

		public static bool WriteOutput(Pixel[] pixels, Coord bufferSize, Coord bufferCoord)
		{
			SmallRect writeRegion = new SmallRect(bufferCoord.X, bufferCoord.Y, bufferSize.X, bufferSize.Y);
			return WriteConsoleOutputW(m_StdOutputHandle, pixels, bufferSize, bufferCoord, ref writeRegion);
		}

		public static ScreenBufferInfo GetScreenBufferInfo()
		{
			ScreenBufferInfo bufferInfo = new ScreenBufferInfo();
			GetConsoleScreenBufferInfo(m_StdOutputHandle, ref bufferInfo);
			return bufferInfo;
		}

		#region Error Handling
		public static string GetError()
		{
			uint errorCode = GetLastError();
			return errorCode.ToString();
		}

		public static bool CheckError(bool success)
		{
			if(!success)
			{
				throw new Exception(GetError());
			}
			return success;
		}
		#endregion

		#region DLL Imports
		private const int SWP_NOSIZE = 0x0001; //ignore pixewidth/height params
		private const int SWP_NOZORDER = 0x4; //don't change order of window
		private const int SWP_NOACTIVATE = 0x10;
		private const long GENERIC_READ = 0x80000000L;
		private const long GENERIC_WRITE = 0x40000000L;

		private const int STD_OUTPUT_HANDLE = -11;
		private const int STD_INPUT_HANDLE = -10;

		[DllImport("user32", ExactSpelling = true)]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
			Int32 x, Int32 y, Int32 pixelWidth, Int32 pixelHeight, UInt32 flags);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool WriteConsoleOutputW([In] IntPtr hWnd, 
			[In] Pixel[] pixels,
			[In] Coord bufferSize, [In] Coord bufferCoord, [In, Out] ref SmallRect writeRegion);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern IntPtr GetStdHandle(Int32 stdHandler);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool GetConsoleScreenBufferInfo([In] IntPtr consoleOutput, [In, Out] ref ScreenBufferInfo bufferInfo);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern uint GetLastError();
		#endregion
	}
}