using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameDevS
{
    internal abstract class Enemy : Sprite
    {
        public bool IsAlive;
        public bool IsDying => currentState == AnimationState.DYING;

        public int Damage;

        public float deathTimer = 0f;
        protected abstract float deathAnimationDuration { get; }

        public Enemy(Texture2D texture, Vector2 position, float scale, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites) 
            : base(texture, position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, numberOfWidthSprites, numberOfHeightSprites)
        {
            IsAlive = true;
            Damage = 10;
        }

        public override void Update(float dt)
        {
            //float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (IsDying)
            {
                deathTimer -= dt;
                if (deathTimer < 0f)
                {
                    IsAlive = false;
                }
            }

            base.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsAlive)
            {
                return;
            }

            base.Draw(spriteBatch);
        }

        public void Die()
        {
            if (IsDying)
            {
                return;
            }

            Debug.WriteLine("Enemy died");

            currentState = AnimationState.DYING;
            ServiceLocator.AudioService.Play("monsterDeath");

            deathTimer = deathAnimationDuration;
            speed /= 3;
        }
    }
}
