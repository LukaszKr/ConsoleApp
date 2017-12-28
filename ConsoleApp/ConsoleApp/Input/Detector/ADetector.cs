namespace ProceduralLevel.ConsoleApp.Input
{
	public abstract class ADetector
	{
		public abstract bool Triggered { get; }

		public abstract void Update(AInputManager inputManager);

		public override string ToString()
		{
			return string.Format("[Triggered: {0}]", Triggered);
		}
	}
}
