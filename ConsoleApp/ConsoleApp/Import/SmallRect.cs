using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp.Import
{
	[StructLayout(LayoutKind.Sequential)]
	public struct SmallRect
	{
		public short Left;
		public short Top;
		public short Right;
		public short Bottom;

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
