namespace ProceduralLevel.ConsoleApp.Input
{
	public class MultipleDetector: AGroupDetector
	{
		protected override bool DefaultState { get { return false; } }

		public MultipleDetector(params AInputDetector[] detectors) : base(detectors)
		{
		}

		protected override bool IsTriggered(bool collectiveState, bool detectorState)
		{
			return collectiveState || detectorState;
		}
	}
}
