using SDSMTGDT.GWorks.Spriting;
using SDSMTGDT.GWorks.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AnimationTests
{
    /// <summary>
    /// Game object manages the information for the test's gameObject,
    /// it implements DrawListener and UpdateListener to minimize the number
    /// of files for the test.
    /// </summary>
    public class GameObject : DrawListener, UpdateListener
    {
        //constants that define how fast the object can move.
        const float MOVE_AMOUNT = 0.1f;
        const float ROTATE_AMOUNT = 0.01f;
        Animation walking;
        AnimationState walkState;
        StaticSprite spaceShip;
        Sprite currentGraphic;
        Vector2 position;
        Vector2 origin;
        float angle;
        /// <summary>
        /// The constructor for the test game's gameObject
        /// </summary>
        /// <param name="walking">animation to test</param>
        /// <param name="spaceShip">static image to test</param>
        /// <param name="angle">rotation of the image</param>
        /// <param name="position"> position on the screen of the image</param>
        internal GameObject(Animation walking, StaticSprite spaceShip, float angle = 0, Vector2? position = default(Vector2?))
        {
            this.walking = walking;
            this.walkState = new AnimationState(walking, true);
            //If this works as intended, current graphic will be a reference
            //to the walkState or spaceShip references as needed.
            currentGraphic = walkState;
            this.spaceShip = spaceShip;
            this.angle = angle;
            //if a position is provided use it, otherwise start at the indicated
            //position.
            this.position = position??new Vector2(0,0);
            this.origin = currentGraphic.ImageBounds / 2;
        }

        /// <summary>
        /// When this function is called it advances to the next graphic on the
        /// GameObject. As GameObject implements DrawListener and UpdateListener
        /// listener disposal is handled in the game state.
        /// </summary>
        private void changeGraphics()
        {
            if (currentGraphic == walkState)
            {
                currentGraphic = spaceShip;
                origin = currentGraphic.ImageBounds / 2;
            }
            else
            {
                currentGraphic = walkState;
                origin = currentGraphic.ImageBounds / 2;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch graphics)
        {
            Rectangle destination = new Rectangle(position.ToPoint(),(currentGraphic.ImageBounds.ToPoint()));
            currentGraphic.Draw(graphics, destination, origin, angle, null, null, SpriteEffects.None, 0);
        }

        public int GetZIndex()
        {
            return 0;
        }

        public void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position -= MOVE_AMOUNT * Vector2.UnitY * gameTime.ElapsedGameTime.Milliseconds;
            }
            else if ( Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position += MOVE_AMOUNT * Vector2.UnitY * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position += MOVE_AMOUNT * Vector2.UnitX * gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position -= MOVE_AMOUNT * Vector2.UnitX * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.OemComma))
            {
                angle -= ROTATE_AMOUNT * gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.OemPeriod))
            {
                angle += ROTATE_AMOUNT * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                changeGraphics();
            }

            currentGraphic.Update(gameTime);
        }
    }
}