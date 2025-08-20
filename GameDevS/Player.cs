using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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

        public Vector2 KnockbackVelocity { get; private set; } = Vector2.Zero;
        private float knockbackDuration = 0.2f;
        private float knockbackTimer = 0f;

        public int Score;

        private float hurtTimer = 0f;
        private float hurtDuration = 0.3f;

        public Player(Vector2 position, float scale, List<Sprite> collisionGroup, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, IMovementController movementController) 
            : base(position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, movementController) 
        {
            this.collisionGroup = collisionGroup;

            speed = 240f;
            jumpSpeed = 680f;

            Health = new Health(5);
            Score = 0;
        }

        public override void Update(float dt)
        {
            if (hurtTimer > 0f)
            {
                hurtTimer -= dt;
                if (hurtTimer < 0f)
                {
                    SetAnimation(AnimationState.IDLE);
                }
            }

            base.Update(dt);
        }

        public void TakeDamage(int amount)
        {
            if (Health.Current <= 0)
            {
                return;
            }

            Health.TakeDamage(amount);

            SetAnimation(AnimationState.HURTING);
            //currentState = AnimationState.HURTING;
            hurtTimer = hurtDuration;
            ServiceLocator.AudioService.Play("playerHurt");

            if (Health.Current <= 0)
            {
                Die();
            }
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
