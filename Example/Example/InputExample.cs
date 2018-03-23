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

			double averageFPS = Math.Round(m_Timers[0].AverageFPS);
			double fps = Math.Round(m_Timers[0].FPS);
			m_Console.Canvas.SetColor(EColor.White, EColor.Black);
			m_Console.Canvas.Clear(0, 0, m_Console.Canvas.Width, 1);
			m_Console.Canvas.DrawText("FPS: "+fps+", Average FPS: "+averageFPS, 0, 0);
			
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
