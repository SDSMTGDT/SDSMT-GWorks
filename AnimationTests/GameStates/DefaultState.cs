using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using SDSMTGDT.GWorks.GameStates;
using SDSMTGDT.GWorks.Spriting;
/// <summary>
/// The DefaultState game state handles the general operations
/// of the test "game".
/// </summary>
namespace AnimationTests.GameStates
{
    public class DefaultState : MutableGameState
    {
        readonly List<Rectangle> locations;
        GameObject testObject;
        Animation walking;
        StaticSprite ship;
        internal DefaultState(GameStateManager manager, ContentManager Content) : base(manager)
        {
            //create the list of locations
            locations = new List<Rectangle>(16);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    locations.Insert(i + j, new Rectangle(i * 64, j * 64, 64, 64));
                }
            }
            walking = new Animation(locations, locations, Content.Load<Texture2D>("SmileyWalk"), new System.TimeSpan(1));
            ship = new StaticSprite(Content.Load<Texture2D>("shuttle"), new Rectangle(0, 0, 142, 220), new Rectangle(0, 0, 142, 220));
            testObject = new GameObject(walking, ship, 0, new Vector2(200, 200));
        }

        public override void OnAddState()
        {
            AddDrawListener(testObject);
            AddUpdateListener(testObject);
        }

        public override void OnRemoveState()
        {
            RemoveDrawListener(testObject);
            RemoveUpdateListener(testObject);
        }

        public override void Draw(GameTime gameTime, SpriteBatch graphics)
        {
            base.Draw(gameTime, graphics);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}