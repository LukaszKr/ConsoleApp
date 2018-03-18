using System;

namespace ProceduralLevel.ConsoleApp
{
	//https://docs.microsoft.com/en-us/windows/console/setconsolemode
	[Flags]
	public enum EOutputMode: uint
	{
		ENABLE_PROCESSED_OUTPUT				= 0x0001,
		ENABLE_WRAP_AT_EOL_OUTPUT			= 0x0002,
		ENABLE_VIRTUAL_TERMINAL_PROCESSING	= 0x0004,
		DISABLE_NEWLINE_AUTO_RETURN			= 0x0008,
		ENABLE_LVB_GRID_WORLDWIDE			= 0x0010,

		Default = ENABLE_PROCESSED_OUTPUT
	}
}
