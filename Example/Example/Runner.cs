using ProceduralLevel.ConsoleApp.Input;
using System;
using System.Collections.Generic;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class Runner: AConsoleApp
	{
		private const int START_AT = 1;

		protected readonly InputManager m_Input;
		private int m_Current;
		private AExample m_Running = null;

		private readonly MultipleDetector m_ExitInput = new MultipleDetector(
			new TriggerDetector(ConsoleKey.Escape)
		);
		private readonly MultipleDetector m_PrevExampleInput = new MultipleDetector(
			new TriggerDetector(ConsoleKey.A), new TriggerDetector(ConsoleKey.LeftArrow), new TriggerDetector(ConsoleKey.PageDown)
		);
		private readonly MultipleDetector m_NextExampleInput = new MultipleDetector(
			new TriggerDetector(ConsoleKey.D), new TriggerDetector(ConsoleKey.RightArrow), new TriggerDetector(ConsoleKey.PageUp)
		);
		private readonly DetectorUpdater m_Updater;

		private readonly AExample[] m_Examples;

		public Runner()
		{
			m_Input = new InputManager();

			m_Examples = new AExample[]
			{
				new PerformanceExample(m_Input),
				new BasicExample(m_Input),
				new ColorExample(m_Input),
				new InputExample(m_Input)
			};

			m_Updater = new DetectorUpdater(m_ExitInput, m_PrevExampleInput, m_NextExampleInput);
			SetCurrent(m_Current);

		}

		protected override void InitializeTimers(List<Timer> timers)
		{
			timers.Add(new Timer(100, UpdateInput));
		}

		private void UpdateInput(double deltaTime)
		{
			m_Input.Update(deltaTime);
			m_Updater.Update(m_Input);

			if(m_ExitInput.Triggered)
			{
				Exit();
			}

			if(m_NextExampleInput.Triggered)
			{
				SetCurrent(m_Current+1);
			}
			if(m_PrevExampleInput.Triggered)
			{
				SetCurrent(m_Current-1);
			}
		}

		private void SetCurrent(int index)
		{
			m_Current = index;
			if(m_Current < 0)
			{
				m_Current = m_Examples.Length-1;
			}
			else if(m_Current > m_Examples.Length-1)
			{
				m_Current = 0;
			}

			if(m_Running != null)
			{
				m_Running.Exit();
			}
			m_Running = m_Examples[m_Current];
			m_Running.Setup();
		}

		public override void Tick()
		{
			base.Tick();
			m_Running.Tick();
		}
	}
}
