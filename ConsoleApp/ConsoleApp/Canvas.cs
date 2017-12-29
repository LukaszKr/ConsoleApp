using System;
using System.Text;

namespace ProceduralLevel.ConsoleApp
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

			StringBuilder builder = new StringBuilder(Width);

			int cx = posX;
			int cy = posY;

			int brX = maxWidth+posX;
			int brY = maxHeight+posY;

			//flicker happens when you write to bottom right pixel
			//cursor moves to the right, which creates new row
			bool willFlicker = (brX == Console.BufferWidth && brY == Console.BufferHeight);

			ConsoleColor textColor = Console.ForegroundColor;
			ConsoleColor bgColor = Console.BackgroundColor;

			for(int y = 0; y < maxHeight; y++)
			{
				for(int x = 0; x < maxWidth; x++)
				{
					Pixel pixel = FrameBuffer[x][y];
					bool skipPixel = (willFlicker && x == maxWidth-1 && y == maxHeight-1);
					//skip last pixel as it's already there
					if(pixel.TextColor != textColor || pixel.BGColor != bgColor || skipPixel)
					{
						textColor = pixel.TextColor;
						bgColor = pixel.BGColor;
						if(builder.Length > 0)
						{
							Console.SetCursorPosition(cx, cy);
							cx = posX+x;
							cy = posY+y;
							Console.Write(builder.ToString());
							builder.Clear();
						}

						Console.ForegroundColor = textColor;
						Console.BackgroundColor = bgColor;
					}

					if(!skipPixel)
					{
						builder.Append(pixel.Value);
					}
				}
			}

			if(builder.Length > 0)
			{
				Console.SetCursorPosition(cx, cy);
				Console.Write(builder.ToString());
			}

			//handle bottom-right pixel
			if(willFlicker)
			{
				Pixel original = FrameBuffer[0][0];
				Pixel pixel = FrameBuffer[maxWidth-1][maxHeight-1];
				Console.SetCursorPosition(posX, posY);
				Console.ForegroundColor = pixel.TextColor;
				Console.BackgroundColor = pixel.BGColor;
				Console.Write(pixel.Value);
				//this will prevent cursor from moving to next the right causing flickering
				Console.MoveBufferArea(posX, posY, 1, 1, maxWidth-1, maxHeight-1,
					original.Value, original.TextColor, original.BGColor);
			}
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

		public bool Plot(Pixel pixel, int posX, int posY)
		{
			if(posX >= 0 && posX < Width && posY >= 0 && posY < Height)
			{
				FrameBuffer[posX][posY] = pixel;
				return true;
			}
			return false;
		}

		#region Draw
		public bool DrawChar(char chr, int posX, int posY)
		{
			return Plot(new Pixel(chr, TextColor, BGColor), posX, posY);
		}

		public void SetColor(ConsoleColor textColor, ConsoleColor bgColor)
		{
			TextColor = textColor;
			BGColor = bgColor;
		}

		public void DrawText(string text, int posX, int posY)
		{
			for(int x = 0; x < text.Length; x++)
			{
				DrawChar(text[x], posX+x, posY);
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


		//Bresenham line algorithm, adapted for c# from wikipedia
		//https://pl.wikipedia.org/wiki/Algorytm_Bresenhama#Implementacja
		public void DrawLine(char chr, int x1, int y1, int x2, int y2)
		{
			int d, dx, dy, ai, bi, xi, yi;
			int x = x1, y = y1;
			if(x1 < x2)
			{
				xi = 1;
				dx = x2 - x1;
			}
			else
			{
				xi = -1;
				dx = x1 - x2;
			}
			if(y1 < y2)
			{
				yi = 1;
				dy = y2 - y1;
			}
			else
			{
				yi = -1;
				dy = y1 - y2;
			}
			DrawChar(chr, x, y);
			if(dx > dy)
			{
				ai = (dy - dx) * 2;
				bi = dy * 2;
				d = bi - dx;
				while(x != x2)
				{
					if(d >= 0)
					{
						x += xi;
						y += yi;
						d += ai;
					}
					else
					{
						d += bi;
						x += xi;
					}
					DrawChar(chr, x, y);
				}
			}
			else
			{
				ai = (dx - dy) * 2;
				bi = dx * 2;
				d = bi - dy;
				while(y != y2)
				{
					if(d >= 0)
					{
						x += xi;
						y += yi;
						d += ai;
					}
					else
					{
						d += bi;
						y += yi;
					}
					DrawChar(chr, x, y);
				}
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

		public void DrawFrame(int posX, int posY, int width, int height, string horizontal, string vertical, char cross)
		{
			DrawGrid(posX, posY, 1, 1, width-1, height-1, horizontal, vertical, cross);
		}

		public void DrawGrid(int posX, int posY, int width, int height, int cellWidth, int cellHeight, string horizontal, string vertical, char cross)
		{
			for(int y = 0; y <= height; y++)
			{
				DrawRect(horizontal, posX, posY+y*cellHeight, width*cellWidth, 1);
			}

			for(int x = 0; x <= width; x++)
			{
				DrawRect(vertical, posX+x*cellWidth, posY, 1, height*cellHeight);
			}

			for(int x = 0; x <= width; x++)
			{
				for(int y = 0; y <= height; y++)
				{
					DrawChar(cross, posX+x*cellWidth, posY+y*cellHeight);
				}
			}
		}
		#endregion
	}
}
