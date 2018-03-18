using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	[StructLayout(LayoutKind.Explicit)]
	public struct InputRecord
	{
		[FieldOffset(0)]
		public EInputEvent EventType;
		[FieldOffset(4)]
		public KeyEventRecord KeyEvent;
		[FieldOffset(4)]
		public MouseEventRecord MouseEvent;
		[FieldOffset(4)]
		public WindowBufferSizeRecord WindowBufferSizeEvent;
	};
}
