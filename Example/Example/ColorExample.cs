using System;
using System.Collections.Generic;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class ColorExample: AExample
	{
		private const int GRID_X = 6;
		private const int GRID_Y = 2;

		public ColorExample(InputManager inputManager) : base(inputManager)
		{
		}

		protected override void OnSetup()
		{
		}

		protected override Window PrepareWindow()
		{
			return new Window("ColorExample", 16*GRID_X, 16*GRID_Y+1);
		}

		protected override void InitializeTimers(List<Timer> timers)
		{
			base.InitializeTimers(timers);

			timers.Add(new Timer(60, Render));
			timers.Add(new Timer(10, Update));
		}

		private void Render(double deltaTime)
		{
			double averageFPS = Math.Round(m_Timers[0].AverageFPS);
			double fps = Math.Round(m_Timers[0].FPS);
			m_Console.Canvas.SetColor(EColor.White, EColor.Black);
			m_Console.Canvas.Clear(0, 0, m_Console.Canvas.Width, 1);
			m_Console.Canvas.DrawText("FPS: "+fps+", Average FPS: "+averageFPS, 0, 0);

			EColor textColor = 0;
			EColor bgColor = 0;
			for(int x = 0; x < 16; ++x)
			{
				for(int y = 0; y < 16; ++y)
				{
					m_Console.Canvas.SetColor(textColor, bgColor);
					m_Console.Canvas.DrawRect(new Pixel(' ', textColor, bgColor), x*GRID_X, y*GRID_Y+1, GRID_X, GRID_Y);
					string textBin = Convert.ToString((int)textColor, 2).PadLeft(4, '0');
					string bgBin = Convert.ToString((int)bgColor, 2).PadLeft(4, '0');
					m_Console.Canvas.DrawText(bgBin, x*GRID_X, y*GRID_Y+1);
					m_Console.Canvas.DrawText(textBin, x*GRID_X, y*GRID_Y+2);
					++textColor;
				}
				++bgColor;
				textColor = 0;
			}
			m_Console.Render();
		}

		private void Update(double deltaTime)
		{

		}

		protected override void OnUpdateInput()
		{
			
		}
	}
}
