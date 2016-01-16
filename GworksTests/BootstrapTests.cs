using System;

namespace SDSMTGDT.DungeonCrawler
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class BootstrapTests
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new TestGame())
                game.Run();
        }
    }
#endif
}
