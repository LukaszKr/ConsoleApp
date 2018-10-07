using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	public static partial class ConsoleHelper
	{
		public static void SetWindowPosition(int px, int py)
		{
			CheckError(SetWindowPos(m_ConsoleHandle, IntPtr.Zero, px, py, 0, 0, SWP_NOACTIVATE | SWP_NOACTIVATE | SWP_NOSIZE));
		}

		public static bool SetSize(ref int width, ref int height)
		{
			bool changed = false;
			ScreenBufferInfo info = GetScreenBufferInfo();
			//bounds can't be smaller than screen buffer
			int hSize = info.Bounds.Right+1+info.Bounds.Left;
			if(width != hSize)
			{
				changed = true;
			}
			int vSize = info.Bounds.Bottom+1+info.Bounds.Top;
			if(height != vSize)
			{
				changed = true;
			}

			bool widthDecrease = false;
			bool heightDecrease = false;

			if(width < info.Size.X)
			{
				widthDecrease = true;
			}
			if(height < info.Size.Y)
			{
				heightDecrease = true;
			}
			if(width == info.Size.X && height == info.Size.Y && info.Bounds.Left == 0 && info.Bounds.Top == 0)
			{
				return changed;
			}

			if(!widthDecrease && !heightDecrease) //if screen increases, we have to set buffer first, then window
			{
				CheckError(SetConsoleScreenBufferSize(m_StdOutputHandle, new Coord(width, height)));
				SmallRect rect = new SmallRect(0, 0, width-1, height-1);
				CheckError(SetConsoleWindowInfo(m_StdOutputHandle, true, ref rect));
			}
			else if(widthDecrease && heightDecrease) //in case of decreasing, window changes first or we get an error
			{
				SmallRect rect = new SmallRect(0, 0, width-1, height-1);
				CheckError(SetConsoleWindowInfo(m_StdOutputHandle, true, ref rect));
				CheckError(SetConsoleScreenBufferSize(m_StdOutputHandle, new Coord(width, height)));
			}
			else //for mixed case, each dimension has to be handled separately
			{
				SmallRect rect = new SmallRect(0, 0, width-1, info.MaximumWindowSize.Y-1);
				if(widthDecrease)
				{
					CheckError(SetConsoleWindowInfo(m_StdOutputHandle, true, ref rect));
					CheckError(SetConsoleScreenBufferSize(m_StdOutputHandle, new Coord(width, info.MaximumWindowSize.Y)));
				}
				else
				{
					CheckError(SetConsoleScreenBufferSize(m_StdOutputHandle, new Coord(width, info.MaximumWindowSize.Y)));
					CheckError(SetConsoleWindowInfo(m_StdOutputHandle, true, ref rect));
				}

				rect = new SmallRect(0, 0, width-1, height-1);
				if(heightDecrease)
				{
					CheckError(SetConsoleWindowInfo(m_StdOutputHandle, true, ref rect));
					CheckError(SetConsoleScreenBufferSize(m_StdOutputHandle, new Coord(width, height)));
				}
				else
				{
					CheckError(SetConsoleScreenBufferSize(m_StdOutputHandle, new Coord(width, height)));
					CheckError(SetConsoleWindowInfo(m_StdOutputHandle, true, ref rect));
				}
			}
			////somehow removes column that was occupied by vertical scrollbar
			Console.SetCursorPosition(0, 0);
			////same for horizontal
			Console.SetWindowPosition(0, 0);
			return true;
		}

		public static void ValidateScreenSize(int width, int height)
		{
			ScreenBufferInfo info = GetScreenBufferInfo();
			if(info.MaximumWindowSize.X < width)
			{
				throw new ArgumentOutOfRangeException(string.Format("MaxWidth: {0}, Attempted: {1}", info.MaximumWindowSize.X.ToString(), width.ToString()));
			}
			if(info.MaximumWindowSize.Y < height)
			{
				throw new ArgumentOutOfRangeException(string.Format("MaxHeight: {0}, Attempted: {1}", info.MaximumWindowSize.Y.ToString(), height.ToString()));
			}
		}

		public static ScreenBufferInfo GetScreenBufferInfo()
		{
			ScreenBufferInfo bufferInfo = new ScreenBufferInfo();
			GetConsoleScreenBufferInfo(m_StdOutputHandle, ref bufferInfo);
			return bufferInfo;
		}

		#region Import
		[DllImport("user32", ExactSpelling = true)]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
			Int32 x, Int32 y, Int32 pixelWidth, Int32 pixelHeight, UInt32 flags);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool GetConsoleScreenBufferInfo([In] IntPtr consoleOutput, [In, Out] ref ScreenBufferInfo bufferInfo);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool SetConsoleScreenBufferSize([In] IntPtr consoleOutput, [In] Coord size);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool SetConsoleWindowInfo([In] IntPtr consoleOutput, [In] bool absolute, [In, Out] ref SmallRect consoleWindow);
		#endregion
	}
}
