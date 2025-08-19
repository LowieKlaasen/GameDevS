using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

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

            JumpDelay = 1f;
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
            return IsGrounded && JumpCooldown <= 0f;
        }
    }
}
