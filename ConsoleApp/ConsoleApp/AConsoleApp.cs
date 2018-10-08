using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace ProceduralLevel.ConsoleApp
{
	public abstract class AConsoleApp
	{
		protected Timer[] m_Timers;
		private bool m_Exit;

		private Stopwatch m_Watch;

		public double DeltaTime { get; private set; }

		public AConsoleApp()
		{
			m_Watch = new Stopwatch();

			List<Timer> timers = new List<Timer>();
			InitializeTimers(timers);
			m_Timers = timers.ToArray();
		}

		protected abstract void InitializeTimers(List<Timer> timers);

		public void Run()
		{
			while(!m_Exit)
			{
				Thread.Sleep(1);
				Tick();
			}
		}

		public virtual void Tick()
		{
			long ticks = m_Watch.ElapsedTicks;
			DeltaTime = (double)ticks/Stopwatch.Frequency;
			m_Watch.Restart();

			for(int x = 0; x < m_Timers.Length; ++x)
			{
				m_Timers[x].Update(DeltaTime);
			}
		}

		public void ResetTimers()
		{
			m_Watch.Reset();
			for(int x = 0; x < m_Timers.Length; ++x)
			{
				m_Timers[x].Reset();
			}
		}

		public void Exit()
		{
			m_Exit = true;
		}
	}
}
