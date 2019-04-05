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

		public static bool WriteOutput(FramePixel[] pixels, Coord bufferSize, Coord bufferCoord)
		{
			SmallRect writeRegion = new SmallRect(bufferCoord.X, bufferCoord.Y, bufferSize.X, bufferSize.Y);
			return WriteConsoleOutputW(m_StdOutputHandle, pixels, bufferSize, bufferCoord, ref writeRegion);
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

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool WriteConsoleOutputW([In] IntPtr hWnd, 
			[In] FramePixel[] pixels,
			[In] Coord bufferSize, [In] Coord bufferCoord, [In, Out] ref SmallRect writeRegion);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern IntPtr GetStdHandle(Int32 stdHandler);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern uint GetLastError();
		#endregion
	}
}