using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameDevS
{
    internal class Player : Sprite, ICollidable2 /*IMovable (moven to Sprite)*/
    {
        //public Rectangle HitBox { get; } (Already in Sprite class)

        //public Vector2 Position { get; set; }
        //public float Speed { get; }

        //private const int Speed = 4;
        private const float JumpVelocity = -12f;

        public Vector2 velocity;
        public Vector2 Velocity 
        { 
            get { return velocity; } 
            set { velocity = value; } 
        }

        private List<Sprite> collisionGroup;

        private float gravity = 1f;
        private bool isGrounded = false;

        public Player(Texture2D texture, Vector2 position, float scale, List<Sprite> collisionGroup, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites) : base(texture, position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, numberOfWidthSprites, numberOfHeightSprites) 
        {
            this.collisionGroup = collisionGroup;

            velocity = Vector2.Zero;
            speed = 4;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ////velocity = Vector2.Zero;
            //velocity.X = 0;

            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //{
            //    velocity.X += Speed;
            //    this.effect = SpriteEffects.None;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //{
            //    velocity.X -= Speed;
            //    this.effect = SpriteEffects.FlipHorizontally;
            //}

            ////foreach (var sprite in collisionGroup)
            ////{
            ////    if (sprite != this && sprite.Rectangle.Intersects(Rectangle))
            ////    {
            ////        Position.X -= velocity.X;
            ////    }
            ////}

            ////velocity.Y = 1;
            ////if (Keyboard.GetState().IsKeyDown(Keys.Up))
            ////{
            ////    velocity.Y -= Speed;
            ////}
            ////if (Keyboard.GetState().IsKeyDown(Keys.Down))
            ////{
            ////    velocity.Y += Speed;
            ////}

            ////if (velocity != Vector2.Zero)
            ////{
            ////    velocity = Vector2.Normalize(velocity) * Speed;
            ////    Position += velocity;
            ////}

            //velocity.Y += gravity;

            //if (isGrounded && Keyboard.GetState().IsKeyDown(Keys.Up))
            //{
            //    velocity.Y = JumpVelocity;
            //    isGrounded = false;
            //}

            //Position += velocity;

            ////foreach (var sprite in collisionGroup)
            ////{
            ////    if (sprite != this && sprite.Rectangle.Intersects(Rectangle))
            ////    {
            ////        Position.Y -= velocity.Y;
            ////    }
            ////}

            //foreach (var sprite in collisionGroup)
            //{
            //    if (sprite != this && sprite.Rectangle.Intersects(Rectangle))
            //    {
            //        if (velocity.Y > 0)
            //        {
            //            //Position.Y = sprite.Rectangle.Top
            //        }
            //    }
            //}
        }
    }
}
