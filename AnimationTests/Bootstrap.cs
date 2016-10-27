using System;

namespace SDSMTGDT.GWorks.AnimationTests
{
    /// <summary>
    /// Starts up the test
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //start the tests and dispose of the game after it is finished.
            using (var game = new AnimationTests())
                game.Run();
        }
    }
}
