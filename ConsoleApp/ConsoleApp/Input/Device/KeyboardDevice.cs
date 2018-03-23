using System;
using System.Collections.Generic;

namespace ProceduralLevel.ConsoleApp.Input
{
	public class KeyboardDevice: AInputDevice
	{
		private const int STATE_SIZE = 256;
		private const int MODIFIER_SIZE = 8;

		private readonly static bool[] VALID_KEYS = new bool[STATE_SIZE];
		private readonly static EInputModifier[] MODIFIERS;

		private EButtonState[] m_Modifiers;

		private HashSet<int> m_Pressed = new HashSet<int>();

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
			m_Modifiers = new EButtonState[MODIFIERS.Length];
		}

		protected override void OnProcessRecord(InputRecord record)
		{
			KeyEventRecord keyRecord = record.KeyEvent;
			if(keyRecord.KeyDown)
			{
				m_Pressed.Add(keyRecord.VirtualKeyCode);
			}
			else
			{
				m_Pressed.Remove(keyRecord.VirtualKeyCode);
			}
			uint modifier = keyRecord.ControlKeyState;
			for(int x = 0; x < MODIFIERS.Length; ++x)
			{
				int offseted = 1 << (int)x;
				if((modifier & offseted) != 0)
				{
					if(keyRecord.KeyDown)
					{
						m_Pressed.Add(x);
					}
				}
				else
				{
					m_Pressed.Remove(x);
				}
			}
		}

		protected override bool IsPressed(int codeValue)
		{
			return m_Pressed.Contains(codeValue);
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
