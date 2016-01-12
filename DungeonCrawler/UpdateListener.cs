using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.DungeonCrawler
{
    public interface UpdateListener
    {
        void update(GameTime gameTime);
    }
}
