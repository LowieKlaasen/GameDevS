using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameDevS
{
    public class CollisionManager2 : ICollisionService2
    {
        private readonly List<ICollidable2> collidables = new List<ICollidable2>();

        public void Register(ICollidable2 collidable)
        {
            collidables.Add(collidable);
        }

        public void Remove(ICollidable2 collidable)
        {
            collidables.Remove(collidable);
        }

        //public bool WouldColide(ICollidable2 movingObject, Vector2 newPosition)
        //{
        //    int offsetX = 0;
        //    int offsetY = 0;

        //    if (movingObject is Sprite sprite)
        //    {
        //        offsetX = sprite.hitboxStartX;
        //        offsetY = sprite.hitboxStartY;
        //    }

        //    Rectangle newHitbox = new Rectangle(
        //        (int)newPosition.X + offsetX,
        //        (int)newPosition.Y + offsetY,
        //        movingObject.HitBox.Width,
        //        movingObject.HitBox.Height
        //    );

        //    foreach (ICollidable2 collidable in collidables)
        //    {
        //        if (!ReferenceEquals(collidable, movingObject) && newHitbox.Intersects(collidable.HitBox))
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        public struct CollisionResult
        {
            public CollisionDirection Direction;
            public ICollidable2 Collidable;

            public CollisionResult(CollisionDirection direction, ICollidable2 collidable)
            {
                Direction = direction;
                Collidable = collidable;
            }
        }

        public CollisionResult CheckCollision(ICollidable2 movingObject, Vector2 startPosition, Vector2 endPosition)
        {
            int offsetX = 0;
            int offsetY = 0;

            if (movingObject is Sprite sprite)
            {
                offsetX = sprite.hitboxStartX;
                offsetY = sprite.hitboxStartY;
            }

            int steps = (int)Math.Ceiling(Vector2.Distance(startPosition, endPosition));
            if (steps < 1) steps = 1;

            Vector2 step = (endPosition - startPosition) / steps;
            Vector2 checkPosition = startPosition;

            for (int i = 0; i <= steps; i++)
            {
                Rectangle hitbox = new Rectangle(
                    (int)checkPosition.X + offsetX,
                    (int)checkPosition.Y + offsetY,
                    movingObject.HitBox.Width,
                    movingObject.HitBox.Height
                );

                foreach (ICollidable2 collidable in collidables)
                {
                    if (ReferenceEquals(collidable, movingObject))
                        continue;

                    if (hitbox.Intersects(collidable.HitBox))
                    {
                        Rectangle other = collidable.HitBox;
                        Vector2 velocity = endPosition - startPosition;

                        CollisionDirection direction;
                        if (Math.Abs(velocity.Y) > Math.Abs(velocity.X))
                        {
                            direction = (velocity.Y > 0) ? CollisionDirection.BOTTOM : CollisionDirection.TOP;
                        }
                        else
                        {
                            direction = (velocity.X > 0) ? CollisionDirection.RIGHT : CollisionDirection.LEFT;
                        }

                        return new CollisionResult(direction, collidable);
                    }
                }

                checkPosition += step;
            }

            return new CollisionResult(CollisionDirection.NONE, null);
        }

        public bool WouldCollide(ICollidable2 movingObject, Vector2 startPosition, Vector2 endPosition)
        {
            return CheckCollision(movingObject, startPosition, endPosition).Direction != CollisionDirection.NONE;
        }

        public ICollidable2 GetCollidableAt(Vector2 position)
        {
            foreach (var collidable in collidables)
            {
                if (collidable.HitBox.Contains(position))
                    return collidable;
            }
            return null;
        }
    }
}
