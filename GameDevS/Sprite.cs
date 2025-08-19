using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal class Sprite : AnimatedEntity, ICollidable2, IMovable
    {
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

        public Rectangle HitBox => new Rectangle(
            (int)Position.X + hitboxStartX,
            (int)Position.Y + hitboxStartY,
            hitboxWidth,
            hitboxHeight
        );

        public Vector2 Velocity { get; set; }

        public Sprite(Vector2 position, float scale, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight)
            : base(position, scale)
        {
            this.hitboxStartX = hitboxStartX;
            this.hitboxStartY = hitboxStartY;
            this.hitboxWidth = hitboxWidth;
            this.hitboxHeight = hitboxHeight;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }
    }
}
