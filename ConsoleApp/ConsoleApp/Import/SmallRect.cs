using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct SmallRect
	{
		public Int16 Left;
		public Int16 Top;
		public Int16 Right;
		public Int16 Bottom;

		public SmallRect(short left, short top, short right, short bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public SmallRect(int left, int top, int right, int bottom)
		{
			Left = (short)left;
			Top = (short)top;
			Right = (short)right;
			Bottom = (short)bottom;
		}
	}
}
