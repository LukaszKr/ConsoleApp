namespace ProceduralLevel.ConsoleApp.Input
{
	public class MouseButtonProvider: AInputProvider
	{
		private EMouseButton m_Button;

		public MouseButtonProvider(EMouseButton button)
		{
			m_Button = button;
		}

		public override bool IsValid(AInputManager inputManager)
		{
			return inputManager.Mouse.Get(m_Button).IsDown();
		}

		public override string ToString()
		{
			return string.Format("[MouseButton: {0}]", m_Button);
		}
	}
}
