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
			ConsoleHelper.SetFontFace(EFontFace.Terminal);
			ConsoleHelper.SetFontSize(EFontSize.Size_08);
			m_Console = new Window("Performance Example");
		}

		protected override Timer[] InitializeTimers()
		{
			return new Timer[] { new Timer(60, Render), new Timer(10, UpdateInput) };
		}

		private void Render(double deltaTime)
		{
			double averageFPS = Math.Round(m_Timers[0].AverageFPS);
			double fps = Math.Round(m_Timers[0].FPS);
			m_Console.Canvas.SetColor(EColor.White, EColor.Black);
			m_Console.Canvas.DrawText("FPS: "+fps+", Average FPS: "+averageFPS, 0, 0);



			for(int x = 0; x < m_Console.Width; ++x)
			{
				for(int y = 1; y < m_Console.Height; ++y)
				{ 
					char c = (char)((int)'A'+m_Random.Next(0, 20));
					EColor textColor = (EColor)m_Random.Next(0, 16);
					EColor bgColor = (EColor)m_Random.Next(0, 16);
					m_Console.Canvas.Plot(new Pixel(c, textColor, bgColor), x, y);
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
