using SDSMTGDT.GWorks.Spriting;
using Microsoft.Xna.Framework;

namespace AnimationTests
{
    /// <summary>
    /// Game object has the immediate use of 
    /// </summary>
    public class GameObject
    {
        //constants that define how fast the object can move.
        const float MOVE_AMOUNT = 0.1f;
        const float ROTATE_AMOUNT = 0.01f;
        Animation walking;
        AnimationState walkState;
        StaticSprite spaceShip;
        Sprite currentGraphic;
        Vector2 position;
        float angle;
        GameObject(Animation walking, StaticSprite spaceShip, float angle = 0, Vector2? position = default(Vector2?))
        {
            this.walking = walking;
            this.walkState = new AnimationState(walking, false);
            //If this works as intended, current graphic will be a reference
            //to the walkState or spaceShip references as needed.
            currentGraphic = walkState;
            this.spaceShip = spaceShip;
            this.angle = angle;
            //if a position is provided use it, otherwise start at the indicated
            //position.
            this.position = position??new Vector2(0,0);
        }

        /// <summary>
        /// When this function is called it advances to the next graphic on the
        /// GameObject. This is also where unregistering any events will happen
        /// to prevent leaving events, or even multiple events, in the manager.
        /// </summary>
        private void changeGraphics()
        {
            if (currentGraphic == walkState)
            {
                //TODO:"Dispose" of the events
                currentGraphic = spaceShip;
            }
            else
            {
                //TODO:"Dispose" of the events
                currentGraphic = walkState;
            }
        }
    }
}