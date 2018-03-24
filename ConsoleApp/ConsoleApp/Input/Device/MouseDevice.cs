namespace ProceduralLevel.ConsoleApp.Input
{
	public class MouseDevice: AInputDevice
	{
		private const uint FORWARD_SCROLL = 7864320; //magic numbers!
		private const uint BACKWARD_SCROLL = 4287102976;

		private int m_PosX;
		private int m_PosY;
		private int m_Scroll = 0;

		public int X { get { return m_PosX; } }
		public int Y { get { return m_PosY; } }
		public int Scroll { get { return m_Scroll; } }

		private uint m_ButtomState; 

		public MouseDevice() : base(3, null)
		{
		}

		protected override bool IsPressed(int codeValue)
		{
			return (m_ButtomState & (1 << codeValue)) != 0;
		}

		protected override void OnUpdateState()
		{
			m_Scroll = 0;
			base.OnUpdateState();
		}

		protected override void OnProcessRecord(InputRecord record)
		{
			MouseEventRecord mouseRecord = record.MouseEvent;
			m_PosX = mouseRecord.MousePosition.X;
			m_PosY = mouseRecord.MousePosition.Y;

			if((mouseRecord.EventFlags & 0x004) != 0) //vertical
			{
				if(mouseRecord.ButtonState == FORWARD_SCROLL)
				{
					m_Scroll = 1;
					m_IsActive = true;
				}
				else if(mouseRecord.ButtonState == BACKWARD_SCROLL)
				{
					m_Scroll = -1;
					m_IsActive = true;
				}
			}
			else
			{
				m_ButtomState = mouseRecord.ButtonState;
			}
		}

		public EButtonState Get(EMouseButton button)
		{
			return m_KeyStates[(int)button];
		}
	}
}
