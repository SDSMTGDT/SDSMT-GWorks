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
            /*
            var curr = info.collider.speedPxPerMillis;
            curr.Y *= -1;
            info.collider.speedPxPerMillis = curr;
            */

            var collVector = -info.getCollisionVector(); // paddle to ball

            int paddleWidth = info.collided.getBounds().Width;
            float maxRotation = (float)(Math.PI / 4);

            float rotation = (-maxRotation + 
                ((collVector.X + paddleWidth / 2) / (paddleWidth + info.collider.getBounds().Width)) * 
                (2 * maxRotation));
            Console.WriteLine(rotation);

            info.collider.speedPxPerMillis = Vector2.Transform(-Vector2.UnitY * info.collider.speedPxPerMillis.Length(), Matrix.CreateRotationZ(rotation));

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
            Vector2 collVector = info.getCollisionVector();
            Vector2 current = info.collider.speedPxPerMillis;

            float angle1 = info.collided.upperRightAngle; // roughly -pi / 4
            float angle2 = (float)-(Math.PI + angle1);
            float angle3 = (float)(Math.PI + angle1);
            float angle4 = -angle1;

            float angle = (float)(Math.Atan2(collVector.Y, collVector.X));
            if (angle > angle4 && angle < angle3)
            {
                current.Y = -Math.Abs(current.Y);
            }
            else if (angle < angle1 && angle > angle2)
            {
                current.Y = Math.Abs(current.Y);
            }
            else if (angle > angle1 && angle < angle4)
            {
                current.X = -Math.Abs(current.X);
            }
            else
            {
                current.X = Math.Abs(current.X);
            }
            
            info.collider.speedPxPerMillis = current;

            while (info.collided.getBounds().Intersects(info.collider.getBounds()))
            {
                info.collider.move(current.X, current.Y);
            }

            //
        }
    }
}
