namespace ProceduralLevel.ConsoleApp.Input
{
	public class MouseDevice: AInputDevice
	{
		private int m_PosX;
		private int m_PosY;

		public int X { get { return m_PosX; } }
		public int Y { get { return m_PosY; } }

		private uint m_ButtomState; 

		public MouseDevice() : base(3, null)
		{
		}

		protected override bool IsPressed(int codeValue)
		{
			return (m_ButtomState & (1 << codeValue)) != 0;
		}

		protected override void OnProcessRecord(InputRecord record)
		{
			MouseEventRecord mouseRecord = record.MouseEvent;
			m_PosX = mouseRecord.MousePosition.X;
			m_PosY = mouseRecord.MousePosition.Y;

			m_ButtomState = mouseRecord.ButtonState;
		}

		public EButtonState Get(EMouseButton button)
		{
			return m_KeyStates[(int)button];
		}
	}
}
