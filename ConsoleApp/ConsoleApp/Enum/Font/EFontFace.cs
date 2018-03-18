namespace ProceduralLevel.ConsoleApp
{
	public enum EFontFace
	{
		Consolas = 0,
		CourierNew = 1,
		LucidaConsole = 2,
		LucidaSansTypewriter = 3,
		MSGothic = 4,
		NSimSun = 5,
		SimSun_ExtB = 6
	}

	public static class EFontFaceExt
	{ 
		public const string CONSOLAS = "Consolas";
		public const string COURIER_NEW = "Courier New";
		public const string TERMINAL = "Terminal";
		public const string LUCIDA_CONSOLE = "Lucida Console";
		public const string LUCIDA_SANS_TYPEWRITER = "Lucida Sans Typewriter";
		public const string MS_GOTHIC = "MS Gothic";
		public const string N_SIM_SUN = "NSimSun";
		public const string SIM_SUN_EXTB = "SimSun-ExtB";

		public readonly static string[] Names = new string[]
		{
			CONSOLAS,
			COURIER_NEW,
			LUCIDA_CONSOLE,
			LUCIDA_SANS_TYPEWRITER,
			MS_GOTHIC,
			N_SIM_SUN,
			SIM_SUN_EXTB
		};

		public static string ToFaceName(this EFontFace face)
		{
			return Names[(int)face];
		}
	}

}
