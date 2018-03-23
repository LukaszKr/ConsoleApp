namespace ProceduralLevel.ConsoleApp.Input
{
	public class MouseDevice: AInputDevice
	{
		private int m_PosX;
		private int m_PosY;

		public int X { get { return m_PosX; } }
		public int Y { get { return m_PosY; } }

		public MouseDevice() : base(0, null)
		{
		}

		protected override bool IsPressed(int codeValue)
		{
			return false;
		}

		protected override void OnProcessRecord(InputRecord record)
		{
			MouseEventRecord mouseRecord = record.MouseEvent;
			m_PosX = mouseRecord.MousePosition.X;
			m_PosY = mouseRecord.MousePosition.Y;
		}
	}
}
