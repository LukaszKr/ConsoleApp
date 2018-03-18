using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ScreenBufferInfo
	{
		public Coord Size;
		public Coord CursorPosition;
		public ushort Attributes;
		public SmallRect Bounds;
		public Coord MaximumWindowSize;
	}
}
