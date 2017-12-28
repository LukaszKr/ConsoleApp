using System.Diagnostics;

namespace ProceduralLevel.ConsoleApp
{
	public class Clock
	{
		private Stopwatch m_Watch;

		private double m_Remaining;
		private double m_FrameLength;

		public readonly int FPS;
		public bool Tick { get; private set; }
		public long TickCount { get; private set; }
		public double DeltaTime { get; private set; }

		public Clock(int fps)
		{
			FPS = fps;
			m_Watch = new Stopwatch();
			m_FrameLength = 1.0/FPS;
			m_Remaining = m_FrameLength;
		}

		public void Update()
		{
			long ticks = m_Watch.ElapsedTicks;
			double elapsedTime = (double)ticks/Stopwatch.Frequency;
			m_Remaining -= elapsedTime;
			m_Watch.Stop();
			m_Watch.Reset();
			m_Watch.Start();

			if(m_Remaining <= 0)
			{
				Tick = true;
				TickCount ++;
				DeltaTime = m_FrameLength-m_Remaining;
				m_Remaining = m_FrameLength;
			}
			else
			{
				Tick = false;
			}
		}
	}
}
