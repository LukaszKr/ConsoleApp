using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp.Import
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

		public FontInfo(EFontSize fontSize, EFontWeight weight, uint fontIndex = 0)
		{
			SizeInBytes = (uint)Marshal.SizeOf<FontInfo>();
			FontSize = new Coord(0, (int)fontSize);
			FontWeight = weight;
			FontIndex = fontIndex;
			FontFamily = 54; //seems to be always returned in getinto?
		}

		public void Init()
		{
			SizeInBytes = (uint)Marshal.SizeOf<FontInfo>();
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
