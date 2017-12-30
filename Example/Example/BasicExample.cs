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
			m_Console = new Window("BasicExample");
		}

		protected override Timer[] InitializeTimers()
		{
			return new Timer[]
			{
				new Timer(60, Render), new Timer(10, UpdateInput), new Timer(5, Update)
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
		}

		private void Render(double deltaTime)
		{
			//this could be done once as it doesn't change
			//putting it here to test performance
			for(int x = 0; x < m_Console.Width; ++x)
			{
				for(int y = 0; y < m_Console.Height; ++y)
				{
					m_Console.Canvas.Plot(new Pixel((m_Offset % 2 == 0? '.': ','), EColor.DarkGray, EColor.Black), x, y);
				}
			}

			string text = "Hello World!";
			int px = m_Console.Width/4-text.Length/2;
			int py = m_Console.Height/2;
			m_Console.Canvas.Clear(px-1, py-1, text.Length+2, 3);
			m_Console.Canvas.DrawText(text, px, py);
			m_Console.Canvas.SetColor(EColor.White, EColor.Gray);
			m_Console.Canvas.DrawFrame(px-2, py-2, text.Length+4, 5, "-", "|", '#');


			int cx = (int)(m_Console.Width*0.75f);
			int cy = m_Console.Height/2;
			int lines = 20;
			int leng = 14;
			double rotSteps = 60;
			double offset = (m_Offset % rotSteps)/rotSteps;
			double doublePI = Math.PI*2;
			m_Console.Canvas.SetColor(EColor.Green, EColor.Black);
			for(int x = 0; x < lines; ++x)
			{
				double rad = doublePI*(x/(double)lines)+doublePI*offset;
				int dx = cx+(int)Math.Ceiling((Math.Cos(rad)*leng));
				int dy = cy+(int)Math.Ceiling((Math.Sin(rad)*leng));
				m_Console.Canvas.DrawLine('#', cx, cy, dx, dy);
			}

			m_Console.Canvas.SetColor(EColor.Red, EColor.Black);
			m_Console.Canvas.DrawCircle('@', cx, cy, leng+1);
			m_Console.Canvas.SetColor(EColor.White, EColor.Black);
			
			double averageFPS = Math.Round(m_Timers[0].AverageFPS);
			double fps = Math.Round(m_Timers[0].FPS);
			m_Console.Canvas.DrawText("FPS: "+fps+", Average FPS: "+averageFPS, 0, 0);
			m_Console.Canvas.DrawText((m_Offset % 2 == 0? "+": "-"), m_Offset % m_Console.Width, 1);
			m_Console.Render();
		}
	}
}
