using System;

namespace ProceduralLevel.ConsoleCanvas
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
			: this(title, Console.LargestWindowWidth, Console.LargestWindowHeight-1)
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
				Console.BufferWidth != m_Width || Console.BufferHeight != m_Height+1)
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
			Console.BufferHeight = height+1;
		}

		public void SetMaxSize()
		{
			Console.SetWindowPosition(0, 0);
			SetSize(Console.LargestWindowWidth, Console.LargestWindowHeight-1);
		}
	}
}
