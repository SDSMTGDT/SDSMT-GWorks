using SDSMTGDT.GWorks.GameStates;
using SDSMTGDT.GWorks.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BrickBreaker.GameStates.PlayStates.Normal.Model;

namespace BrickBreaker.GameStates.PlayStates.Normal.Updates
{
    //Pumps collisions because im lazy to do it any other way
    internal class CollisionChecker : UpdateListener
    {
        private CollisionManager collisions;
        private Ball ball;
        
        internal CollisionChecker(CollisionManager collisions, Ball ball)
        {
            this.collisions = collisions;
            this.ball = ball;
        }

        public void update(GameTime gameTime)
        {
            collisions.checkCollisions(ball);
        }
    }
}
