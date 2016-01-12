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
        public Camera2d()
        {
            zoom = 1.0f;
            rotation = 0.0f;
            position = Vector2.Zero;
        }

        public Vector2 position
        {
            get;
            set;
        }

        private Matrix transformation
        {
            get;
            set;
        }

        private Matrix inverseTransformation
        {
            get { return Matrix.Invert(transformation); }
        }

        public float zoom
        {
            get;
            set;
        }

        public float rotation
        {
            get;
            set;
        }

        public void move(Vector2 amount)
        {
            position += amount;
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            transformation =       
            Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                                         Matrix.CreateRotationZ(rotation) *
                                         Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f,
                                         graphicsDevice.Viewport.Height * 0.5f, 0)
            );
            return transformation;
        }
    }
}
