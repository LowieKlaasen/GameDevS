using Microsoft.Xna.Framework;

namespace GameDevS.Movement.Controllers
{
    public interface IMovementController
    {
        //Vector2 GetDesiredVelocity(IMovable movable, GameTime gameTime);
        Vector2 GetDesiredVelocity(IMovable movable, float dt);
    }
}
