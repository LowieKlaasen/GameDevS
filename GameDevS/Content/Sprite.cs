using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS.Content
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position;

        private float Scale;

        public Rectangle Rectangle 
        { 
            get
            {
                return new Rectangle(
                    (int)position.X, 
                    (int)position.Y, 
                    texture.Width, 
                    texture.Height
                );
            }
        }

        public Sprite(Texture2D texture, Vector2 position, float scale)
        {
            this.texture = texture;
            this.position = position;
            this.Scale = scale;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, Rectangle, Color.White);

            spriteBatch.Draw(
                texture,
                position,
                null,
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
