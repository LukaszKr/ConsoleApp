namespace ProceduralLevel.ConsoleApp.Input
{
	public enum EButtonState
    {
        Released = 0,
        JustPressed = 1,
        Pressed = 2,
        JustReleased = 3
    }

    public static class EKeyStateExt
    {
		public static bool IsDown(this EButtonState state)
		{
			return state == EButtonState.Pressed || state == EButtonState.JustPressed;
		}

		public static bool IsUp(this EButtonState state)
		{
			return state == EButtonState.Released || state == EButtonState.JustReleased;
		}

        public static EButtonState GetNextState(this EButtonState current, bool isPressed)
        {
            if(isPressed)
            {
                if (current == EButtonState.Released)
                {
                    return EButtonState.JustPressed;
                }
                return EButtonState.Pressed;
            }
            else if (current == EButtonState.Pressed)
            {
                return EButtonState.JustReleased;
            }
            return EButtonState.Released;
        }
    }
}
