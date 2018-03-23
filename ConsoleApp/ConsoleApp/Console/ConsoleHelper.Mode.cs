using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	public static partial class ConsoleHelper
	{
		public static void SetFullScreen(bool fullscreen)
		{
			Coord coords = new Coord();
			CheckError(SetConsoleDisplayMode(m_StdOutputHandle, (uint)(fullscreen ? 1 : 2), ref coords));
		}

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

		#region Imports
		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool SetConsoleDisplayMode([In] IntPtr consoleOutput, [In] UInt32 flags, [In, Out] ref Coord screenBufferDimensions);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool SetConsoleMode([In] IntPtr consoleHandle, [In] uint mode);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool GetConsoleMode([In] IntPtr consoleHandle, [In, Out] ref uint mode);
		#endregion
	}
}
