using System;
using System.Threading;

namespace WinPad
{
#if WINDOWS || XBOX
	static class Program
	{

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			bool createdNew = true;
			using (Mutex mutex = new Mutex(true, "jristic.WinPad", out createdNew))
			{
				if (createdNew)
				{
					using (WinPad prog = new WinPad())
					{
						prog.Run();
					}
				}
			}
		}
	}
#endif
}

