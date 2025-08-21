using GameDevS.Entities;
using GameDevS.Entities.Enemies;
using GameDevS.Entities.PlayerMap;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS.Animations
{
    public class AnimationUpdater
    {
        public void UpdateAnimation(Sprite sprite)
        {
            if (sprite is Enemy enemy && enemy.IsDying 
                || sprite is Player player && player.IsDying)
            {
                return;
            }

            if (sprite is Player playerB && playerB.CurrentState == AnimationState.HURTING)
            {
                return;
            }

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
