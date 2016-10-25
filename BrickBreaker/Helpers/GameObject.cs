using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker.Helpers
{
    //Very cruddy Game object
    interface GameObject
    {
        Texture2D texture { get; }
        Vector2 getLocation();
        void setLocation(float x, float y);
        void move(float x, float y);
    }
}
