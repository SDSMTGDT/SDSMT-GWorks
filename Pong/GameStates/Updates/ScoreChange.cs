using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pong.GameStates.Models;

namespace Pong.GameStates.Updates
{
    class ScoreChange : UpdateListener
    {
        //the scoreboard being updated
        Scoreboard scoreboard;

        public ScoreChange (Scoreboard scoreboard)
        {
            this.scoreboard = scoreboard;
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
