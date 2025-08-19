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

        public Player(Vector2 position, float scale, List<Sprite> collisionGroup, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight) 
            : base(position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight) 
        {
            this.collisionGroup = collisionGroup;

            speed = 240f;
            jumpSpeed = 680f;

            Health = new Health(5);
        }

        public override void Update(float dt)
        {
            //float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!Health.IsAlive)
            {
                Die();
            }

            if (IsDying)
            {
                deathTimer -= dt;
                if (deathTimer < 0f)
                {
                    // ToDo: Die
                }
            }

            base.Update(dt);
        }

        public void TakeDamage(int amount)
        {
            Health.TakeDamage(amount);

            currentState = AnimationState.HURTING;
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
