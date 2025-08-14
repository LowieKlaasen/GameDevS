using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class Enemy : Sprite
    {
        public Enemy(Texture2D texture, Vector2 position, float scale, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites) 
            : base(texture, position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, numberOfWidthSprites, numberOfHeightSprites)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
