using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace SDSMTGDT.GWorks.GameStates
{
    public interface DrawListener
    {
        //Draws components to the screen
        void draw(GameTime gameTime, GraphicsDeviceManager graphics);
   
        //Returns the depth at which to draw, lower is closer to the screen
        int getZIndex();
    }
}
