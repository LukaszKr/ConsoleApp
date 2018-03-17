using System;

namespace ProceduralLevel.ConsoleApp.Input
{
	public class KeyboardDevice: AInputDevice
	{
		private const int STATE_SIZE = 256;
		private const int KEY_BUFFER_SIZE = 8;

		private readonly static bool[] VALID_KEYS = new bool[STATE_SIZE];

		private int m_BufferHead = 0;
		private ConsoleKey[] m_KeyBuffer;
		private ConsoleModifiers m_Modifiers = 0;

		static KeyboardDevice()
		{
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
			VALID_KEYS[(int)ConsoleModifiers.Alt] = true;
			VALID_KEYS[(int)ConsoleModifiers.Shift] = true;
			VALID_KEYS[(int)ConsoleModifiers.Control] = true;
		}

		public KeyboardDevice()
			: base(STATE_SIZE, VALID_KEYS)
		{
			m_KeyBuffer = new ConsoleKey[KEY_BUFFER_SIZE];
		}

		protected override void OnUpdateState()
		{
			m_BufferHead = 0;
			m_Modifiers = 0;
			while(Console.KeyAvailable && m_BufferHead < KEY_BUFFER_SIZE)
			{
				ConsoleKeyInfo info = Console.ReadKey(true);
				m_KeyBuffer[m_BufferHead] = info.Key;
				m_Modifiers = m_Modifiers | info.Modifiers;
				m_BufferHead ++;
			}
			base.OnUpdateState();
		}

		protected override bool IsPressed(int codeValue)
		{
			if(codeValue >= 8)
			{
				ConsoleKey code = (ConsoleKey)codeValue;
				for(int x = 0; x < m_BufferHead; ++x)
				{
					if(m_KeyBuffer[x] == code)
					{
						return true;
					}
				}
				return false;
			}
			else
			{
				ConsoleModifiers modifier = (ConsoleModifiers)codeValue;
				return (m_Modifiers & modifier) != 0;
			}
		}

		public EButtonState Get(ConsoleKey code)
		{
			return m_KeyStates[(int)code];
		}

		public EButtonState Get(ConsoleModifiers modifier)
		{
			return m_KeyStates[(int)modifier];
		}
	}
}
