using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal interface ICollidable
    {
        Rectangle Bounds { get; }

        void OnCollision(ICollidable other);
    }
}
