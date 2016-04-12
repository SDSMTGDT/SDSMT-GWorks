using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pong.GameStates.Models;

namespace Pong.GameStates.Updates
{
    class BallStarter : UpdateListener
    {
        //ball which will begin moving
        private Ball ball;

        //viewport
        private Rectangle screen;

        //playstate to alter
        private MutableGameState playState;

        public void update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
