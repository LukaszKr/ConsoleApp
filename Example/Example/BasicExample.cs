using System;
using System.Collections.Generic;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class BasicExample: AExample
	{
		private int m_Offset = 0;

		public BasicExample(InputManager inputManager) : base(inputManager)
		{
		}

		protected override void OnSetup()
		{
			ConsoleHelper.SetFontSize(EFontSize.Size_16);
			ConsoleHelper.SetFontFace(EFontFace.Consolas);
		}

		protected override Window PrepareWindow()
		{
			return new Window("BasicExample", 130, 50);
		}

		protected override void InitializeTimers(List<Timer> timers)
		{
			base.InitializeTimers(timers);

			timers.Add(new Timer(200, Render));
			timers.Add(new Timer(5, Update));
		}

		private void Update(Timer timer)
		{
			m_Offset++;
		}

		private void Render(Timer timer)
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
			m_Console.Canvas.Clear(0, 0, m_Console.Canvas.Width, 1);
			m_Console.Canvas.DrawFrame(px-2, py-2, text.Length+4, 5, 
				new Pixel('-', EColor.White, EColor.Gray), 
				new Pixel('|', EColor.White, EColor.Gray), 
				new Pixel('#', EColor.White, EColor.Gray));


			int cx = (int)(m_Console.Width*0.75f);
			int cy = m_Console.Height/2;
			int lines = 20;
			int leng = 14;
			double rotSteps = 60;
			double offset = (m_Offset % rotSteps)/rotSteps;
			double doublePI = Math.PI*2;
			for(int x = 0; x < lines; ++x)
			{
				double rad = doublePI*(x/(double)lines)+doublePI*offset;
				int dx = cx+(int)Math.Ceiling((Math.Cos(rad)*leng));
				int dy = cy+(int)Math.Ceiling((Math.Sin(rad)*leng));
				m_Console.Canvas.DrawLine(new Pixel('#', EColor.Green, EColor.Black), cx, cy, dx, dy);
			}

			m_Console.Canvas.DrawCircle(new Pixel('@', EColor.Red, EColor.Black), cx, cy, leng+1);
			m_Console.Canvas.SetColor(EColor.White, EColor.Black);
			
			double averageFPS = Math.Round(timer.AverageFPS);
			double fps = Math.Round(timer.FPS);
			m_Console.Canvas.DrawText("FPS: "+fps+", Average FPS: "+averageFPS, 0, 0);
			m_Console.Canvas.DrawText((m_Offset % 2 == 0? "+": "-"), m_Offset % m_Console.Width, 1);
			m_Console.Render();
		}

		protected override void OnUpdateInput()
		{

		}
	}
}
