using System;
using System.Runtime.InteropServices;

namespace ProceduralLevel.ConsoleApp
{
	public static partial class ConsoleHelper
	{
		public static uint ReadAvail()
		{
			uint avail = 0;
			CheckError(GetNumberOfConsoleInputEvents(m_StdInputHandle, ref avail));
			return avail;
		}

		public static void FlushInput()
		{
			CheckError(FlushConsoleInputBufferW(m_StdInputHandle));
		}

		public static uint PeekInput(InputRecord[] buffer, EInputEvent filter = EInputEvent.KeyEvent | EInputEvent.MouseEvent)
		{
			uint read = 0;
			CheckError(PeekConsoleInputW(m_StdInputHandle, buffer, (uint)buffer.Length, ref read));
			return FilterInputs(read, buffer);
		}

		public static uint ReadInput(InputRecord[] buffer, EInputEvent filter = EInputEvent.KeyEvent | EInputEvent.MouseEvent)
		{
			uint read = 0;
			CheckError(ReadConsoleInputW(m_StdInputHandle, buffer, (uint)buffer.Length, ref read));
			return FilterInputs(read, buffer);
		}

		public static uint FilterInputs(uint count, InputRecord[] buffer, EInputEvent filter = EInputEvent.KeyEvent | EInputEvent.MouseEvent)
		{ 
			uint newCount = count;
			if(count > 0)
			{
				for(int x = (int)count-1; x >= 0; --x)
				{
					InputRecord record = buffer[x];
					if((filter & record.EventType) == 0)
					{
						//(uint)record.EventType == 0x0004 //buffer resize
						--newCount;
						for(int y = x; y < count-1; ++y)
						{
							buffer[y] = buffer[y+1];
						}
						buffer[count-1] = default(InputRecord);
					}
				}
			}
			return newCount;
		}

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool GetNumberOfConsoleInputEvents([In] IntPtr consoleInput, [In, Out] ref uint numberOfEvents);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern bool FlushConsoleInputBufferW([In] IntPtr consoleInput);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern bool PeekConsoleInputW([In] IntPtr consoleInput, [Out] InputRecord[] record, [In] uint arrayLength, [In, Out] ref uint numberRead);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern bool ReadConsoleInputW([In] IntPtr consoleInput, [Out] InputRecord[] record, [In] uint arrayLength, [In, Out] ref uint numberRead);
	}
}
