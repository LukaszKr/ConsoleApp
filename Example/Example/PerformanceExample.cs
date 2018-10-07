using System;
using System.Collections.Generic;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class PerformanceExample: AExample
	{
		private Random m_Random;

		private int m_Offset = 0;

		public PerformanceExample(InputManager inputManager) : base(inputManager)
		{
		}

		protected override void OnSetup()
		{
			m_Random = new Random();

			FontInfo info = new FontInfo(EFontFace.Consolas, EFontSize.Size_16, EFontWeight.Weight_300);
			ConsoleHelper.SetFont(info);
		}

		protected override Window PrepareWindow()
		{
			return new Window("Performance Example", true);
		}

		protected override void InitializeTimers(List<Timer> timers)
		{
			base.InitializeTimers(timers);

			timers.Add(new Timer(60, Render));
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

		protected override void OnUpdateInput()
		{
		}
	}
}
