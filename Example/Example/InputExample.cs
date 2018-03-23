using ProceduralLevel.ConsoleApp.Input;
using System;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class InputExample: AConsoleApp
	{
		private const int GRID_X = 6;
		private const int GRID_Y = 2;

		private InputManager m_Input;
		private Window m_Console;

		public InputExample()
		{
			ConsoleHelper.SetFont(new FontInfo(EFontFace.Consolas, EFontSize.Size_20));
			m_Input = new InputManager();
			m_Console = new Window("InputExample");
		}

		protected override Timer[] InitializeTimers()
		{
			return new Timer[]
			{
				new Timer(60, Render), new Timer(20, UpdateInput), new Timer(10, Update)
			};
		}

		private void Render(double deltaTime)
		{
			m_Console.Canvas.Clear();

			m_Console.Canvas.DrawLine('O', m_Console.Canvas.Width/2, 0, m_Input.Mouse.X, m_Input.Mouse.Y);
			m_Console.Canvas.DrawEllipse('o', m_Input.Mouse.X, m_Input.Mouse.Y, 8, 4);
			m_Console.Canvas.Plot(new Pixel('@', EColor.DarkBlue, EColor.DarkGray), m_Input.Mouse.X, m_Input.Mouse.Y);

			double averageFPS = Math.Round(m_Timers[0].AverageFPS);
			double fps = Math.Round(m_Timers[0].FPS);
			m_Console.Canvas.SetColor(EColor.White, EColor.Black);
			m_Console.Canvas.DrawText("FPS: "+fps+", Average FPS: "+averageFPS+", Mouse("+m_Input.Mouse.X+", "+m_Input.Mouse.Y+")", 0, 0);

			if(m_Input.Keyboard.AnyKeyPressed)
			{
				int offy = 1;
				EButtonState[] states = m_Input.Keyboard.GetButtons();
				for(int x = 0; x < states.Length; ++x)
				{
					EButtonState state = states[x];
					if(state != EButtonState.Released)
					{
						string name;
						if(x >= 8)
						{
							name = ((ConsoleKey)x).ToString();
						}
						else
						{
							name = ((EInputModifier)x).ToString();
						}
						m_Console.Canvas.DrawText(string.Format("{0} [{1}]", name, state), 0, offy);
						++offy;
					}
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

		private void Update(double deltaTime)
		{

		}
	}
}
