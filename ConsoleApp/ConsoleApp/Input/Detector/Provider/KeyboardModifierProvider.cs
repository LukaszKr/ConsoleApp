using System;

namespace ProceduralLevel.ConsoleApp.Input
{
	public class KeyboardModifierProvider: AInputProvider
	{
		private ConsoleModifiers m_Modifier;

		public KeyboardModifierProvider(ConsoleModifiers modifier)
		{
			m_Modifier = modifier;
		}

		public override bool IsValid(AInputManager inputManager)
		{
			return inputManager.Keyboard.Get(m_Modifier).IsDown();
		}

		public override string ToString()
		{
			return string.Format("[Modifier: {0}]", m_Modifier);
		}
	}
}
