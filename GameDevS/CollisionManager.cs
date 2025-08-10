using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace GameDevS
{
    internal class CollisionManager
    {
        private List<ICollidable> collidables;

        public CollisionManager()
        {
            collidables = new List<ICollidable>();
        }

        public void Register(ICollidable collidable)
        {
            if (!collidables.Contains(collidable))
            {
                collidables.Add(collidable);
            }
        }

        public void Unregister(ICollidable collidable)
        {
            collidables.Remove(collidable);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < collidables.Count; i++)
            {
                for (int j = i + 1; j < collidables.Count; j++)
                {
                    ICollidable a = collidables[i];
                    ICollidable b = collidables[j];

                    if (a.Bounds.Intersects(b.Bounds))
                    {
                        a.OnCollision(b);
                        b.OnCollision(a);
                    }
                }
            }
        }
    }
}
