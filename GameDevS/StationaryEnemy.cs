using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class StationaryEnemy : Enemy
    {
        protected override float deathAnimationDuration { get { return 1f; } }

        private float flipTimer = 0f;
        private float flipDelay = 2f;

        public StationaryEnemy(Vector2 position, float scale, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, IMovementController movementController) 
            : base(position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, movementController)
        {
            Effect = SpriteEffects.FlipHorizontally;
        }

        public override void Update(float dt)
        {
            if (flipTimer <= 0f)
            {
                FlipSprite();
                flipTimer = flipDelay;
            }
            else
            {
                flipTimer -= dt;
            }

            base.Update(dt);
        }

        public void FlipSprite()
        {
            if (Effect == SpriteEffects.FlipHorizontally)
            {
                Effect = SpriteEffects.None;
                return;
            }
            Effect = SpriteEffects.FlipHorizontally;
        }

    }
}
