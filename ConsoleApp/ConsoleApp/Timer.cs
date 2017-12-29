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

		private double m_AverageTime;
		public double AverageFPS { get; private set; }

		public long TickCount { get; private set; }

		public delegate void TickCallback(double deltaTime);

		public Timer(int targetFPS, TickCallback callback)
		{
			TargetFPS = targetFPS;
			m_FrameLength = 1.0/TargetFPS;
			m_Remaining = m_FrameLength;
			m_Callback = callback;

			m_AverageTime = m_FrameLength;
			m_SampleWeight = 1.0/targetFPS;
		}

		public void Update(double deltaTime)
		{
			m_Remaining -= deltaTime;
			if(m_Remaining <= 0)
			{
				TickCount ++;
				double totalDeltaTime = m_FrameLength-m_Remaining;
				m_AverageTime = (1-m_SampleWeight)*m_AverageTime+totalDeltaTime*m_SampleWeight;
				AverageFPS = 1.0/m_AverageTime;
				FPS = 1.0/totalDeltaTime;

				m_Remaining = m_FrameLength;
				m_Callback(totalDeltaTime);
			}
		}
	}
}
