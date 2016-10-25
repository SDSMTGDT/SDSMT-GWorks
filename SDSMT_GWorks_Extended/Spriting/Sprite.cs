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
        /// Returns the user-defined bounds for this sprite
        /// </summary>
        Rectangle bounds
        {
            get;
        }

        /// <summary>
        /// Returns the height and width of the underlying image
        /// </summary>
        Vector2 imageBounds
        {
            get;
        }

        void draw(SpriteBatch batch, Rectangle dest,
            Vector2? origin = default(Vector2?), float rotation = 0,
            Vector2? scale = default(Vector2?), Color? color = default(Color?),
            SpriteEffects effects = SpriteEffects.None, float layerDepth = 0);

        void update(GameTime dt);
    }
}
