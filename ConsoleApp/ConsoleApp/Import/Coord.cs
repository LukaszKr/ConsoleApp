using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Coord
	{
		public Int16 X;
		public Int16 Y;

		public Coord(short x, short y)
		{
			X = x;
			Y = y;
		}

		public Coord(int x, int y)
		{
			X = (short)x;
			Y = (short)y;
		}

		public override string ToString()
		{
			return string.Format("[{0}, {1}]", X, Y);
		}
	}
}
