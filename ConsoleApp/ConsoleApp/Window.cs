using System;

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
			Console.CursorVisible = false;
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
			Console.BufferWidth = width;
			Console.BufferHeight = height;
			//somehow removes column that was occupied by vertical scrollbar
			Console.SetCursorPosition(0, 0);
			//same for horizontal
			Console.SetWindowPosition(0, 0);
		}

		public void SetMaxSize()
		{
			SetSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
		}
	}
}
