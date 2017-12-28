using ProceduralLevel.ConsoleApp.Input;

namespace ProceduralLevel.ConsoleApp.Example
{
	public class InputManager: InputManager<EInputLayer>
	{
		protected override int IDToInt(EInputLayer id)
		{
			return (int)id;
		}
	}
}
