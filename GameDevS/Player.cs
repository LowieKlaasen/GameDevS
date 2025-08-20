using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameDevS
{
    internal class Player : Sprite, ICollidable2
    {
        // ToDo: Check if this is necessery (& remove)
        private List<Sprite> collisionGroup;

        public Health Health { get; private set; }

        public float deathTimer = 0f;
        public float deathAnimationDuration = 1f;

        public bool IsDying => currentState == AnimationState.DYING;

        public int Score;

        private float hurtTimer = 0f;
        private float hurtDuration = 0.3f;

        public Vector2 KnockbackVelocity { get; private set; } = Vector2.Zero;
        private float knockbackDuration = 0.2f;
        private float knockbackTimer = 0f;

        private bool isInvincible = false;
        private float invincibleTimer = 0f;
        private float invincibleDuration = 1f;

        private bool isVisible = true;

        public bool IsKnockBackActive => knockbackTimer > 0f;

        public Player(Vector2 position, float scale, List<Sprite> collisionGroup, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, IMovementController movementController) 
            : base(position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, movementController) 
        {
            this.collisionGroup = collisionGroup;

            speed = 240f;
            jumpSpeed = 680f;

            Health = new Health(5);
            Score = 0;

            //Health.TakeDamage(4);
        }

        public override void Update(float dt)
        {
            if (hurtTimer > 0f)
            {
                hurtTimer -= dt;
                if (hurtTimer <= 0f && !IsDying)
                {
                    SetAnimation(AnimationState.IDLE);
                }
            }

            if (knockbackTimer > 0f)
            {
                knockbackTimer -= dt;
            }

            if (isInvincible)
            {
                invincibleTimer -= dt;
                if (invincibleTimer <= 0f)
                {
                    isInvincible = false;
                }
                else
                {
                    if ((int)(invincibleTimer * 10) % 2 == 0)
                    {
                        isVisible = false;
                    }
                    else
                    {
                        isVisible = true;
                    }
                }
            }
            else
            {
                isVisible = true;
            }

            base.Update(dt);
        }

        public void TakeDamage(int amount, CollisionDirection collisionDirection)
        {
            if (isInvincible || Health.Current <= 0)
            {
                return;
            }

            Health.TakeDamage(amount);

            SetAnimation(AnimationState.HURTING);
            hurtTimer = hurtDuration;

            ServiceLocator.AudioService.Play("playerHurt");

            float horizontalForce = 350f;
            float verticalForce = -150f;

            if (collisionDirection == CollisionDirection.LEFT)
            {
                KnockbackVelocity = new Vector2(-horizontalForce, verticalForce);
            }
            else if (collisionDirection == CollisionDirection.RIGHT)
            {
                KnockbackVelocity = new Vector2(horizontalForce, verticalForce);
            }
            else
            {
                KnockbackVelocity = new Vector2(0, verticalForce);
            }

            knockbackTimer = knockbackDuration;

            isInvincible = true;
            invincibleTimer = invincibleDuration;

            if (Health.Current <= 0)
            {
                Die();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isVisible)
            {
                return;
            }

            base.Draw(spriteBatch);
        }

        private void Die() 
        {
            if (IsDying)
            {
                return;
            }

            Debug.WriteLine("Player died");

            currentState = AnimationState.DYING;
            //ServiceLocator.AudioService.Play("");

            deathTimer = deathAnimationDuration;
            speed /= 2;
        }
    }
}
