using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class AnimationUpdater
    {
        public void UpdateAnimation(Sprite sprite)
        {
            if (sprite.Velocity.X > 0)
            {
                sprite.Effect = SpriteEffects.None;
            }
            else if (sprite.Velocity.X < 0)
            {
                sprite.Effect = SpriteEffects.FlipHorizontally;
            }

            if (!sprite.IsGrounded)
            {
                sprite.SetAnimation(AnimationState.JUMPING);
            }
            else if (sprite.Velocity.X != 0)
            {
                sprite.SetAnimation(AnimationState.RUNNING);
            }
            else
            {
                sprite.SetAnimation(AnimationState.IDLE);
            }
        }
    }
}
