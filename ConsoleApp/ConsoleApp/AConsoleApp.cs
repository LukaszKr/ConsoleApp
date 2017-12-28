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
					Update(m_LogicClock.DeltaTime);
				}
				if(m_RenderClock.Tick)
				{
					Render(m_RenderClock.DeltaTime);
				}
			}
		}

		protected void Exit()
		{
			m_Exit = true;
		}

		protected abstract void Render(double deltaTime);
		protected abstract void Update(double deltaTime);
	}
}
