namespace ProceduralLevel.ConsoleApp.Input
{
	public abstract class AInputDevice
	{
		protected EButtonState[] m_KeyStates;

		public bool Enabled = true;
		protected bool m_IsActive;
		protected bool m_AnyKeyPressed;

		public bool IsActive { get { return m_IsActive; } }
		public bool AnyKeyPressed { get { return m_AnyKeyPressed; } }

		private bool[] m_ValidKeys;

		public AInputDevice(int buttonCount, bool[] validKeys = null)
		{
			m_ValidKeys = validKeys;
			m_KeyStates = new EButtonState[buttonCount];
		}

		public EButtonState[] GetButtons()
		{
			return m_KeyStates;
		}

		public void UpdateState()
		{
			if(!Enabled)
			{
				return;
			}

			OnUpdateState();
		}

		protected virtual void OnUpdateState()
		{
			m_IsActive = false;
			m_AnyKeyPressed = false;

			int length = m_KeyStates.Length;
			for(int codeValue = 0; codeValue < length; ++codeValue)
			{
				if(m_ValidKeys == null || m_ValidKeys[codeValue])
				{
					bool isPressed = IsPressed(codeValue);
					EButtonState oldState = m_KeyStates[codeValue];
					EButtonState newState = oldState.GetNextState(isPressed);
					m_KeyStates[codeValue] = newState;

					m_IsActive = m_IsActive || isPressed;
				}
			}
			m_AnyKeyPressed = m_IsActive;
		}

		protected abstract bool IsPressed(int codeValue);
	}
}
