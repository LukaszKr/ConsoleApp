using ProceduralLevel.ConsoleApp.Input;
using System;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class ColorExample: AConsoleApp
	{
		private const int GRID_X = 4;
		private const int GRID_Y = 2;

		private InputManager m_Input;
		private Window m_Console;

		public ColorExample()
		{
			ConsoleHelper.SetWindowPosition(0, 0);

			m_Input = new InputManager();
			m_Console = new Window("ColorExample", 16*GRID_X, 16*GRID_Y+1);
		}

		protected override Timer[] InitializeTimers()
		{
			return new Timer[]
			{
				new Timer(60, Render), new Timer(10, UpdateInput), new Timer(10, Update)
			};
		}

		private void Render(double deltaTime)
		{
			double averageFPS = Math.Round(m_Timers[0].AverageFPS, 2);
			double fps = Math.Round(m_Timers[0].FPS, 2);
			m_Console.Canvas.SetColor(EColor.White, EColor.Black);
			m_Console.Canvas.DrawText("FPS: "+fps+", Average FPS: "+averageFPS, 0, 0);

			EColor textColor = 0;
			EColor bgColor = 0;
			for(int x = 0; x < 16; ++x)
			{
				for(int y = 0; y < 16; ++y)
				{
					m_Console.Canvas.SetColor(textColor, bgColor);
					m_Console.Canvas.DrawRect(" ", x*GRID_X, y*GRID_Y+1, GRID_X, GRID_Y);
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

		}
	}
}
