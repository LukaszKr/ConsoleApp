using System;

namespace ProceduralLevel.ConsoleApp
{
	[Flags]
	public enum EInputEvent: ushort
	{
		KeyEvent = 0x0001,
		MouseEvent = 0x0002,
		WindowBufferSizeEvent = 0x0004
	}
}
