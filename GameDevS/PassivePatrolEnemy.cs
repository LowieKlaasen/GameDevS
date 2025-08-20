using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameDevS
{
    internal class PassivePatrolEnemy : Enemy
    {
        public float JumpCooldown;
        public readonly float JumpDelay;

        protected override float deathAnimationDuration { get { return 1f; } }

        public PassivePatrolEnemy(Vector2 position, float scale, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, IMovementController movementController) 
            : base(position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, movementController)
        {
            speed = 75f;

            jumpSpeed = 500;

            JumpDelay = 2f;
        }

        public override void Update(float dt)
        {
            if (JumpCooldown > 0)
            {
                JumpCooldown -= dt;
            }

            base.Update(dt);
        }

        public bool CanJump()
        {
            return IsGrounded && JumpCooldown <= 0f && currentState != AnimationState.DYING;
        }

        public bool IsGroundAhead(CollisionManager2 collisionManager, PassivePatrolEnemy enemy)
        {
            if (enemy.MovementController is ActivePatrolController controller)
            {
                Vector2 start = new Vector2(
                    enemy.Position.X + controller.Direction * (enemy.HitBox.Width / 2 + 2),
                    enemy.Position.Y + enemy.HitBox.Height + 1
                );

                Vector2 end = start + new Vector2(0, 1);

                var collisionResult = collisionManager.CheckCollision(enemy, start, end);
                if (collisionResult.Collidable is Tile2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
