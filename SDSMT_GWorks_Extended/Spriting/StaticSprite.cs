using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Spriting
{
    public class StaticSprite : Sprite
    {
        /// <summary>
        /// Source texture to pull from
        /// </summary>
        private Texture2D source;

        /// <summary>
        /// Location in the input texture to pull from. Nullable
        /// </summary>
        private Rectangle? location;

        /// <summary>
        /// Returns the height and width of the image underlying this sprite
        /// </summary>
        public Vector2 imageBounds { get; private set; }

        /// <summary>
        /// Returns the user-defined bounds of this sprite
        /// </summary>
        public Rectangle bounds { get; private set; }

        /// <summary>
        /// Create a new static sprite
        /// </summary>
        /// <param name="image">A texture to pull the sprite from</param>
        /// <param name="location">If the sprite is part of an atlas, define a source rect</param>
        /// <param name="bounds">Defines the bounds of the sprite if the bounds of the sprite do not match the texture</param>
        public StaticSprite(Texture2D image, Rectangle? location = null, Rectangle? bounds = null)
        {
            this.source = image;
            this.location = location;
            if (location.HasValue)
                this.imageBounds = location.Value.Size.ToVector2();
            else
                this.imageBounds = image.Bounds.Size.ToVector2();

            if (bounds.HasValue)
                this.bounds = bounds.Value;
            else
                this.bounds = image.Bounds;
        }

        public void draw(SpriteBatch batch, Rectangle dest,
            Vector2? origin = default(Vector2?), float rotation = 0,
            Vector2? scale = default(Vector2?), Color? color = default(Color?),
            SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
        {
            batch.Draw(source, null, dest, location.Value, origin, rotation, scale, color, effects, layerDepth);
        }

        /// <summary>
        /// Empty update for static sprite
        /// </summary>
        /// <param name="dt">Delta time</param>
        public void update(GameTime dt) { }
    }
}
