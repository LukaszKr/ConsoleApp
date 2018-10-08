namespace ProceduralLevel.ConsoleApp
{
	public class Timer
	{		
		private TickCallback m_Callback;
		private double m_Remaining;
		private double m_AverageSmooth;

		public readonly double FrameTime;
		public readonly int TargetFPS;
		public double FPS { get; private set; }

		public double AverageFPS { get; private set; }

		public long TickCount { get; private set; }

		public delegate void TickCallback(Timer timer);

		public Timer(int targetFPS, TickCallback callback)
		{
			TargetFPS = targetFPS;
			FrameTime = 1.0/TargetFPS;
			m_Callback = callback;
			m_AverageSmooth = 1-FrameTime;

			Reset();
		}

		public void Reset()
		{
			m_Remaining = FrameTime;
			FPS = TargetFPS;
			AverageFPS = TargetFPS/2;
		}

		public void Update(double deltaTime)
		{
			m_Remaining -= deltaTime;
			FPS = 1.0/deltaTime;
			FPS = (TargetFPS > FPS ? FPS : TargetFPS);
			AverageFPS = FPS*m_AverageSmooth+(AverageFPS*(1-m_AverageSmooth));

			if(m_Remaining <= 0f) //some issue here, when on low FPS it's spiking randomly causing Average FPS to go a bit crazy
			{
				TickCount ++;
				m_Remaining += FrameTime;
				m_Callback(this);
			}
		}

		public bool IsFallingBehind()
		{
			return m_Remaining < -FrameTime;
		}

		public void SkipTick()
		{
			m_Remaining += FrameTime;
		}
	}
}
