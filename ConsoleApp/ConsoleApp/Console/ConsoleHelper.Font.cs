using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	public static partial class ConsoleHelper
	{
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

		#region Imports
		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool SetCurrentConsoleFontEx([In] IntPtr consoleOutput, [In] bool maximumWindow, [In] ref FontInfo fontInfo);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool GetCurrentConsoleFontEx([In] IntPtr consoleOutput, [In] bool maximumWindow, [In, Out] ref FontInfo fontInfo);
		#endregion
	}
}
