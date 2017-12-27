using System;

namespace ProceduralLevel.ConsoleCanvas
{
	public class Painter
	{
		public ConsoleColor TextColor = ConsoleColor.White;
		public ConsoleColor BGColor = ConsoleColor.Black;
		public Canvas Canvas;

		public Painter(Canvas canvas)
		{
			Canvas = canvas;
		}

		public bool DrawChar(char chr, int posX, int posY)
		{
			return Canvas.Plot(new Pixel(chr, TextColor, BGColor), posX, posY);
		}

		public void DrawText(string text, int posX, int posY, bool vCenter = false)
		{
			if(string.IsNullOrEmpty(text))
			{
				return;
			}
			int vOffset = (vCenter ? text.Length/2 : 0);

			for(int x = 0; x < text.Length; x++)
			{
				DrawChar(text[x], posX+x-vOffset, posY);
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
