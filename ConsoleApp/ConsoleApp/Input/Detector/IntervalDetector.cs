using System;

namespace ProceduralLevel.ConsoleApp.Input
{
	public class IntervalDetector: DurationDetector
	{
		private double[] m_Intervals;
		private double m_PreviousTrigger;
		private double m_NextTrigger;
		private int m_IntervalIndex;

		public double CurrentInterval { get; private set; }
		public double Count { get; private set; }
		public double Progress 
		{ 
			get 
			{
				double diff = m_NextTrigger-m_PreviousTrigger;
				if(diff > 0)
				{
					return (Duration-m_PreviousTrigger)/diff;
				}
				return 0f;
			}
		}

		public IntervalDetector(AInputProvider inputProvider, params double[] intervals)
			: base(inputProvider)
		{
			m_Intervals = new double[intervals.Length];
			for(int x = 0; x < m_Intervals.Length; ++x)
			{
				m_Intervals[x] = intervals[x];
			}
		}

		#region Shortcut Constructors
		public IntervalDetector(ConsoleKey key, params double[] intervals)
			: this(new KeyboardKeyProvider(key), intervals)
		{

		}

		public IntervalDetector(EInputModifier modifiers, params double[] intervals)
			: this(new KeyboardModifierProvider(modifiers), intervals)
		{

		}

		public IntervalDetector(EMouseButton button, params double[] intervals)
			: this(new MouseButtonProvider(button), intervals)
		{

		}
		#endregion

		protected override bool OnUpdate(AInputManager inputManager)
		{
			base.OnUpdate(inputManager);

			if(Duration >= m_NextTrigger)
			{
				UpdateInterval();
				return true;
			}
			return false;
		}

		protected override void OnReset(AInputManager inputManager)
		{
			base.OnReset(inputManager);

			m_PreviousTrigger = 0f;
			m_NextTrigger = m_Intervals[0];
			CurrentInterval = m_Intervals[0];
			m_IntervalIndex = 0;
			Count = 0;
		}

		private void UpdateInterval()
		{
			if(m_IntervalIndex < m_Intervals.Length-1)
			{
				m_IntervalIndex ++;
				CurrentInterval = m_Intervals[m_IntervalIndex];
			}
			m_PreviousTrigger = m_NextTrigger;
			m_NextTrigger += m_Intervals[m_IntervalIndex];
			Count ++;
		}

		public override string ToString()
		{
			string intervals = "";
			for(int x = 0; x < m_Intervals.Length; ++x)
			{
				if(x > 0)
				{
					intervals += ",";
				}
				intervals += m_Intervals[x].ToString();
			}

			return base.ToString()+string.Format("[CurrentInterval: {0}, Count: {1}, Intervals: {2}]", CurrentInterval, Count, intervals);
		}
	}
}
