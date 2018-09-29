namespace ProceduralLevel.ConsoleApp.Example
{
	public class Program
	{
		private const int EXAMPLE = 0;

		private static readonly AConsoleApp[] m_Examples = new AConsoleApp[]
		{
			new PerformanceExample(),
			new BasicExample(),
			new ColorExample(),
			new InputExample()
		};

		static void Main(string[] args)
		{
			AConsoleApp app = m_Examples[EXAMPLE];
			app.Run();
		}
	}
}
