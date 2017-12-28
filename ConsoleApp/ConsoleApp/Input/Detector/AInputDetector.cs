namespace ProceduralLevel.ConsoleApp.Input
{
	public abstract class AInputDetector: ADetector
	{
		private AInputProvider m_InputProvider;

		private bool m_Triggered;

		public override bool Triggered { get { return m_Triggered; } }

		public AInputDetector(AInputProvider inputProvider)
		{
			m_InputProvider = inputProvider;
		}

		public override void Update(AInputManager inputManager)
		{
			if(m_InputProvider.IsValid(inputManager))
			{
				m_Triggered = OnUpdate(inputManager);
			}
			else
			{
				m_Triggered = false;
				OnReset(inputManager);
			}
		}

		protected abstract bool OnUpdate(AInputManager inputManager);
		protected abstract void OnReset(AInputManager inputManager);

		public override string ToString()
		{
			return base.ToString()+string.Format("[InputProvider: {0}]", m_InputProvider);
		}
	}
}
