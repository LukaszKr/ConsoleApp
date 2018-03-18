namespace ProceduralLevel.ConsoleApp
{
	public enum ETerminalFontSize
	{
		Size_4x6	= 0,
		Size_6x8	= 1,
		Size_8x8	= 2,
		Size_16x8	= 3,
		Size_5x12	= 4,
		Size_7x12	= 5,
		Size_8x12	= 6,
		Size_16x12	= 7,
		Size_12x16	= 8,
		Size_10x18	= 9
	}

	public static class ETerminalFontSizeExt
	{
		public readonly static Coord[] Sizes = new Coord[]
		{
			new Coord(4, 6),
			new Coord(6, 8),
			new Coord(8, 8),
			new Coord(16, 8),
			new Coord(5, 12),
			new Coord(7, 12),
			new Coord(8, 12),
			new Coord(16, 12),
			new Coord(12, 16),
			new Coord(10, 18)
		};

		public static Coord ToCoord(this ETerminalFontSize size)
		{
			return Sizes[(int)size];
		}
	}
}
