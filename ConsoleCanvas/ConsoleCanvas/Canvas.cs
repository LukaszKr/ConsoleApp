using System;
using System.Text;

namespace ProceduralLevel.ConsoleCanvas
{
	public class Canvas
	{
		public readonly Pixel[][] FrameBuffer;

		public readonly int Width;
		public readonly int Height;
		public readonly Painter Painter;

		public Canvas(int width, int height)
		{
			Width = width;
			Height = height;
			Painter = new Painter(this);

			FrameBuffer = new Pixel[width][];
			for(int x = 0; x < width; ++x)
			{
				FrameBuffer[x] = new Pixel[height];
				for(int y = 0; y < height; ++y)
				{
					FrameBuffer[x][y] = new Pixel(' ', Painter.TextColor, Painter.BGColor);
				}
			}
		}

		public void Render(int posX, int posY)
		{
			int maxWidth = Math.Min(Console.BufferWidth-posX, Width);
			int maxHeight = Math.Min(Console.BufferHeight-posY, Height);
			maxWidth = Math.Max(maxWidth, 0);
			maxHeight = Math.Max(maxHeight, 0);

			ConsoleColor textColor = 0;
			ConsoleColor bgColor = 0;

			StringBuilder builder = new StringBuilder(Width);

			int cx = posX;
			int cy = posY;
			for(int y = 0; y < maxHeight; y++)
			{
				for(int x = 0; x < maxWidth; x++)
				{
					Pixel pixel = FrameBuffer[x][y];
					if(pixel.TextColor != textColor || pixel.BGColor != bgColor)
					{
						textColor = pixel.TextColor;
						bgColor = pixel.BGColor;
						Console.SetCursorPosition(cx, cy);
						cx = posX+x;
						cy = posY+y;
						Console.Write(builder);
						builder.Clear();

						Console.ForegroundColor = textColor;
						Console.BackgroundColor = bgColor;
					}

					builder.Append(pixel.Value);
				}
			}

			Console.SetCursorPosition(cx, cy);
			Console.Write(builder);
			Console.SetCursorPosition(posX, posY);
		}

		public bool Plot(Pixel pixel, int posX, int posY)
		{
			if(posX >= 0 && posX < Width && posY >= 0 && posY < Height)
			{
				FrameBuffer[posX][posY] = pixel;
				return true;
			}
			return false;
		}

		public void Clear()
		{
			Clear(0, 0, Width, Height);
		}

		public void Clear(int posX, int posY, int width, int height)
		{
			for(int x = 0; x < width; x++)
			{
				for(int y = 0; y < height; y++)
				{
					Plot(new Pixel(' ', ConsoleColor.White, ConsoleColor.Black), posX+x, posY+y);
				}
			}
		}
	}
}
