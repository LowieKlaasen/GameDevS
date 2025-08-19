using Microsoft.Xna.Framework;
using System;

namespace GameDevS
{
    internal class ActivePatrolController : IMovementController
    {
        private readonly Player player;
        private readonly float detectionRadius;

        private int direction = -1;

        public ActivePatrolController(Player player, float detectionRadius = 400f)
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
                    float moveDirection = MathF.Sign(player.Position.X - enemy.Position.X);
                    
                    Vector2 velocity = new Vector2(moveDirection * enemy.Speed, enemy.Velocity.Y);

                    if (player.Position.Y + 50 < enemy.Position.Y && enemy.IsGrounded)
                    {
                        if (enemy.CanJump())
                        {
                            velocity.Y = -enemy.JumpSpeed;
                            enemy.JumpCooldown = enemy.JumpDelay;
                        }
                    }
                    return velocity;
                }
                else
                {
                    return new Vector2 (direction * enemy.Speed, enemy.Velocity.Y);
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
