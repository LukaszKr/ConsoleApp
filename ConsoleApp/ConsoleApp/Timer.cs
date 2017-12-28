namespace ProceduralLevel.ConsoleApp
{
	public class Timer
	{
		private double m_Remaining;
		private double m_FrameLength;
		private TickCallback m_Callback;

		public readonly int FPS;
		public long TickCount { get; private set; }

		public delegate void TickCallback(double deltaTime);

		public Timer(int fps, TickCallback callback)
		{
			FPS = fps;
			m_FrameLength = 1.0/FPS;
			m_Remaining = m_FrameLength;
			m_Callback = callback;
		}

		public void Update(double deltaTime)
		{
			m_Remaining -= deltaTime;
			if(m_Remaining <= 0)
			{
				TickCount ++;
				double totalDeltaTime = m_FrameLength-m_Remaining;
				m_Remaining = m_FrameLength;
				m_Callback(totalDeltaTime);
			}
		}
	}
}
