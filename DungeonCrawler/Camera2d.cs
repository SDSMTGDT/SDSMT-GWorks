using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.DungeonCrawler
{
    /*
    *Credit to David Amador for the Camera2d class.
    *URL: http://www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/
    *Modifications By: Logan Lembke
    *Note: This camera centers on position.
    */
    public class Camera2d
    {
        //makes a camera with a zoom of 1, no rotation, centered on the origin.
        public Camera2d()
        {
            zoom = 1.0f;
            rotation = 0.0f;
            position = Vector2.Zero;
        }

        //Properties allow us to add functionality later if we need to
        //The camera centers around position
        public Vector2 position
        {
            get;
            set;
        }

        //Represents a linear transformation [scales, rotates]
        //Applys to every point drawn
        //Translates from game space to screen space
        private Matrix transformation
        {
            get;
            set;
        }

        //allows us to translate from the screen space to the game space
        private Matrix inverseTransformation
        {
            get { return Matrix.Invert(transformation); }
        }


        public float zoom
        {
            get;
            set;
        }

        //takes in an amount of radians and rotates the camera anti clockwise
        public float rotation
        {
            get;
            set;
        }

        //changes the position by the given amount.
        public void move(Vector2 amount)
        {
            position += amount;
        }

        //When a change is made,we need a new transformation matrix to
        //get the same result from the new starting values
        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            transformation =       
            Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                                         Matrix.CreateRotationZ(rotation) *
                                         Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                                         Matrix.CreateTranslation(
                                         new Vector3(graphicsDevice.Viewport.Width * 0.5f,
                                         graphicsDevice.Viewport.Height * 0.5f, 0)
            );
            return transformation;
        }

        //When we create a new transformation matrix, we need to create
        //a new inverse transformation to mate it
        public Matrix getInverseTranformation()
        {
            return inverseTransformation;
        }
    }
}
