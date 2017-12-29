using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	public class Window
	{
		private Canvas m_Canvas;
		private int m_Width;
		private int m_Height;

		public int Width { get { return m_Width; } }
		public int Height { get { return m_Height; } }

		public Canvas Canvas { get { return m_Canvas; } }

		public Window(string title)
			: this(title, Console.LargestWindowWidth, Console.LargestWindowHeight)
		{

		}

		public Window(string title, int width, int height)
		{
			SetSize(width, height);
			Console.CursorVisible = false;
			Console.Title = title;
			m_Canvas = new Canvas(width, height);
		}

		public void Render()
		{
			if(Console.WindowWidth != m_Width || Console.WindowHeight != m_Height ||
				Console.BufferWidth != m_Width || Console.BufferHeight != m_Height)
			{
				SetSize(m_Width, m_Height);
			}
			m_Canvas.Render(0, 0);
		}

		public void SetSize(int width, int height)
		{
			m_Width = width;
			m_Height = height;
			Console.SetWindowSize(width, height);
			Console.SetBufferSize(width, height);
			//somehow removes column that was occupied by vertical scrollbar
			Console.SetCursorPosition(0, 0);
			//same for horizontal
			Console.SetWindowPosition(0, 0);
			//resizing seems to restart this
			Console.CursorVisible = false;
		}

		public void SetMaxSize()
		{
			SetSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
		}

		public void SetWindowPosition(int px, int py)
		{
			IntPtr handle = GetConsoleWindow();
			SetWindowPos(handle, IntPtr.Zero, px, py, 0, 0, SWP_NOACTIVATE | SWP_NOACTIVATE | SWP_NOSIZE);
		}

		#region DLL Imports
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetConsoleWindow();

		private const int SWP_NOSIZE = 0x0001; //ignore pixewidth/height params
		private const int SWP_NOZORDER = 0x4; //don't change order of window
		private const int SWP_NOACTIVATE = 0x10;

		[DllImport("user32")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, 
			int x, int y, int pixelWidth, int pixelHeight, int flags);
		#endregion
	}
}
