using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.GWorks.GameStates
{
    public interface UpdateListener
    {
        void Update(GameTime gameTime);
    }
}
