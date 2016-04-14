using FallingBricks.GameStates.PlayStates.Normal.Model;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingBricks.GameStates.PlayStates.Normal
{
    internal class NormalPlayState : MutableGameState
    {
        internal int bricksWide { get; private set; }
        internal int bricksHigh { get; private set; }
        internal int brickSize { get; private set; }
        private BrickBrickGraveyard graveyard {get; set;}

        public NormalPlayState(GameStateManager manager) : base(manager)
        {
            var screenWidth = manager.settings.access(manager.settings.engineSettings.WINDOW_WIDTH);
            var screenHeight = manager.settings.access(manager.settings.engineSettings.WINDOW_HEIGHT);

            bricksWide = 10;
            brickSize = screenWidth / bricksWide;
            bricksHigh = screenHeight / brickSize;
        }
    }
}
