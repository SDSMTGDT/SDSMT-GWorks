using System;

namespace BrickBreaker
{
    /// <summary>
    /// This class kicks off the program.
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Using disposes the game after its done running
            using (var game = new BrickBreaker())
                game.Run();
        }
    }
}
