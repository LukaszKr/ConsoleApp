namespace ProceduralLevel.ConsoleApp
{
	public partial class Canvas
	{
		public readonly Pixel[][] FrameBuffer;

		public readonly int Width;
		public readonly int Height;

		public EColor TextColor = EColor.White;
		public EColor BGColor = EColor.Black;

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



		public void Clear()
		{
			Clear(0, 0, Width, Height);
		}

		public void Clear(int posX, int posY, int width, int height)
		{
			Pixel pixel = new Pixel(' ', EColor.Transparent, EColor.Transparent);
			for(int x = 0; x < width; x++)
			{
				for(int y = 0; y < height; y++)
				{
					FrameBuffer[posX+x][posY+y] = pixel;
				}
			}
		}

		public bool Plot(Pixel pixel, int posX, int posY)
		{
			if(posX >= 0 && posX < Width && posY >= 0 && posY < Height)
			{
				Pixel[] column = FrameBuffer[posX];
				Pixel oldPixel = column[posY];
				column[posY] = pixel.Overwrite(oldPixel);
				return true;
			}
			return false;
		}

		public bool SetPixel(Pixel pixel, int posX, int posY)
		{
			if(posX >= 0 && posX < Width && posY >= 0 && posY < Height)
			{
				FrameBuffer[posX][posY] = pixel;
				return true;
			}
			return false;
		}

		public Pixel Read(int posX, int posY)
		{
			return FrameBuffer[posX][posY];
		}

		public void SetColor(EColor textColor, EColor bgColor)
		{
			TextColor = textColor;
			BGColor = bgColor;
		}
	}
}
