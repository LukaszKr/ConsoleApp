using System.Threading;

namespace ProceduralLevel.ConsoleApp
{
	public abstract class AConsoleApp
	{
		protected Clock m_LogicClock;
		protected Clock m_RenderClock;
		private bool m_Exit;

		public AConsoleApp(Clock logicClock, Clock renderClock)
		{
			m_LogicClock = logicClock;
			m_RenderClock = renderClock;
		}

		public void Run()
		{
			while(!m_Exit)
			{
				Thread.Sleep(1);

				m_LogicClock.Update();
				m_RenderClock.Update();
				if(m_LogicClock.Tick)
				{
					Update();
				}
				if(m_RenderClock.Tick)
				{
					Render();
				}
			}
		}

		protected void Exit()
		{
			m_Exit = true;
		}

		protected abstract void Render();
		protected abstract void Update();
	}
}
