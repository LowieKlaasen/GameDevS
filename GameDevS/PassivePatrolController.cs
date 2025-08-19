using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal class PassivePatrolController : IMovementController
    {
        private int direction = 1;

        public Vector2 GetDesiredVelocity(IMovable movable, float dt)
        {
            //if (movable is PassivePatrolEnemy enemy)
            //{
            //    return new Vector2(direction * movable.Speed, 0);
            //}

            //return Vector2.Zero;

            return new Vector2(direction * movable.Speed, 0);
        }

        public void ReverseDirection(IMovable movable)
        {
            if (movable is PassivePatrolEnemy enemy)
            {
                direction *= -1;
            }
        }
    }
}
