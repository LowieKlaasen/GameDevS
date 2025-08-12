using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameDevS
{
    internal class Sprite : ICollidable2, IMovable
    {
        public Texture2D texture;
        public Vector2 Position { get; set; }

        protected float speed;
        public float Speed
        {
            get { return speed; }
        }

        protected float jumpSpeed;
        public float JumpSpeed
        {
            get { return jumpSpeed; }
        }

        protected bool isGrounded;
        public bool IsGrounded
        {
            get { return isGrounded; }
            set { isGrounded = value; }
        }

        public int hitboxStartX;
        public int hitboxStartY;
        private int hitboxWidth;
        private int hitboxHeight;

        private float Scale;

        protected SpriteEffects effect = SpriteEffects.None;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)Position.X + hitboxStartX,
                    (int)Position.Y + hitboxStartY,
                    hitboxWidth,
                    hitboxHeight
                );
            }
        }

        public Rectangle HitBox
        {
            get { return Rectangle; }
        }

        public Animation animation;

        public Sprite(Texture2D texture, Vector2 position, float scale, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            this.texture = texture;
            this.Position = position;
            Scale = scale;

            hitboxStartX = 0;
            hitboxStartY = 0;
            hitboxWidth = (int)(texture.Width * Scale);
            hitboxHeight = (int)(texture.Height * Scale);

            animation = new Animation();
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, numberOfWidthSprites, numberOfHeightSprites);

            speed = 4;
        }
        public Sprite(Texture2D texture, Vector2 position, float scale, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            this.texture = texture;
            this.Position = position;
            Scale = scale;

            this.hitboxStartX = hitboxStartX;
            this.hitboxStartY = hitboxStartY;
            this.hitboxWidth = hitboxWidth;
            this.hitboxHeight = hitboxHeight;

            animation = new Animation();
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, numberOfWidthSprites, numberOfHeightSprites);

            speed = 4;
        }

        public virtual void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (animation.CurrentFrame != null)
            {
                spriteBatch.Draw(
                    texture,
                    Position,
                    animation.CurrentFrame.SourceRectangle,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    Scale,
                    effect,
                    0f
                );
            }
        }
    }
}
