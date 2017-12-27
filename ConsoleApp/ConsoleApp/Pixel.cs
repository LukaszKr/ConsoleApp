using System;

namespace ProceduralLevel.ConsoleApp
{
	public struct Pixel
	{
		public readonly char Value;
		public readonly ConsoleColor TextColor;
		public readonly ConsoleColor BGColor;

		public Pixel(char value, ConsoleColor textColor, ConsoleColor bgColor)
		{
			Value = value;
			TextColor = textColor;
			BGColor = bgColor;
		}

		public override string ToString()
		{
			return string.Format("['{0}', TextColor: {1}, BGColor: {2}]", Value.ToString(), TextColor.ToString(), BGColor.ToString());
		}
	}
}
