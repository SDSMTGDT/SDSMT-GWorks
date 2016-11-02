using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.GameStates.Diagnostic
{
    internal class DiagnosticDisplay : DrawListener
    {
        private Vector2 position;
        private SpriteFont font;
        private Vector2 lineOffset;
        private Vector2 indent;


        internal DiagnosticDisplay(int x, int y, SpriteFont font)
        {
            this.position = new Vector2(x, y);
            this.font = font;
            this.lineOffset = new Vector2(0, font.LineSpacing);
            this.indent = new Vector2(100, 0);
        }

        public void Draw(GameTime gameTime, SpriteBatch graphics)
        {
            double FPS = 1000D / (gameTime.ElapsedGameTime.Milliseconds);
            Rectangle screenSize = graphics.GraphicsDevice.Viewport.Bounds;
            string diagnostic1 = "Gworks Test Game:";
            string diagnostic2 = "Window Size: (" + screenSize.Width + ", " + screenSize.Height + ")";
            string diagnostic3 = "FPS: " + FPS.ToString();
            graphics.DrawString(font, diagnostic1, position, Color.Black);
            graphics.DrawString(font, diagnostic2, position + lineOffset + indent, Color.Black);
            graphics.DrawString(font, diagnostic3, position + 2 * lineOffset + indent, Color.Black);
        }

        public int GetZIndex()
        {
            return 0;
        }
    }
}
