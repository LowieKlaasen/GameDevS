using Microsoft.Xna.Framework;

namespace GameDevS.Movement.Controllers
{
    internal class StationaryController : IMovementController
    {
        public Vector2 GetDesiredVelocity(IMovable movable, float dt)
        {
            return Vector2.Zero;
        }
    }
}
