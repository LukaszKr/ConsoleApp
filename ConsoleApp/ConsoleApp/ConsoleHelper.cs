using ProceduralLevel.ConsoleApp.Import;
using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	public static class ConsoleHelper
	{
		private static readonly IntPtr m_ConsoleHandle;
		private static readonly IntPtr m_StdHandle;

		static ConsoleHelper()
		{
			m_ConsoleHandle = GetConsoleWindow();
			m_StdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
		}

		public static void SetWindowPosition(int px, int py)
		{
			SetWindowPos(m_ConsoleHandle, IntPtr.Zero, px, py, 0, 0, SWP_NOACTIVATE | SWP_NOACTIVATE | SWP_NOSIZE);
		}

		public static bool WriteOutput(Pixel[] pixels, Coord bufferSize, Coord bufferCoord)
		{
			SmallRect writeRegion = new SmallRect(bufferCoord.X, bufferCoord.Y, bufferSize.X, bufferSize.Y);
			return WriteConsoleOutput(m_StdHandle, pixels, bufferSize, bufferCoord, ref writeRegion);
		}

		public static bool SetFontSize(EFontSize size)
		{
			FontInfo info = GetFontInfo();
			info.FontSize = new Coord(0, (int)size);
			return SetFont(info);
		}

		public static bool SetFontWeight(EFontWeight weight)
		{
			FontInfo info = GetFontInfo();
			info.FontWeight = weight;
			return SetFont(info);
		}

		public static bool SetFontFace(EFontFace face)
		{
			return SetFontFace(face.ToFaceName());
		}

		public static bool SetFontFace(string name)
		{
			FontInfo info = GetFontInfo();
			info.SetFace(name);
			return SetFont(info);
		}

		public static bool SetFont(FontInfo info)
		{
			return SetCurrentConsoleFontEx(m_StdHandle, false, ref info);
		}

		public static FontInfo GetFontInfo()
		{
			FontInfo info = new FontInfo();
			info.Init();
			CheckError(GetCurrentConsoleFontEx(m_StdHandle, false, ref info));
			return info;
		}

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

		#region DLL Imports
		private const int SWP_NOSIZE = 0x0001; //ignore pixewidth/height params
		private const int SWP_NOZORDER = 0x4; //don't change order of window
		private const int SWP_NOACTIVATE = 0x10;

		private const int STD_OUTPUT_HANDLE = -11;

		[DllImport("user32")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
			Int32 x, Int32 y, Int32 pixelWidth, Int32 pixelHeight, UInt32 flags);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool WriteConsoleOutput(IntPtr hWnd, Pixel[] pixels, 
			Coord bufferSize, Coord bufferCoord, ref SmallRect writeRegion);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetStdHandle(Int32 stdHandler);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool SetCurrentConsoleFontEx(IntPtr consoleOutput, bool maximumWindow, ref FontInfo fontInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool GetCurrentConsoleFontEx(IntPtr consoleOutput, bool maximumWindow, ref FontInfo fontInfo);

		[DllImport("kernel32.dll")]
		private static extern uint GetLastError();
		#endregion
	}
}