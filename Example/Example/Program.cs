using System;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class Program
	{
		private const int EXAMPLE = 3;

		private static readonly Func<AConsoleApp>[] m_Examples = new Func<AConsoleApp>[]
		{
			() => new PerformanceExample(),
			() => new BasicExample(),
			() => new ColorExample(),
			() => new InputExample()
		};

		static void Main(string[] args)
		{
			AConsoleApp app = m_Examples[EXAMPLE]();
			app.Run();
		}
	}
}
