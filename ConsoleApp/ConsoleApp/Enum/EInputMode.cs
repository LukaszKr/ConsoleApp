using System;

namespace ProceduralLevel.ConsoleApp
{
	//https://docs.microsoft.com/en-us/windows/console/setconsolemode
	[Flags]
	public enum EInputMode: uint
	{
		ENABLE_PROCESSED_INPUT	= 0x0001,
		ENABLE_LINE_INPUT		= 0x0002,
		ENABLE_ECHO_INPUT		= 0x0004,
		ENABLE_WINDOW_INPUT		= 0x0008,
		ENABLE_MOUSE_INPUT		= 0x0010,
		ENABLE_INSERT_MODE		= 0x0020,
		ENABLE_QUICK_EDIT_MODE	= 0x0040,
		ENABLE_EXTENDED_FLAGS	= 0x0080,
		ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200,

		Default = ENABLE_MOUSE_INPUT | ENABLE_LINE_INPUT | ENABLE_EXTENDED_FLAGS //disable processed to prevent ctrl+a 
	}
}
