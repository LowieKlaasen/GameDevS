using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal interface IMovable
    {
        public Vector2 Position { get; set; }
        public float Speed { get; }

        public float JumpSpeed { get; }

        public bool IsGrounded { get; set; }
    }
}
