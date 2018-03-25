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

		public override string ToString()
		{
			string str = "";
			switch(EventType)
			{
				case EInputEvent.KeyEvent:
					str = KeyEvent.ToString();
					break;
				case EInputEvent.MouseEvent:
					str = MouseEvent.ToString();
					break;
			}
			return string.Format("[{0}] {1}", EventType.ToString(), str);
		}
	};
}
