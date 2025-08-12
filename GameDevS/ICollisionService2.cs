using Microsoft.Xna.Framework;
namespace GameDevS
{
    internal interface ICollisionService2
    {
        bool WouldColide(ICollidable2 movingObject, Vector2 newPosition);
    }
}
