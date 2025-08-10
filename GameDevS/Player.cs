using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameDevS
{
    internal class Player : Sprite
    {
        private const int Speed = 4;

        public Vector2 velocity;
        public Vector2 Velocity 
        { 
            get { return velocity; } 
            set { velocity = value; } 
        }

        private List<Sprite> collisionGroup;

        private float gravity = 1f;

        public Player(Texture2D texture, Vector2 position, float scale, List<Sprite> collisionGroup, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites) : base(texture, position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, numberOfWidthSprites, numberOfHeightSprites) 
        {
            this.collisionGroup = collisionGroup;

            velocity = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            velocity = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X += Speed;
                this.effect = SpriteEffects.None;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X -= Speed;
                this.effect = SpriteEffects.FlipHorizontally;
            }

            foreach (var sprite in collisionGroup)
            {
                if (sprite != this && sprite.Rectangle.Intersects(Rectangle))
                {
                    Position.X -= velocity.X;
                }
            }

            velocity.Y = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                velocity.Y -= Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                velocity.Y += Speed;
            }

            if (velocity != Vector2.Zero)
            {
                velocity = Vector2.Normalize(velocity) * Speed;
                Position += velocity;
            }

            foreach (var sprite in collisionGroup)
            {
                if (sprite != this && sprite.Rectangle.Intersects(Rectangle))
                {
                    Position.Y -= velocity.Y;
                }
            }
        }
    }
}
