using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position;

        private int hitboxStartX;
        private int hitboxStartY;
        private int hitboxWidth;
        private int hitboxHeight;

        private float Scale;

        //public Rectangle Rectangle 
        //{ 
        //    get
        //    {
        //        return new Rectangle(
        //            (int)position.X, 
        //            (int)position.Y, 
        //            (int)(texture.Width * Scale), 
        //            (int)(texture.Height * Scale)
        //        );
        //    }
        //}

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)position.X + hitboxStartX,
                    (int)position.Y + hitboxStartY,
                    hitboxWidth,
                    hitboxHeight
                );
            }
        }

        public Animation animation;

        public Sprite(Texture2D texture, Vector2 position, float scale, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            this.texture = texture;
            this.position = position;
            Scale = scale;

            hitboxStartX = 0;
            hitboxStartY = 0;
            hitboxWidth = (int)(texture.Width * Scale);
            hitboxHeight = (int)(texture.Height * Scale);

            animation = new Animation();
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, numberOfWidthSprites, numberOfHeightSprites);
        }
        public Sprite(Texture2D texture, Vector2 position, float scale, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            this.texture = texture;
            this.position = position;
            Scale = scale;

            this.hitboxStartX = hitboxStartX;
            this.hitboxStartY = hitboxStartY;
            this.hitboxWidth = hitboxWidth;
            this.hitboxHeight = hitboxHeight;

            animation = new Animation();
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, numberOfWidthSprites, numberOfHeightSprites);
        }

        public virtual void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, Rectangle, Color.White);

            spriteBatch.Draw(
                texture,
                position,
                animation.CurrentFrame.SourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
