using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameDevS
{
    internal class Enemy : Sprite
    {
        public bool IsAlive;

        public int Damage;

        public Enemy(Texture2D texture, Vector2 position, float scale, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites) 
            : base(texture, position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, numberOfWidthSprites, numberOfHeightSprites)
        {
            IsAlive = true;
            Damage = 10;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsAlive)
            {
                return;
            }

            base.Update(gameTime);
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
            // ToDo: add die animation (+ sound)
            Debug.WriteLine("Enemy died");

            IsAlive = false;
        }
    }
}
