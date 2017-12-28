using System;

namespace ProceduralLevel.ConsoleApp.Input
{
	public class KeyboardKeyProvider: AInputProvider
	{
		private ConsoleKey m_KeyCode;

		public KeyboardKeyProvider(ConsoleKey keyCode)
		{
			m_KeyCode = keyCode;
		}

		public override bool IsValid(AInputManager inputManager)
		{
			return inputManager.Keyboard.Get(m_KeyCode).IsDown();
		}

		public override string ToString()
		{
			return string.Format("[KeyCode: {0}]", m_KeyCode);
		}
	}
}
