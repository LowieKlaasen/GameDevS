using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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

        public SpriteEffects Effect = SpriteEffects.None;

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

        protected Dictionary<AnimationState, Animation2> animations = new Dictionary<AnimationState, Animation2>();
        protected AnimationState currentState;

        public Vector2 Velocity { get; set; }

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

            //speed = 4;

            currentState = AnimationState.IDLE;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (animations.ContainsKey(currentState))
            {
                animations[currentState].Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (animations.ContainsKey(currentState))
            {
                Animation2 animation = animations[currentState];
                if (animation.CurrentFrame != null)
                {
                    spriteBatch.Draw(
                        animation.SpriteSheet.Texture,
                        Position,
                        animation.CurrentFrame.SourceRectangle,
                        Color.White,
                        0f,
                        Vector2.Zero,
                        Scale,
                        Effect,
                        0f
                    );
                }
            }
        }

        public void AddAnimation(AnimationState state, Animation2 animation)
        {
            animations[state] = animation;
            if (animations.Count == 1)
            {
                currentState = state;
            }
        }

        public void SetAnimation(AnimationState state)
        {
            if (currentState != state)
            {
                currentState = state;
            }
        }
    }
}
