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
            Rectangle newHitbox = new Rectangle(
                (int)newPosition.X,
                (int)newPosition.Y,
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

        //public bool CheckTileMapCollisions(Player player, TileMap2 tileMap)
        //{
        //    foreach (Tile2 tile in tileMap.GetCollidables())
        //    {
        //        if (tile.HitBox.Intersects(player.HitBox))
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
    }
}
