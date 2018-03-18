namespace ProceduralLevel.ConsoleApp
{
	public class Timer
	{
		private double m_Remaining;
		private double m_FrameTime;
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
			m_FrameTime = 1.0/TargetFPS;
			m_Remaining = m_FrameTime;
			m_Callback = callback;

			AverageFPS = 0f;
			m_SampleWeight = m_FrameTime;
		}

		public void Update(double deltaTime)
		{
			m_Remaining -= deltaTime;
			if(m_Remaining <= 0f) //some issue here, when on low FPS it's spiking randomly causing Average FPS to go a bit crazy
			{
				TickCount ++;
				double timePassed = m_FrameTime-m_Remaining;
				FPS = 1.0/timePassed;
				AverageFPS = (1-m_SampleWeight)*AverageFPS+FPS*m_SampleWeight;
				m_Remaining = m_FrameTime;
				m_Callback(timePassed);
			}
		}
	}
}
