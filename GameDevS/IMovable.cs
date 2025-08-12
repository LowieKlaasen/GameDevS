using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal interface IMovable
    {
        public Vector2 Position { get; set; }
        public float Speed { get; }

        //public void Move(); (MovementManager handles movement, so entities don't have to move themselves)
    }
}
