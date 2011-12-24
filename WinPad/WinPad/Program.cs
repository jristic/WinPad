using System;

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
            using (WinPad prog = new WinPad())
            {
                prog.Run();
            }
        }
    }
#endif
}

