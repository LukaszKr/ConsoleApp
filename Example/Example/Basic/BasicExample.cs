using ProceduralLevel.ConsoleApp.Input;
using System;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class BasicExample: AConsoleApp
	{
		private InputManager m_Input;
		private Window m_Console;
		private int m_Offset = 0;

		public BasicExample()
			: base(new Clock(20), new Clock(20))
		{
			m_Input = new InputManager();
			m_Console = new Window("BasicExample", 81, 21);
		}

		protected override void Render(double deltaTime)
		{
			string text = "Hello World!";
			int px = m_Console.Width/2-text.Length/2;
			int py = m_Console.Height/2;
			m_Console.Canvas.DrawText(text, px, py);
			m_Console.Canvas.DrawFrame(px-2, py-2, text.Length+4, 5, "-", "|", '+');
			m_Console.Canvas.DrawText(m_LogicClock.TickCount+":"+m_RenderClock.TickCount, 0, 0);
			m_Console.Canvas.DrawText((m_LogicClock.TickCount % 2 == 0? "+": "-"), m_Offset, 1);
			m_Console.Render();
		}

		protected override void Update(double deltaTime)
		{
			m_Input.Update(deltaTime);
			m_Offset ++;
			m_Offset = m_Offset % m_Console.Width;

			if(m_Input.Keyboard.Get(ConsoleKey.Escape).IsDown())
			{
				Exit();
			}
		}
	}
}
