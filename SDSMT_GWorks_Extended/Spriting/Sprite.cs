using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Spriting
{
    public interface Sprite
    {
        /// <summary>
        /// Returns the user-defined Bounds for this sprite
        /// </summary>
        Rectangle Bounds
        {
            get;
        }

        /// <summary>
        /// Returns the height and width of the underlying image
        /// </summary>
        Vector2 ImageBounds
        {
            get;
        }

        void Draw(SpriteBatch batch, Rectangle dest,
            Vector2? origin = default(Vector2?), float rotation = 0,
            Vector2? scale = default(Vector2?), Color? color = default(Color?),
            SpriteEffects effects = SpriteEffects.None, float layerDepth = 0);

        void Update(GameTime dt);
    }
}
