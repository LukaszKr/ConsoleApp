using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Pixel
	{
		public readonly char Value;
		public readonly EColor TextColor;
		public readonly EColor BGColor;

		public Pixel(char value, EColor textColor, EColor bgColor)
		{
			Value = value;
			TextColor = textColor;
			BGColor = bgColor;
		}

		public Pixel Overwrite(Pixel other)
		{
			char value;
			EColor textColor;
			EColor bgColor = (BGColor != EColor.Transparent? BGColor: other.BGColor);
			if(TextColor != EColor.Transparent)
			{
				value = Value;
				textColor = TextColor;
			}
			else
			{
				value = other.Value;
				textColor = other.TextColor;
			}
			return new Pixel(value, textColor, bgColor);
		}

		public override string ToString()
		{
			return string.Format("['{0}', TextColor: {1}, BGColor: {2}]", Value.ToString(), TextColor.ToString(), BGColor.ToString());
		}
	}
}
