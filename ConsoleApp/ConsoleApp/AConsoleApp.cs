using System.Diagnostics;
using System.Threading;

namespace ProceduralLevel.ConsoleApp
{
	public abstract class AConsoleApp
	{
		protected Clock m_LogicClock;
		protected Clock m_RenderClock;
		private bool m_Exit;

		private Stopwatch m_Watch;

		public double DeltaTime { get; private set; }

		public AConsoleApp(Clock logicClock, Clock renderClock)
		{
			m_Watch = new Stopwatch();

			m_LogicClock = logicClock;
			m_RenderClock = renderClock;
		}

		public void Run()
		{
			while(!m_Exit)
			{
				Thread.Sleep(1);
				UpdateTime();

				m_LogicClock.Update(DeltaTime);
				m_RenderClock.Update(DeltaTime);
				if(m_LogicClock.Tick)
				{
					Update(DeltaTime);
				}
				if(m_RenderClock.Tick)
				{
					Render(DeltaTime);
				}
			}
		}

		private void UpdateTime()
		{
			long ticks = m_Watch.ElapsedTicks;
			DeltaTime = (double)ticks/Stopwatch.Frequency;
			m_Watch.Restart();
		}

		protected void Exit()
		{
			m_Exit = true;
		}

		protected abstract void Render(double deltaTime);
		protected abstract void Update(double deltaTime);
	}
}
