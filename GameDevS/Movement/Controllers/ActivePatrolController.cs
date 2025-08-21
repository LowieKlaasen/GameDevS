using GameDevS.Collision;
using GameDevS.Entities.Enemies;
using GameDevS.Entities.PlayerMap;
using GameDevS.Movement;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace GameDevS.Movement.Controllers
{
    internal class ActivePatrolController : IMovementController
    {
        private readonly Player player;
        private readonly float detectionRadius;

        private int direction = -1;
        public int Direction { get { return direction; } }

        private CollisionManager2 collisionManager;

        public ActivePatrolController(Player player, CollisionManager2 collisionManager, float detectionRadius = 250f)
        {
            this.player = player;
            this.detectionRadius = detectionRadius;

            this.collisionManager = collisionManager;
        }
        public Vector2 GetDesiredVelocity(IMovable movable, float dt)
        {
            if (movable is PatrolEnemy enemy)
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
                    if (!enemy.IsGroundAhead(collisionManager, enemy))
                    {
                        ReverseDirection(enemy);
                        return Vector2.Zero;
                    }

                    return new Vector2 (direction * enemy.Speed, enemy.Velocity.Y);
                }
            }

            return Vector2.Zero;
        }

        public void ReverseDirection(IMovable movable)
        {
            if (movable is PatrolEnemy enemy)
            {
                direction *= -1;
            }
        }
    }
}
