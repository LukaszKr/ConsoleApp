using System;

namespace ProceduralLevel.ConsoleApp.Input
{
	public class DurationDetector: AInputDetector
	{
		public double Duration { get; private set; }


		public DurationDetector(AInputProvider inputProvider)
			: base(inputProvider)
		{
		}

		#region Shortcut Constructors
		public DurationDetector(ConsoleKey key)
			: this(new KeyboardKeyProvider(key))
		{

		}

		public DurationDetector(EInputModifier modifiers)
			: this(new KeyboardModifierProvider(modifiers))
		{

		}

		public DurationDetector(EMouseButton button)
			: this(new MouseButtonProvider(button))
		{

		}
		#endregion

		protected override bool OnUpdate(AInputManager inputManager)
		{
			Duration += inputManager.DeltaTime;
			return true;
		}

		protected override void OnReset(AInputManager inputManager)
		{
			Duration = 0f;
		}

		public override string ToString()
		{
			return base.ToString()+string.Format("[Duration: {0}]", Duration);
		}
	}
}
