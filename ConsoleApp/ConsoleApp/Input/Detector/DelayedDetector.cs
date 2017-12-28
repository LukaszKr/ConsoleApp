using System;

namespace ProceduralLevel.ConsoleApp.Input
{
	public abstract class DelayedDetector: DurationDetector
	{
		private bool m_Detected;

		public float Progress { get { return Math.Max(Duration/Delay, 1f); } }
		public float Delay { get; private set; }

		public DelayedDetector(AInputProvider inputProvider, float delay)
			: base(inputProvider)
		{
			Delay = delay;
		}

		#region Shortcut Constructors
		public DelayedDetector(ConsoleKey key, float delay)
			: this(new KeyboardKeyProvider(key), delay)
		{

		}

		public DelayedDetector(ConsoleModifiers modifiers, float delay)
			: this(new KeyboardModifierProvider(modifiers), delay)
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
