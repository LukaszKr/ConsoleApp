using System;

namespace ProceduralLevel.ConsoleApp
{
	[Flags]
	public enum EColor: byte
	{
		//blue - 1, green - 2, red - 4, light - 8
		Black		= 0,
		DarkBlue	= 1,
		DarkGreen	= 2,
		DarkCyan	= 3,
		DarkRed		= 4,
		DarkMagenta = 5,
		DarkYellow	= 6,
		Gray		= 7,
		DarkGray	= 8,
		Blue		= 9,
		Green		= 10,
		Cyan		= 11,
		Red			= 12,
		Magenta		= 13,
		Yellow		= 14,
		White		= 15
	}
}
