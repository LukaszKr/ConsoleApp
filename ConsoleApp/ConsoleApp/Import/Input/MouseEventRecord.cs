using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	[StructLayout(LayoutKind.Explicit)]
	public struct MouseEventRecord
	{
		[FieldOffset(0)]
		public Coord MousePosition;
		[FieldOffset(4)]
		public uint ButtonState;
		[FieldOffset(8)]
		public uint ControlKeyState;
		[FieldOffset(12)]
		public uint EventFlags;
	}
}
