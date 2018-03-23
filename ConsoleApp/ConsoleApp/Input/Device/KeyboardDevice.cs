using System;

namespace ProceduralLevel.ConsoleApp.Input
{
	public class KeyboardDevice: AInputDevice
	{
		private const int STATE_SIZE = 256;
		private const int KEY_BUFFER_SIZE = 32;
		private const int MODIFIER_SIZE = 8;

		private readonly static bool[] VALID_KEYS = new bool[STATE_SIZE];
		private readonly static EInputModifier[] MODIFIERS;

		private int m_BufferHead = 0;
		private int[] m_KeyBuffer;
		private EButtonState[] m_Modifiers;

		static KeyboardDevice()
		{
			MODIFIERS = (EInputModifier[])Enum.GetValues(typeof(EInputModifier));
			ConsoleKey[] values = (ConsoleKey[])Enum.GetValues(typeof(ConsoleKey));
			for(int x = 0; x < values.Length; ++x)
			{
				int value = (int)values[x];
				if(value < STATE_SIZE)
				{
					VALID_KEYS[value] = true;
				}
			}
			//modifiers
			for(int x = 0; x < MODIFIERS.Length; ++x)
			{
				VALID_KEYS[x] = true;
			}
		}

		public KeyboardDevice()
			: base(STATE_SIZE, VALID_KEYS)
		{
			m_KeyBuffer = new int[KEY_BUFFER_SIZE];
			m_Modifiers = new EButtonState[MODIFIERS.Length];
		}

		protected override void OnProcessRecord(InputRecord record)
		{
			KeyEventRecord keyRecord = record.KeyEvent;
			m_KeyBuffer[m_BufferHead] = keyRecord.VirtualKeyCode;
			uint modifier = keyRecord.ControlKeyState;
			for(int x = 0; x < MODIFIERS.Length; ++x)
			{
				int offseted = 1 << (int)x;
				if((modifier & offseted) != 0)
				{
					m_KeyBuffer[m_BufferHead] = x;
				}
			}
			m_BufferHead++;
		}

		protected override void OnUpdateState()
		{
			base.OnUpdateState();
			m_BufferHead = 0;
		}

		protected override bool IsPressed(int codeValue)
		{
			for(int x = 0; x < m_BufferHead; ++x)
			{
				if(m_KeyBuffer[x] == codeValue)
				{
					return true;
				}
			}
			return false;

		}

		public EButtonState Get(ConsoleKey code)
		{
			return m_KeyStates[(int)code];
		}

		public EButtonState Get(EInputModifier code)
		{
			return m_KeyStates[(int)code];
		}
	}
}
