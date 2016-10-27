using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        internal DefaultState(GameStateManager manager) : base(manager)
        {

        }

        public override void onAddState()
        {
            base.onAddState();
        }

        public override void draw(GameTime gameTime, SpriteBatch graphics)
        {
            base.draw(gameTime, graphics);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }
    }
}