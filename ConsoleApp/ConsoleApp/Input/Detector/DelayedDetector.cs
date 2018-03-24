using System;

namespace ProceduralLevel.ConsoleApp.Input
{
	public abstract class DelayedDetector: DurationDetector
	{
		private bool m_Detected;

		public double Progress { get { return Math.Max(Duration/Delay, 1.0); } }
		public double Delay { get; private set; }

		public DelayedDetector(AInputProvider inputProvider, double delay)
			: base(inputProvider)
		{
			Delay = delay;
		}

		#region Shortcut Constructors
		public DelayedDetector(ConsoleKey key, double delay)
			: this(new KeyboardKeyProvider(key), delay)
		{

		}

		public DelayedDetector(EInputModifier modifiers, double delay)
			: this(new KeyboardModifierProvider(modifiers), delay)
		{

		}

		public DelayedDetector(EMouseButton button, double delay)
			: this(new MouseButtonProvider(button), delay)
		{

		}
		#endregion

		protected override bool OnUpdate(AInputManager inputManager)
		{
			base.OnUpdate(inputManager);

			if(!m_Detected && Duration >= Delay)
			{
				m_Detected = true;
				return true;
			}
			return false;
		}

		protected override void OnReset(AInputManager inputManager)
		{
			base.OnReset(inputManager);
			m_Detected = false;
		}

		public override string ToString()
		{
			return base.ToString()+string.Format("[Detected: {0}, Progress: {1}, Delay: {2}]", m_Detected, Progress, Delay);
		}
	}
}
