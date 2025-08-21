using Microsoft.Xna.Framework;
using static GameDevS.Collision.CollisionManager2;
namespace GameDevS.Collision
{
    public interface ICollisionService2
    {
        //bool WouldColide(ICollidable2 movingObject, Vector2 newPosition);

        public CollisionResult CheckCollision(ICollidable2 movingObject, Vector2 newPosition, Vector2 endPosition);
    }
}
