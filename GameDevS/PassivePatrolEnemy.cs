using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class PassivePatrolEnemy : Enemy
    {
        //public int Direction { get; set; }

        protected override float deathAnimationDuration { get { return 1f; } }

        public PassivePatrolEnemy(Vector2 position, float scale, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, IMovementController movementController) 
            : base(position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, movementController)
        {
            speed = 75f;

            //Direction = 1;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }
    }
}
