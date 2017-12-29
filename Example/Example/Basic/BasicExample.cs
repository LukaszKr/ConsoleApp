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
		{
			m_Input = new InputManager();
			//m_Console = new Window("BasicExample", 81, 36);
			m_Console = new Window("BasicExample");
			m_Console.SetWindowPosition(0, 0);
		}

		protected override Timer[] InitializeTimers()
		{
			return new Timer[]
			{
				new Timer(20, UpdateInput), new Timer(20, Update), new Timer(20, Render)
			};
		}

		private void UpdateInput(double deltaTime)
		{
			m_Input.Update(deltaTime);

			if(m_Input.Get(ConsoleKey.Escape).IsDown())
			{
				Exit();
			}
		}

		private void Update(double deltaTime)
		{
			m_Offset++;
			m_Offset = m_Offset % m_Console.Width;
		}

		private void Render(double deltaTime)
		{
			//this could be done once as it doesn't change
			//putting it here to test performance
			for(int x = 0; x < m_Console.Width; ++x)
			{
				for(int y = 0; y < m_Console.Height; ++y)
				{
					m_Console.Canvas.Plot(new Pixel('.', ConsoleColor.DarkGray, ConsoleColor.Black), x, y);
				}
			}

			string text = "Hello World!";
			int px = m_Console.Width/4-text.Length/2;
			int py = m_Console.Height/2;
			m_Console.Canvas.Clear(px-1, py-1, text.Length+2, 3);
			m_Console.Canvas.DrawText(text, px, py);
			m_Console.Canvas.SetColor(ConsoleColor.White, ConsoleColor.DarkGray);
			m_Console.Canvas.DrawFrame(px-2, py-2, text.Length+4, 5, "-", "|", '#');


			int cx = (int)(m_Console.Width*0.75f);
			int cy = m_Console.Height/2;
			int lines = 16;
			int leng = 12;
			m_Console.Canvas.SetColor(ConsoleColor.DarkGreen, ConsoleColor.Black);
			for(int x = 0; x < lines; ++x)
			{
				double rad = (2*Math.PI)*(x/(double)lines);
				m_Console.Canvas.DrawLine('#', cx, cy, cx+(int)(Math.Cos(rad)*leng), cy+(int)(Math.Sin(rad)*leng));
			}

			m_Console.Canvas.SetColor(ConsoleColor.White, ConsoleColor.Black);
			

			m_Console.Canvas.DrawText(m_Timers[0].TickCount+":"+m_Timers[1].TickCount, 0, 0);
			m_Console.Canvas.DrawText((m_Timers[0].TickCount % 2 == 0? "+": "-"), m_Offset, 1);
			m_Console.Render();
		}
	}
}
