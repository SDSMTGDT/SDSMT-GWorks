using System;

namespace FallingBricks
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new FallingBricks())
                game.Run();
        }
    }
}
