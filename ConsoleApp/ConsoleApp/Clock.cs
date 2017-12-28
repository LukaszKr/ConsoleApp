namespace ProceduralLevel.ConsoleApp
{
	public class Clock
	{

		private double m_Remaining;
		private double m_FrameLength;

		public readonly int FPS;
		public bool Tick { get; private set; }
		public long TickCount { get; private set; }

		public Clock(int fps)
		{
			FPS = fps;
			m_FrameLength = 1.0/FPS;
			m_Remaining = m_FrameLength;
		}

		public void Update(double deltaTime)
		{
			m_Remaining -= deltaTime;
			if(m_Remaining <= 0)
			{
				Tick = true;
				TickCount ++;
				deltaTime = m_FrameLength-m_Remaining;
				m_Remaining = m_FrameLength;
			}
			else
			{
				Tick = false;
			}
		}
	}
}
