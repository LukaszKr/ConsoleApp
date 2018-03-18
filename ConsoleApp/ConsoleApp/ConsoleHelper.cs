using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	public static class ConsoleHelper
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

		public static void SetFullScreen(bool fullscreen)
		{
			Coord coords = new Coord();
			CheckError(SetConsoleDisplayMode(m_StdOutputHandle, (uint)(fullscreen? 1: 2), ref coords));
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

		#region Mode
		public static void SetInputMode(EInputMode mode)
		{
			CheckError(SetConsoleMode(m_StdInputHandle, (uint)mode));
		}

		public static EInputMode GetInputMode()
		{
			uint mode = 0;
			CheckError(GetConsoleMode(m_StdInputHandle, ref mode));
			return (EInputMode)mode;
		}

		public static void SetOutputMode(EOutputMode mode)
		{
			CheckError(SetConsoleMode(m_StdOutputHandle, (uint)mode));
		}

		public static EOutputMode GetOutputMode()
		{
			uint mode = 0;
			CheckError(GetConsoleMode(m_StdOutputHandle, ref mode));
			return (EOutputMode)mode;
		}
		#endregion

		#region Font
		public static bool SetFontSize(EFontSize size)
		{
			FontInfo info = GetFontInfo();
			info.FontSize = new Coord(0, (int)size);
			return SetFont(info);
		}

		public static bool SetFontSize(ETerminalFontSize size)
		{
			FontInfo info = GetFontInfo();
			info.FontSize = size.ToCoord();
			info.SetFace(EFontFaceExt.TERMINAL);
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
			return SetCurrentConsoleFontEx(m_StdOutputHandle, false, ref info);
		}

		public static FontInfo GetFontInfo()
		{
			FontInfo info = new FontInfo();
			info.Init();
			GetCurrentConsoleFontEx(m_StdOutputHandle, false, ref info);
			return info;
		}
		#endregion

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
		private static extern bool SetConsoleDisplayMode([In] IntPtr consoleOutput, [In] UInt32 flags, [In, Out] ref Coord screenBufferDimensions);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool SetConsoleMode([In] IntPtr consoleHandle, [In] uint mode);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool GetConsoleMode([In] IntPtr consoleHandle, [In, Out] ref uint mode);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern bool PeekConsoleInputW([In] IntPtr consoleInput, [In] InputRecord[] record, [In] uint arrayLength, [In, Out] ref uint numberRead);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool WriteConsoleOutputW([In] IntPtr hWnd, 
			[In] Pixel[] pixels,
			[In] Coord bufferSize, [In] Coord bufferCoord, [In, Out] ref SmallRect writeRegion);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern IntPtr GetStdHandle(Int32 stdHandler);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool SetCurrentConsoleFontEx([In] IntPtr consoleOutput, [In] bool maximumWindow, [In] ref FontInfo fontInfo);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool GetCurrentConsoleFontEx([In] IntPtr consoleOutput, [In] bool maximumWindow, [In, Out] ref FontInfo fontInfo);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool GetConsoleScreenBufferInfo([In] IntPtr consoleOutput, [In, Out] ref ScreenBufferInfo bufferInfo);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern uint GetLastError();
		#endregion
	}
}