using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameDevS
{
    internal class Player : Sprite
    {
        private const int Speed = 4;

        private List<Sprite> collisionGroup;

        public Player(Texture2D texture, Vector2 position, float scale, List<Sprite> collisionGroup, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites) : base(texture, position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, numberOfWidthSprites, numberOfHeightSprites) 
        {
            this.collisionGroup = collisionGroup;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float changeX = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                changeX += Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                changeX -= Speed;
            }
            position.X += changeX;

            foreach (var sprite in collisionGroup)
            {
                if (sprite != this && sprite.Rectangle.Intersects(Rectangle))
                {
                    position.X -= changeX;
                }
            }

            float changeY = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                changeY -= Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                changeY += Speed;
            }
            position.Y += changeY;

            foreach (var sprite in collisionGroup)
            {
                if (sprite != this && sprite.Rectangle.Intersects(Rectangle))
                {
                    position.Y -= changeY;
                }
            }
        }
    }
}
