using ProceduralLevel.ConsoleApp.Import;
using System;

namespace ProceduralLevel.ConsoleApp
{
	public class Window
	{
		private Canvas m_Canvas;
		private int m_Width;
		private int m_Height;

		private Pixel[] m_Buffer;

		public int Width { get { return m_Width; } }
		public int Height { get { return m_Height; } }

		public Canvas Canvas { get { return m_Canvas; } }

		public Window(string title)
			//without fullscreen last line hides under taskbar
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
				Console.BufferWidth != m_Width || Console.BufferHeight != m_Height)
			{
				SetSize(m_Width, m_Height);
			}
			m_Canvas.Render(this, 0, 0);

			ConsoleHelper.WriteOutput(m_Buffer, new Coord(Width, Height), new Coord(0, 0));
		}

		public void Plot(Pixel pixel, int x, int y)
		{
			m_Buffer[y*Width+x] = pixel;
		}

		public void SetSize(int width, int height)
		{
			m_Buffer = new Pixel[width*height];

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
			SetSize(Console.LargestWindowWidth, Console.LargestWindowHeight-1);
		}
	}
}
