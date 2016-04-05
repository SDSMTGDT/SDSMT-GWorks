using BrickBreaker.GameStates.PlayStates.Normal.Model;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Physics.Collisions.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker.GameStates.PlayStates.Normal.Events
{
    class Collisions
    {
        internal static void handleBallNorthWallCollision(TypedCollisionEventInfo<Ball, NorthWall> info)
        {
            var curr = info.collider.speedPxPerMillis;
            curr.Y *= -1;
            info.collider.speedPxPerMillis = curr;
            while (info.collided.getBounds().Intersects(info.collider.getBounds()))
            {
                info.collider.move(info.collider.speedPxPerMillis.X, info.collider.speedPxPerMillis.Y);
            }
        }

        internal static void handleBallEastWallCollision(TypedCollisionEventInfo<Ball, EastWall> info)
        {
            var curr = info.collider.speedPxPerMillis;
            curr.X *= -1;
            info.collider.speedPxPerMillis = curr;
            while (info.collided.getBounds().Intersects(info.collider.getBounds()))
            {
                info.collider.move(info.collider.speedPxPerMillis.X, info.collider.speedPxPerMillis.Y);
            }
        }

        internal static void handleBallWestWallCollision(TypedCollisionEventInfo<Ball, WestWall> info)
        {
            var curr = info.collider.speedPxPerMillis;
            curr.X *= -1;
            info.collider.speedPxPerMillis = curr;
            while (info.collided.getBounds().Intersects(info.collider.getBounds()))
            {
                info.collider.move(info.collider.speedPxPerMillis.X, info.collider.speedPxPerMillis.Y);

            }
        }

        internal static void handleBallSouthWallCollision(TypedCollisionEventInfo<Ball, SouthWall> info)
        {
            info.collider.starter.resetBallStarter();
        }


        internal static void handleBallPaddleCollision(TypedCollisionEventInfo<Ball, Paddle> info)
        {
            var curr = info.collider.speedPxPerMillis;
            curr.Y *= -1;
            info.collider.speedPxPerMillis = curr;
            while(info.collided.getBounds().Intersects(info.collider.getBounds()))
            {
                info.collider.move(info.collider.speedPxPerMillis.X, info.collider.speedPxPerMillis.Y);
            }
        }

        internal static void handleBallBrickCollision(TypedCollisionEventInfo<Ball, Brick> info)
        {
            info.collided.destroyed = true;
            info.physicsManager.eventManager.queueAction(() =>
            {
                info.physicsManager.unregisterCollidableFromSystem(info.collided);
            });
        }
    }
}
