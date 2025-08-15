using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public Player(Texture2D texture, Vector2 position, float scale, List<Sprite> collisionGroup, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites) : base(texture, position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, numberOfWidthSprites, numberOfHeightSprites) 
        {
            this.collisionGroup = collisionGroup;

            speed = 240f;
            jumpSpeed = 680f;

            Health = new Health(5);
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

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

            base.Update(gameTime);
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
