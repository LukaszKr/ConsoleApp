using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Pixel
	{
		[FieldOffset(0)] public readonly char Value;
		[FieldOffset(2)] public readonly ushort Attributes;

		public Pixel(char value, EColor textColor, EColor bgColor)
		{
			Value = value;
			Attributes = (ushort)((ushort)textColor | ((ushort)bgColor << 4));
		}

		public override string ToString()
		{
			return string.Format("['{0}', Attributes: {1}]", Value.ToString(), Attributes.ToString());
		}
	}
}
