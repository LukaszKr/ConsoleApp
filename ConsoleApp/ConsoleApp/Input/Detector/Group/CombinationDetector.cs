namespace ProceduralLevel.ConsoleApp.Input
{
	public class CombinationDetector: AGroupDetector
	{
		protected override bool DefaultState { get { return true; } }

		public CombinationDetector(params AInputDetector[] detectors) : base(detectors)
		{
		}

		protected override bool IsTriggered(bool collectiveState, bool detectorState)
		{
			return collectiveState && detectorState;
		}
	}
}
