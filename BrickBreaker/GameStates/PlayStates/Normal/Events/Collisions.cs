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
        internal static bool doSegmentsIntersect(Point a, Vector2 segment1, Point b, Vector2 segment2)
        {
            float cross1 = segment1.X * segment2.Y - segment1.Y * segment2.X;
            Vector2 distance = (b - a).ToVector2();
            float cross2 = distance.X * segment1.Y - distance.Y * segment1.X;

            if (cross1 == 0 && cross2 == 0) //colinear
                return true;

            Vector2 segment2OverCross1 = segment2 / cross1;
            Vector2 segment1OverCross1 = segment1 / cross1;
            float t = distance.X * segment2OverCross1.Y - distance.Y * segment2OverCross1.X;
            float u = distance.X * segment1OverCross1.Y - distance.Y * segment1OverCross1.X;
            if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
                return true;
            return false;
        }

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
            Vector2 ballBrickVector = info.getCollisionVector();
            Vector2 current = info.collider.speedPxPerMillis;
            
            if (doSegmentsIntersect(
                info.collider.getBounds().Center,
                ballBrickVector,
                new Point(info.collided.getBounds().X, info.collided.getBounds().Y + info.collided.getBounds().Height),
                new Vector2(info.collided.getBounds().Width, 0)
            ))
            {
                current.Y = Math.Abs(current.Y);
            }
            else if (doSegmentsIntersect(
                info.collider.getBounds().Center,
                ballBrickVector,
                new Point(info.collided.getBounds().X, info.collided.getBounds().Y),
                new Vector2(info.collided.getBounds().Width, 0)
            ))
            {
                current.Y = -Math.Abs(current.Y);
            }
            else if (doSegmentsIntersect(
                info.collider.getBounds().Center,
                ballBrickVector,
                new Point(info.collided.getBounds().X + info.collided.getBounds().Width, info.collided.getBounds().Y),
                new Vector2(0, info.collided.getBounds().Height)
            ))
            {
                current.X = Math.Abs(current.X);
            }
            else
            {
                current.X = -Math.Abs(current.X);
            }

            while (doSegmentsIntersect(
                info.collider.getBounds().Center,
                ballBrickVector,
                new Point(info.collided.getBounds().X, info.collided.getBounds().Y + info.collided.getBounds().Height),
                new Vector2(info.collided.getBounds().Width, 0)
            ))
            {
                info.collider.move(-info.collider.speedPxPerMillis.X, -info.collider.speedPxPerMillis.Y);
            }
            info.collider.speedPxPerMillis = current;
        }
    }
}
