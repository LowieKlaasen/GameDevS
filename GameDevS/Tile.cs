using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameDevS
{
    internal class Tile : ICollidable
    {
        private int TILESIZE = 48;

        private Rectangle bounds;
        public Rectangle Bounds
        {
            get
            {
                return bounds;
            }
        }

        public Texture2D Texture;
        private Rectangle sourceRectangle;

        public Tile(Texture2D texture, Rectangle srcRect)
        {
            bounds = new Rectangle(0, 0, TILESIZE, TILESIZE);

            Texture = texture;
            sourceRectangle = srcRect;
        }

        public void OnCollision(ICollidable other)
        {
            // ToDo: Implement logic
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, sourceRectangle, Color.White);
        }
    }
}
