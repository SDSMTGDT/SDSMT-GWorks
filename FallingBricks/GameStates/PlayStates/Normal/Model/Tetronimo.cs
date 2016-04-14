using FallingBricks.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SDSMTGDT.GWorks.Physics;

namespace FallingBricks.GameStates.PlayStates.Normal.Model
{
    internal class Tetronimo : CollisionZone, Locatable, Movable
    {
        private List<BrickBrick> bricks;
        private Color color;

        internal Tetronimo(PhysicsManager physics, Color color, GraphicsDevice graphics, Point location, Vector2 velocity, List<BrickDiagram> bricks)
            : base(physics)
        {
            this.bricks = new List<BrickBrick>();
            this.color = color;
            this.bounds.Location = location;

            foreach (BrickDiagram diagram in bricks)
            {
                this.bricks.Add(
                    new BrickBrick(
                        location.X + diagram.xSlot * diagram.size,
                        location.Y + diagram.ySlot * diagram.size,
                        velocity,
                        diagram.size,
                        color,
                        graphics
                    )
                );
            }

            updateBoundsFromBricks();
        }

        public Point getLocation()
        {
            return bounds.Location;
        }

        //TODO:move all bricks
        public void move(GameTime gameTime)
        {
            //move all bricks
            //update bounds
        }

        //TODO:set velocity of brick bricks
        public void setVelocity(Vector2 velocity)
        {
        }

        //TODO:set velocity of brick bricks
        public void setVelocity(float x, float y)
        {
          
        }

        //Move around and update bricks
        //update bounds
        internal void rotate()
        {
        }

        private void updateBoundsFromBricks()
        {
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;

            foreach(BrickBrick brick in bricks)
            {
                if (brick.getLocation().X < minX)
                {
                    minX = brick.getLocation().X;
                }
                if (brick.getLocation().X + brick.getBounds().Width > maxX)
                {
                    maxX = brick.getLocation().X + brick.getBounds().Width;
                }
                if (brick.getLocation().Y < minY)
                {
                    minY = brick.getLocation().Y;
                }
                if (brick.getLocation().Y + brick.getBounds().Height > maxY)
                {
                    maxY = brick.getLocation().Y + brick.getBounds().Height;
                }
            }
            bounds.X = minX;
            bounds.Y = minY;
            bounds.Width = maxX - minX;
            bounds.Height = maxY - minY;
        }
    }
}
