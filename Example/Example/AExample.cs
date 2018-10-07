using System.Collections.Generic;

namespace ProceduralLevel.ConsoleApp.Example
{
	public abstract class AExample: AConsoleApp
	{
		protected readonly InputManager m_Input;
		protected Window m_Console;

		public AExample(InputManager inputManager)
		{
			m_Input = inputManager;
		}

		public void Setup()
		{
			OnSetup();
			m_Console = PrepareWindow();
		}

		protected override void InitializeTimers(List<Timer> timers)
		{
			timers.Add(new Timer(100, UpdateInput));
		}

		private void UpdateInput(double deltaTime)
		{
			OnUpdateInput();
		}

		protected abstract void OnSetup();
		protected abstract Window PrepareWindow();
		protected abstract void OnUpdateInput();
	}
}
