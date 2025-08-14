using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal class PassivePatrolController : IMovementController
    {
        private int direction = 1;

        public Vector2 GetDesiredVelocity(IMovable movable, GameTime gameTime)
        {
            return new Vector2(direction * movable.Speed, 0);
        }

        public void ReverseDirection() => direction *= -1;
    }
}
