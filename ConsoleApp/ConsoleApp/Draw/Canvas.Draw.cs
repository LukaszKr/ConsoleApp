namespace ProceduralLevel.ConsoleApp
{
	public partial class Canvas
	{
		#region Draw
		public void DrawChar(char chr, int posX, int posY)
		{
			Plot(new Pixel(chr, TextColor, BGColor), posX, posY);
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
				Pixel[] column = canvas.FrameBuffer[x];
				for(int y = 0; y < canvas.Height; y++)
				{
					Plot(column[y], posX+x, posY+y);
				}
			}
		}

		//Bresenham line algorithm
		//https://pl.wikipedia.org/wiki/Algorytm_Bresenhama#Implementacja
		public void DrawLine(Pixel pixel, int x1, int y1, int x2, int y2)
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
			Plot(pixel, x, y);
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
					Plot(pixel, x, y);
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
					Plot(pixel, x, y);
				}
			}
		}

		public void DrawCircle(Pixel pixel, int centerX, int centerY, int radius)
		{
			DrawEllipse(pixel, centerX, centerY, radius, radius);
		}

		//Bresenham ellipse
		//https://sites.google.com/site/ruslancray/lab/projects/bresenhamscircleellipsedrawingalgorithm/bresenham-s-circle-ellipse-drawing-algorithm
		public void DrawEllipse(Pixel pixel, int centerX, int centerY, int width, int height)
		{
			int a2 = width*width;
			int b2 = height*height;
			int fa2 = 4*a2, fb2 = 4*b2;
			int x, y, sigma;

			/* first half */
			for(x = 0, y = height, sigma = 2*b2+a2*(1-2*height); b2*x <= a2*y; x++)
			{
				Plot(pixel, centerX+x, centerY+y);
				Plot(pixel, centerX-x, centerY+y);
				Plot(pixel, centerX+x, centerY-y);
				Plot(pixel, centerX-x, centerY-y);
				if(sigma >= 0)
				{
					sigma += fa2*(1-y);
					y--;
				}
				sigma += b2*((4*x)+6);
			}

			/* second half */
			for(x = width, y = 0, sigma = 2*a2+b2*(1-2*width); a2*y <= b2*x; y++)
			{
				Plot(pixel, centerX+x, centerY+y);
				Plot(pixel, centerX-x, centerY+y);
				Plot(pixel, centerX+x, centerY-y);
				Plot(pixel, centerX-x, centerY-y);
				if(sigma >= 0)
				{
					sigma += fb2*(1-x);
					x--;
				}
				sigma += a2*((4*y)+6);
			}
		}

		public void DrawRect(Pixel pixel, int posX, int posY, int width, int height)
		{
			for(int y = 0; y < height; y++)
			{
				for(int x = 0; x < width; x++)
				{
					Plot(pixel, posX+x, posY+y);
				}
			}
		}

		public void DrawFrame(int posX, int posY, int width, int height, Pixel horizontal, Pixel vertical, Pixel cross)
		{
			DrawGrid(posX, posY, 1, 1, width-1, height-1, horizontal, vertical, cross);
		}

		public void DrawGrid(int posX, int posY, int width, int height, int cellWidth, int cellHeight, Pixel horizontal, Pixel vertical, Pixel cross)
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
					Plot(cross, posX+x*cellWidth, posY+y*cellHeight);
				}
			}
		}
		#endregion
	}
}
