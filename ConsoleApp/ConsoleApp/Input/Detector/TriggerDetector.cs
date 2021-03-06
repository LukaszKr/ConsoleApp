﻿using System;

namespace ProceduralLevel.ConsoleApp.Input
{
	public class TriggerDetector: AInputDetector
	{
		private bool m_Fired = false;

		public TriggerDetector(AInputProvider inputProvider) : base(inputProvider)
		{
		}

		#region Shortcut Constructors
		public TriggerDetector(ConsoleKey key)
			: this(new KeyboardKeyProvider(key))
		{

		}

		public TriggerDetector(EInputModifier modifiers)
			: this(new KeyboardModifierProvider(modifiers))
		{

		}

		public TriggerDetector(EMouseButton button)
			: this(new MouseButtonProvider(button))
		{

		}
		#endregion

		protected override bool OnUpdate(AInputManager inputManager)
		{
			if(!m_Fired)
			{
				m_Fired = true;
				return true;
			}
			return false;
		}

		protected override void OnReset(AInputManager inputManager)
		{
			m_Fired = false;
		}
	}
}
