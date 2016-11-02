using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.Spriting
{
    public class StaticSprite : Sprite
    {
        /// <summary>
        /// Source texture to pull from
        /// </summary>
        private readonly Texture2D source;

        /// <summary>
        /// Location in the input texture to pull from. Nullable
        /// </summary>
        private Rectangle location;

        /// <summary>
        /// Returns the height and width of the image underlying this sprite
        /// </summary>
        public Vector2 ImageBounds { get; }

        /// <summary>
        /// Returns the user-defined Bounds of this sprite
        /// </summary>
        public Rectangle Bounds { get; }

        /// <summary>
        /// Create a new static sprite
        /// </summary>
        /// <param name="image">A texture to pull the sprite from</param>
        /// <param name="location">If the sprite is part of an atlas, define a source rect</param>
        /// <param name="bounds">Defines the Bounds of the sprite if the Bounds of the sprite do not match the texture</param>
        public StaticSprite(Texture2D image, Rectangle? location = null, Rectangle? bounds = null)
        {
            this.location = location ?? image.Bounds;
            source = image;
            ImageBounds = location?.Size.ToVector2() ?? image.Bounds.Size.ToVector2();
            Bounds = bounds ?? image.Bounds;
        }

        public void Draw(SpriteBatch batch, Rectangle dest,
            Vector2? origin = default(Vector2?), float rotation = 0,
            Vector2? scale = default(Vector2?), Color? color = default(Color?),
            SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
        {
            batch.Draw(source, null, dest, location, origin, rotation, scale, color, effects, layerDepth);
        }

        /// <summary>
        /// Empty update for static sprite
        /// </summary>
        /// <param name="dt">Delta time</param>
        public void Update(GameTime dt) { }
    }
}
