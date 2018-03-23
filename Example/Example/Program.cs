namespace ProceduralLevel.ConsoleApp.Example
{
	public class Program
	{

		static void Main(string[] args)
		{
			ConsoleHelper.SetWindowPosition(0, 0);
			AConsoleApp app;
			//app = new PerformanceExample();
			//app = new BasicExample();
			//app = new ColorExample();
			app = new InputExample();
			app.Run();
		}
	}
}
