using System;

namespace ProceduralLevel.ConsoleCanvas.Example
{
	public class BasicExample
	{
		private Window m_Console;

		public BasicExample()
		{
			m_Console = new Window("BasicExample", 80, 21);
			string text = "Hello World!";
			int px = m_Console.Width/2-text.Length/2;
			int py = m_Console.Height/2;
			m_Console.Canvas.Painter.DrawText(text, px, py);
			m_Console.Canvas.Painter.DrawFrame(px-2, py-2, text.Length+4, 5, "-", "|", '+');
			m_Console.Render();
			Console.ReadKey();
		}
	}
}
