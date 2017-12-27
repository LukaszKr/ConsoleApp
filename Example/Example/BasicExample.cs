using System;

namespace ProceduralLevel.ConsoleCanvas.Example
{
	public class BasicExample
	{
		private Window m_Console;

		public BasicExample()
		{
			m_Console = new Window("BasicExample", 80, 21);
			m_Console.Canvas.Painter.DrawText("Hello World!", m_Console.Width/2, m_Console.Height/2, true);
			m_Console.Render();
			Console.ReadKey();
		}
	}
}
