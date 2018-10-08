using ProceduralLevel.ConsoleApp.Input;
using System;
using System.Collections.Generic;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class InputExample: AExample
	{
		private const int GRID_X = 6;
		private const int GRID_Y = 2;

		public InputExample(InputManager inputManager) : base(inputManager)
		{
		}

		protected override void OnSetup()
		{
			ConsoleHelper.SetFont(new FontInfo(EFontFace.Consolas, EFontSize.Size_20));
		}

		protected override Window PrepareWindow()
		{
			return new Window("InputExample", false);
		}

		protected override void InitializeTimers(List<Timer> timers)
		{
			timers.Add(new Timer(50, Render));
			timers.Add(new Timer(10, Update));

			base.InitializeTimers(timers);
		}

		private void Render(Timer timer)
		{
			m_Console.Canvas.Clear();

			m_Console.Canvas.DrawLine(new Pixel('O', EColor.White, EColor.Black), m_Console.Canvas.Width/2, 0, m_Input.Mouse.X, m_Input.Mouse.Y);
			m_Console.Canvas.DrawEllipse(new Pixel('o', EColor.White, EColor.Black), m_Input.Mouse.X, m_Input.Mouse.Y, 8, 4);
			m_Console.Canvas.Plot(new Pixel('@', EColor.DarkBlue, EColor.DarkGray), m_Input.Mouse.X, m_Input.Mouse.Y);

			double averageFPS = Math.Round(timer.AverageFPS);
			double fps = Math.Round(timer.FPS);
			m_Console.Canvas.SetColor(EColor.White, EColor.Black);
			m_Console.Canvas.DrawText("FPS: "+fps+", Average FPS: "+averageFPS+", Mouse("+m_Input.Mouse.X+", "+m_Input.Mouse.Y+", "+m_Input.Mouse.Scroll+")", 0, 0);

			int offy = 1;

			if(m_Input.Keyboard.AnyKeyPressed)
			{
				EButtonState[] states = m_Input.Keyboard.GetKeys();
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

			if(m_Input.Mouse.AnyKeyPressed)
			{
				EButtonState[] states = m_Input.Mouse.GetKeys();
				for(int x = 0; x < states.Length; ++x)
				{
					EButtonState state = states[x];
					if(state != EButtonState.Released)
					{
						m_Console.Canvas.DrawText(string.Format("{0} [{1}]", ((EMouseButton)x).ToString(), state), 0, offy);
						++offy;
					}
				}
			}

			m_Console.Render();
		}

		private void Update(Timer timer)
		{

		}

		protected override void OnUpdateInput()
		{
			
		}
	}
}
