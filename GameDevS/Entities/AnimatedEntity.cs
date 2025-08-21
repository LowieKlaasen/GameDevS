using GameDevS.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameDevS.Entities
{
    public abstract class AnimatedEntity
    {
        public Vector2 Position { get; set; }
        protected float Scale;
        public SpriteEffects Effect = SpriteEffects.None;

        protected Dictionary<AnimationState, Animation2> animations = new Dictionary<AnimationState, Animation2>();
        protected AnimationState currentState;
        public AnimationState CurrentState { get { return currentState; } }

        protected AnimatedEntity(Vector2 position, float scale)
        {
            Position = position;
            Scale = scale;
            currentState = AnimationState.IDLE;
        }

        public virtual void Update(float dt) 
        {
            if (animations.ContainsKey(currentState)) 
            {
                animations[currentState].Update(dt);
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
