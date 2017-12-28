using System.Collections.Generic;

namespace ProceduralLevel.ConsoleApp.Input
{
	public abstract class AGroupDetector: ADetector
	{
		private List<ADetector> m_Detectors = new List<ADetector>();
		private bool m_Triggered;

		public override bool Triggered { get { return m_Triggered; } }

		protected abstract bool DefaultState { get; }

		public AGroupDetector(params ADetector[] detectors)
			: base()
		{
			for(int x = 0; x < detectors.Length; ++x)
			{
				m_Detectors.Add(detectors[x]);
			}
		}

		public override void Update(AInputManager inputManager)
		{
			m_Triggered = DefaultState;
			int count = m_Detectors.Count;
			for(int x = 0; x < count; ++x)
			{
				ADetector detector = m_Detectors[x];
				detector.Update(inputManager);
				m_Triggered = IsTriggered(m_Triggered, detector.Triggered);
			}
		}

		protected abstract bool IsTriggered(bool collectiveState, bool detectorState);
	}
}
