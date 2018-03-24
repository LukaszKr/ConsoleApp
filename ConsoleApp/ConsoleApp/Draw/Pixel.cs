using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Pixel
	{
		public readonly UInt16 Value;
		public readonly UInt16 Attributes;

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
