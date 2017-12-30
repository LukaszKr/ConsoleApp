using System;

namespace ProceduralLevel.ConsoleApp
{
	[Flags]
	public enum EColor: byte
	{
		Black	= 0,
		Blue	= 1,
		Green	= 2,
		Red		= 4,
		Grey	= Blue | Green | Red,
		White	= Blue | Green | Red | Intense,
		Intense = 8
	}
}
