using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public unsafe struct FontInfo
	{
		public uint SizeInBytes;
		public uint FontIndex;
		public Coord FontSize;
		public uint FontFamily;
		public EFontWeight FontWeight; //100-1000, multiplies of 100
		public fixed char FaceName[32];

		public FontInfo(ETerminalFontSize size, EFontWeight weight = EFontWeight.Normal)
		{
			SizeInBytes = (uint)Marshal.SizeOf<FontInfo>();
			FontSize = size.ToCoord();
			FontWeight = weight;
			FontIndex = 0;
			FontFamily = 48; //magic numbers, if set to 54 font size width is ignored
			SetFace(EFontFaceExt.TERMINAL);
		}

		public FontInfo(EFontFace fontFace, EFontSize fontSize, EFontWeight weight = EFontWeight.Normal)
		{
			SizeInBytes = (uint)Marshal.SizeOf<FontInfo>();
			FontSize = new Coord(0, (int)fontSize);
			FontWeight = weight;
			FontIndex = 0;
			FontFamily = 54; //type face font
			SetFace(fontFace);
		}

		public void Init()
		{
			SizeInBytes = (uint)Marshal.SizeOf<FontInfo>();
		}

		public void SetSize(EFontSize size)
		{
			FontFamily = 54;
			FontSize = size.ToCoord();
		}

		public void SetSize(ETerminalFontSize size)
		{
			FontFamily = 48;
			FontSize = size.ToCoord();
		}

		public void SetFace(EFontFace face)
		{
			SetFace(face.ToFaceName());
		}

		public unsafe void SetFace(string fontName)
		{
			fixed (char* name = FaceName)
			{
				for(int x = 0; x < fontName.Length; ++x)
				{
					name[x] = fontName[x];
				}
				for(int x = fontName.Length; x < 32; ++x)
				{
					name[x] = default(char);
				}
			}
		}

		public unsafe string GetName()
		{
			fixed(char* name = FaceName)
			{
				return new string(name);
			}
		}
	}
}
