namespace ProceduralLevel.ConsoleApp.Input
{
	public class CompositeDetector: AGroupDetector
	{
		protected override bool DefaultState { get { return false; } }

		public CompositeDetector(params AInputDetector[] detectors) : base(detectors)
		{
		}

		protected override bool IsTriggered(bool collectiveState, bool detectorState)
		{
			return collectiveState || detectorState;
		}
	}
}
