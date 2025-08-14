using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal interface IMovementController
    {
        Vector2 GetDesiredVelocity(IMovable movable, GameTime gameTime);
    }
}
