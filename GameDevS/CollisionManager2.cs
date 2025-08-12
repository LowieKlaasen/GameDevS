using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDevS
{
    internal class CollisionManager2 : ICollisionService2
    {
        private readonly List<ICollidable2> collidables = new List<ICollidable2>();

        public void Register(ICollidable2 collidable)
        {
            collidables.Add(collidable);
        }

        public bool WouldColide(ICollidable2 movingObject, Vector2 newPosition)
        {
            int offsetX = 0;
            int offsetY = 0;

            if (movingObject is Sprite sprite)
            {
                offsetX = sprite.hitboxStartX;
                offsetY = sprite.hitboxStartY;
            }

            Rectangle newHitbox = new Rectangle(
                (int)newPosition.X + offsetX,
                (int)newPosition.Y + offsetY,
                movingObject.HitBox.Width,
                movingObject.HitBox.Height
            );

            foreach (ICollidable2 collidable in collidables)
            {
                if (!ReferenceEquals(collidable, movingObject) && newHitbox.Intersects(collidable.HitBox))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
