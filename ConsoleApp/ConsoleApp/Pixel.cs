using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Pixel
	{
		[FieldOffset(0)] public readonly SByte Value;
		[FieldOffset(2)] public readonly UInt16 Attributes;

		public Pixel(SByte value, EColor textColor, EColor bgColor)
		{
			Value = (SByte)value;
			Attributes = (ushort)((ushort)textColor | ((ushort)bgColor << 4));
		}

		public Pixel(char value, EColor textColor, EColor bgColor)
		{
			Value = (SByte)value;
			Attributes = (ushort)((ushort)textColor | ((ushort)bgColor << 4));
		}

		public override string ToString()
		{
			return string.Format("['{0}', Attributes: {1}]", Value.ToString(), Attributes.ToString());
		}
	}
}
