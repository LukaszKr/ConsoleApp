using ProceduralLevel.ConsoleApp.Input;
using System;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class PerformanceExample: AConsoleApp
	{
		private Window m_Console;
		private Random m_Random;
		private InputManager m_Input;

		private int m_Offset = 0;

		protected override void Setup()
		{
			m_Random = new Random();
			m_Input = new InputManager();

			FontInfo info = new FontInfo(EFontFace.Consolas, EFontSize.Size_16, EFontWeight.Weight_300);
			ConsoleHelper.SetFont(info);
			m_Console = new Window("Performance Example", true);
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
			canvas.Clear(0, 0, canvas.Width, 1);
			canvas.DrawText("FPS: "+fps+", Average FPS: "+averageFPS, 0, 0);
			++m_Offset;

			for(int x = 0; x < width; ++x)
			{
				for(int y = 1; y < height; ++y)
				{
					//console doesn't like changing colors between cells - so this is a worst case scenario
					//bottleneck here is pInvoke to native code which might be still improved
					char c = (char)('A'+((m_Offset+x) % 23));
					EColor textColor = (EColor)((m_Offset+x) % 16);
					EColor bgColor = (EColor)((m_Offset+x+y) % 16);
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
