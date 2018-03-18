namespace ProceduralLevel.ConsoleApp
{
	//Font sizes are predefined in windows console
	public enum EFontSize
	{
		Size_05	= 5,
		Size_06	= 6,
		Size_07	= 7,
		Size_08	= 8,
		Size_10 = 10,
		Size_12	= 12,
		Size_14	= 14,
		Size_16	= 16,
		Size_18	= 18,
		Size_20	= 20,
		Size_24 = 24,
		Size_28 = 28,
		Size_36 = 36,
		Size_72 = 72,
	}

	public static class EFontSizeExt
	{
		public static Coord ToCoord(this EFontSize size)
		{
			return new Coord(0, (int)size);
		}
	}
}
