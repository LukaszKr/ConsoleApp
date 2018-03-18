namespace ProceduralLevel.ConsoleApp
{
	public class Timer
	{

		private double m_Remaining;
		private double m_FrameLength;
		private TickCallback m_Callback;
		private double m_SampleWeight;

		public readonly int TargetFPS;
		public double FPS { get; private set; }

		public double AverageFPS { get; private set; }

		public long TickCount { get; private set; }

		public delegate void TickCallback(double deltaTime);

		public Timer(int targetFPS, TickCallback callback)
		{
			TargetFPS = targetFPS;
			m_FrameLength = 1.0/TargetFPS;
			m_Remaining = m_FrameLength;
			m_Callback = callback;

			AverageFPS = 0f;
			m_SampleWeight = 0.1f;
		}

		public void Update(double deltaTime)
		{
			m_Remaining -= deltaTime;
			if(m_Remaining <= 0)
			{
				TickCount ++;
				double totalDeltaTime = m_FrameLength-m_Remaining;
				FPS = 1.0/totalDeltaTime;
				AverageFPS = (1-m_SampleWeight)*AverageFPS+FPS*m_SampleWeight;

				m_Remaining = m_FrameLength;
				m_Callback(totalDeltaTime);
			}
		}
	}
}
