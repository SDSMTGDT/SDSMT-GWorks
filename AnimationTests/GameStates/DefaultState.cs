using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SDSMTGDT.GWorks.GameStates;
using SDSMTGDT.GWorks.Spriting;
/// <summary>
/// The DefaultState game state is supposed to handle the 
/// </summary>
namespace AnimationTests.GameStates
{
    public class DefaultState : MutableGameState
    {
        AnimationState[] animations;
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