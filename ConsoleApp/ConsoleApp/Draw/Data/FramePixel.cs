using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct FramePixel
	{
		public readonly ushort Value;
		public readonly ushort Attributes;

		public FramePixel(Pixel pixel)
			: this(pixel.Value, pixel.TextColor, pixel.BGColor)
		{

		}

		public FramePixel(char value, EColor textColor, EColor bgColor)
		{
			textColor = textColor.ToFrameBuffer();
			bgColor = bgColor.ToFrameBuffer();
			Value = value;
			Attributes = (ushort)((ushort)textColor | ((ushort)bgColor << 4));
		}

		public override string ToString()
		{
			return string.Format("['{0}', Attributes: {1}]", Value.ToString(), Attributes.ToString());
		}
	}
}
