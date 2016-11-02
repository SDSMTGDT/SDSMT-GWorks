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
            var curr = info.Collider.speedPxPerMillis;
            curr.Y *= -1;
            info.Collider.speedPxPerMillis = curr;
            while (info.Collided.GetBounds().Intersects(info.Collider.GetBounds()))
            {
                info.Collider.move(info.Collider.speedPxPerMillis.X, info.Collider.speedPxPerMillis.Y);
            }
        }

        internal static void handleBallEastWallCollision(TypedCollisionEventInfo<Ball, EastWall> info)
        {
            var curr = info.Collider.speedPxPerMillis;
            curr.X *= -1;
            info.Collider.speedPxPerMillis = curr;
            while (info.Collided.GetBounds().Intersects(info.Collider.GetBounds()))
            {
                info.Collider.move(info.Collider.speedPxPerMillis.X, info.Collider.speedPxPerMillis.Y);
            }
        }

        internal static void handleBallWestWallCollision(TypedCollisionEventInfo<Ball, WestWall> info)
        {
            var curr = info.Collider.speedPxPerMillis;
            curr.X *= -1;
            info.Collider.speedPxPerMillis = curr;
            while (info.Collided.GetBounds().Intersects(info.Collider.GetBounds()))
            {
                info.Collider.move(info.Collider.speedPxPerMillis.X, info.Collider.speedPxPerMillis.Y);

            }
        }

        internal static void handleBallSouthWallCollision(TypedCollisionEventInfo<Ball, SouthWall> info)
        {
            info.Collider.starter.resetBallStarter();
        }


        internal static void handleBallPaddleCollision(TypedCollisionEventInfo<Ball, Paddle> info)
        {
            /*
            var curr = info.collider.speedPxPerMillis;
            curr.Y *= -1;
            info.collider.speedPxPerMillis = curr;
            */

            var collVector = -info.GetCollisionVector(); // paddle to ball

            int paddleWidth = info.Collided.GetBounds().Width;
            float maxRotation = (float)(Math.PI / 4);

            float rotation = (-maxRotation + 
                ((collVector.X + paddleWidth / 2) / (paddleWidth + info.Collider.GetBounds().Width)) * 
                (2 * maxRotation));

            info.Collider.speedPxPerMillis = Vector2.Transform(-Vector2.UnitY * info.Collider.speedPxPerMillis.Length(), Matrix.CreateRotationZ(rotation));

            while(info.Collided.GetBounds().Intersects(info.Collider.GetBounds()))
            {
                info.Collider.move(info.Collider.speedPxPerMillis.X, info.Collider.speedPxPerMillis.Y);
            }
        }

        internal static void handleBallBrickCollision(TypedCollisionEventInfo<Ball, Brick> info)
        {
            info.Collided.destroyed = true;
            info.PhysicsManager.EventManager.QueueAction(() =>
            {
                info.PhysicsManager.UnregisterCollidable(info.Collided);
            });
            Vector2 ballBrickVector = info.GetCollisionVector();
            Vector2 current = info.Collider.speedPxPerMillis;
            
            if (doSegmentsIntersect(
                info.Collider.GetBounds().Center,
                ballBrickVector,
                new Point(info.Collided.GetBounds().X, info.Collided.GetBounds().Y + info.Collided.GetBounds().Height),
                new Vector2(info.Collided.GetBounds().Width, 0)
            ))
            {
                current.Y = Math.Abs(current.Y);
            }
            else if (doSegmentsIntersect(
                info.Collider.GetBounds().Center,
                ballBrickVector,
                new Point(info.Collided.GetBounds().X, info.Collided.GetBounds().Y),
                new Vector2(info.Collided.GetBounds().Width, 0)
            ))
            {
                current.Y = -Math.Abs(current.Y);
            }
            else if (doSegmentsIntersect(
                info.Collider.GetBounds().Center,
                ballBrickVector,
                new Point(info.Collided.GetBounds().X + info.Collided.GetBounds().Width, info.Collided.GetBounds().Y),
                new Vector2(0, info.Collided.GetBounds().Height)
            ))
            {
                current.X = Math.Abs(current.X);
            }
            else
            {
                current.X = -Math.Abs(current.X);
            }

            info.Collider.speedPxPerMillis = current;
            while (info.Collider.GetBounds().Intersects(info.Collided.GetBounds()))
            {
                info.Collider.move(info.Collider.speedPxPerMillis.X, info.Collider.speedPxPerMillis.Y);
            }
        }
    }
}
