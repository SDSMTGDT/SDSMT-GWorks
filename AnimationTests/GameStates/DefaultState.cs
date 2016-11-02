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

        public override void OnAddState()
        {
            base.OnAddState();
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