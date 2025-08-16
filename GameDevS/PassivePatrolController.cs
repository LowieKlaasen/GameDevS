using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal class PassivePatrolController : IMovementController
    {
        //private int direction = 1;

        public Vector2 GetDesiredVelocity(IMovable movable, float dt)
        {
            if (movable is PassivePatrolEnemy enemy)
            {
                return new Vector2(enemy.Direction * movable.Speed, 0);
            }

            return Vector2.Zero;
        }

        public void ReverseDirection(IMovable movable)
        {
            if (movable is PassivePatrolEnemy enemy)
            {
                enemy.Direction *= -1;
            }
        }
    }
}
