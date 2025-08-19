using Microsoft.Xna.Framework;
using System;

namespace GameDevS
{
    internal class ActivePatrolController : IMovementController
    {
        private readonly Player player;
        private readonly float detectionRadius;

        private int direction = 1;

        public ActivePatrolController(Player player, float detectionRadius = 200f)
        {
            this.player = player;
            this.detectionRadius = detectionRadius;
        }
        public Vector2 GetDesiredVelocity(IMovable movable, float dt)
        {
            if (movable is PassivePatrolEnemy enemy)
            {
                float distance = Vector2.Distance(enemy.Position, player.Position);

                if (distance <= detectionRadius)
                {
                    float direction = MathF.Sign(player.Position.X - enemy.Position.X);
                    return new Vector2(direction * movable.Speed, 0);
                }
                else
                {
                    return new Vector2 (direction * movable.Speed, 0);
                }
            }

            return Vector2.Zero;
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
