using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp.Import
{
	[StructLayout(LayoutKind.Explicit)]
	public struct CharInfo
	{
		[FieldOffset(0)] public char Char;
		[FieldOffset(2)] public CharInfoAttribute Attributes;

		public override string ToString()
		{
			return string.Format("[{0}, {1}]", Char.ToString(), Attributes.ToString());
		}
	}
}
