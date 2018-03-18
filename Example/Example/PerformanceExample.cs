using ProceduralLevel.ConsoleApp.Input;
using System;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class PerformanceExample: AConsoleApp
	{
		private Window m_Console;
		private Random m_Random;
		private InputManager m_Input;

		public PerformanceExample()
		{
			m_Random = new Random();
			m_Input = new InputManager();

			FontInfo info = new FontInfo(ETerminalFontSize.Size_8x12);
			ConsoleHelper.SetFont(info);
			//info = ConsoleHelper.GetFontInfo();
			m_Console = new Window("Performance Example");
		}

		protected override Timer[] InitializeTimers()
		{
			return new Timer[] { new Timer(60, Render), new Timer(10, UpdateInput) };
		}

		private void Render(double deltaTime)
		{
			Canvas canvas = m_Console.Canvas;
			int width = m_Console.Width;
			int height = m_Console.Height;

			double averageFPS = Math.Round(m_Timers[0].AverageFPS);
			double fps = Math.Round(m_Timers[0].FPS);
			canvas.SetColor(EColor.White, EColor.Black);
			canvas.DrawText("FPS: "+fps+", Average FPS: "+averageFPS, 0, 0);



			for(int x = 0; x < width; ++x)
			{
				for(int y = 1; y < height; ++y)
				{ 
					char c = (char)('A'+m_Random.Next(0, 20));
					EColor textColor = (EColor)m_Random.Next(0, 16);
					EColor bgColor = (EColor)m_Random.Next(0, 16);
					canvas.Plot(new Pixel(c, textColor, bgColor), x, y);
				}
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
	}
}
