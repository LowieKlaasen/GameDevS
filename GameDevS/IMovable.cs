using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal interface IMovable
    {
        public Vector2 Position { get; set; }
        public float Speed { get; }

        public Vector2 Velocity { get; set; }

        public float JumpSpeed { get; }

        public bool IsGrounded { get; set; }

        public IMovementController MovementController { get; set; }
    }
}
