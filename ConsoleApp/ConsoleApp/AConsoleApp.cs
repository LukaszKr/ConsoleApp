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
			m_Timers = InitializeTimers();
		}

		protected abstract Timer[] InitializeTimers();

		public void Run()
		{
			Setup();
			while(!m_Exit)
			{
				Thread.Sleep(1);

				long ticks = m_Watch.ElapsedTicks;
				DeltaTime = (double)ticks/Stopwatch.Frequency;
				m_Watch.Restart();

				for(int x = 0; x < m_Timers.Length; ++x)
				{
					m_Timers[x].Update(DeltaTime);
				}

			}
		}

		protected abstract void Setup();

		protected void Exit()
		{
			m_Exit = true;
		}
	}
}
