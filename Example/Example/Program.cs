namespace ProceduralLevel.ConsoleApp.Example
{
	public class Program
	{

		static void Main(string[] args)
		{
			ConsoleHelper.SetWindowPosition(0, 0);
			AConsoleApp app;
			//string name = info.GetName();

			ScreenBufferInfo info = ConsoleHelper.GetScreenBufferInfo();

			//app = new PerformanceExample();
			app = new BasicExample();
			//app = new ColorExample();
			app.Run();
		}
	}
}
