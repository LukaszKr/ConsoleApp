using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp.Import
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Coord
	{
		public short X;
		public short Y;

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
