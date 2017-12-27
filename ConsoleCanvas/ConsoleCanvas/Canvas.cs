using System;
using System.Text;

namespace ProceduralLevel.ConsoleCanvas
{
	public class Canvas
	{
		public readonly Pixel[][] FrameBuffer;

		public readonly int Width;
		public readonly int Height;

		public ConsoleColor TextColor = ConsoleColor.White;
		public ConsoleColor BGColor = ConsoleColor.Black;

		public Canvas(int width, int height)
		{
			Width = width;
			Height = height;

			FrameBuffer = new Pixel[width][];
			for(int x = 0; x < width; ++x)
			{
				FrameBuffer[x] = new Pixel[height];
				for(int y = 0; y < height; ++y)
				{
					FrameBuffer[x][y] = new Pixel(' ', TextColor, BGColor);
				}
			}
		}

		public void Render(int posX, int posY)
		{
			int maxWidth = Math.Min(Console.BufferWidth-posX, Width);
			int maxHeight = Math.Min(Console.BufferHeight-posY, Height);
			maxWidth = Math.Max(maxWidth, 0);
			maxHeight = Math.Max(maxHeight, 0);

			ConsoleColor textColor = TextColor;
			ConsoleColor bgColor = BGColor;

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

		public bool DrawPixel(Pixel pixel, int posX, int posY)
		{
			if(posX >= 0 && posX < Width && posY >= 0 && posY < Height)
			{
				FrameBuffer[posX][posY] = pixel;
				return true;
			}
			return false;
		}

		public bool DrawChar(char chr, int posX, int posY)
		{
			return DrawPixel(new Pixel(chr, TextColor, BGColor), posX, posY);
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
					DrawPixel(new Pixel(' ', ConsoleColor.White, ConsoleColor.Black), posX+x, posY+y);
				}
			}
		}

		public void DrawText(string text, int posX, int posY, bool vCenter = false)
		{
			if(string.IsNullOrEmpty(text))
			{
				return;
			}
			int vOffset = (vCenter? text.Length/2: 0);

			for(int x = 0; x < text.Length; x++)
			{
				DrawChar(text[x], posX+x-vOffset, posY);
			}
		}

		public void DrawRect(string value, int posX, int posY, int width, int height)
		{
			for(int y = 0; y < height; y++)
			{
				for(int x = 0; x < width; x++)
				{
					DrawChar(value[x % value.Length], posX+x, posY+y);
				}
			}
		}

		public void DrawCanvas(Canvas canvas, int posX, int posY)
		{
			for(int x = 0; x < canvas.Width; x++)
			{
				for(int y = 0; y < canvas.Height; y++)
				{
					DrawChar(canvas.FrameBuffer[x][y].Value, posX+x, posY+y);
				}
			}
		}

		public void DrawFrame(int posX, int posY, int width, int height, string horizontal, string vertical, char cross)
		{
			DrawGrid(posX, posY, 1, 1, width-1, height-1, horizontal, vertical, cross);
		}

		public void DrawGrid(int posX, int posY, int width, int height, int cellX, int cellY, string horizontal, string vertical, char cross)
		{
			for(int y = 0; y <= height; y++)
			{
				DrawRect(horizontal, posX, posY+y*cellY, width*cellX, 1);
			}

			for(int x = 0; x <= width; x++)
			{
				DrawRect(vertical, posX+x*cellX, posY, 1, height*cellY);
			}

			for(int x = 0; x <= width; x++)
			{
				for(int y = 0; y <= height; y++)
				{
					DrawChar(cross, posX+x*cellX, posY+y*cellY);
				}
			}
		}
	}
}
