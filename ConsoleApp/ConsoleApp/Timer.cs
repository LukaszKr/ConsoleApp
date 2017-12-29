namespace ProceduralLevel.ConsoleApp
{
	public class Timer
	{
		private const double SAMPLE_WEIGHT = 0.1;

		private double m_Remaining;
		private double m_FrameLength;
		private TickCallback m_Callback;

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
		}

		public void Update(double deltaTime)
		{
			m_Remaining -= deltaTime;
			if(m_Remaining <= 0)
			{
				TickCount ++;
				double totalDeltaTime = m_FrameLength-m_Remaining;
				m_AverageTime = (1-SAMPLE_WEIGHT)*m_AverageTime+totalDeltaTime*SAMPLE_WEIGHT;
				AverageFPS = 1.0/m_AverageTime;
				FPS = 1.0/totalDeltaTime;

				m_Remaining = m_FrameLength;
				m_Callback(totalDeltaTime);
			}
		}
	}
}
