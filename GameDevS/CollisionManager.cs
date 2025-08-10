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

        //public void ResolvePlayerTileCollision(Player player, TileMap tileMap) 
        //{
        //    Rectangle playerBounds = player.Bounds;
        //    Vector2 playerVelocity = player.Velocity;

        //    playerBounds.X += (int)player.Velocity.X;

        //    CheckAndResolveAxis(ref playerBounds, tileMap, true, ref playerVelocity);

        //    playerBounds.Y += (int)player.Velocity.Y;

        //    CheckAndResolveAxis(ref playerBounds, tileMap, false, ref playerVelocity);

        //    player.Position = new Vector2(playerBounds.X, playerBounds.Y);
        //    player.Velocity = playerVelocity;
        //}

        public void ResolvePlayerTileCollision(Player player, TileMap tileMap)
        {
            Rectangle playerBounds = player.Bounds;

            // Horizontal check only
            CheckAndResolveAxis(ref playerBounds, tileMap, true, ref player.velocity);

            // Vertical check only
            CheckAndResolveAxis(ref playerBounds, tileMap, false, ref player.velocity);

            // Apply corrected position
            player.Position = new Vector2(playerBounds.X - player.hitboxStartX, playerBounds.Y - player.hitboxStartY);
        }

        //private void CheckAndResolveAxis(ref Rectangle bounds, TileMap map, bool horizontal, ref Vector2 velocity)
        //{
        //    int startX = bounds.Left / map.TILESIZE;
        //    int endX = bounds.Right / map.TILESIZE;
        //    int startY = bounds.Top / map.TILESIZE;
        //    int endY = bounds.Bottom / map.TILESIZE;

        //    for (int y = startY; y <= endY; y++)
        //    {
        //        for (int x = startX; x <= endX; x++)
        //        {
        //            if (map.IsSolidTile(x,y))
        //            {
        //                Rectangle tileBounds = map.GetTileBounds(x, y);
        //                if (bounds.Intersects(tileBounds))
        //                {
        //                    Rectangle overlap = Rectangle.Intersect(bounds, tileBounds);

        //                    if (horizontal)
        //                    {
        //                        if (velocity.X > 0)
        //                        {
        //                            bounds.X -= overlap.Width;
        //                        }
        //                        else if (velocity.X < 0)
        //                        {
        //                            bounds.X += overlap.Width;
        //                        }
        //                        else
        //                        {
        //                            velocity.X = 0;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (velocity.Y > 0)
        //                        {
        //                            bounds.Y -= bounds.Height;
        //                        }
        //                        else if (velocity.Y < 0)
        //                        {
        //                            bounds.Y += bounds.Height;
        //                        }
        //                        else
        //                        {
        //                            velocity.Y = 0;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        private void CheckAndResolveAxis(ref Rectangle bounds, TileMap map, bool horizontal, ref Vector2 velocity)
        {
            int startX = bounds.Left / map.TILESIZE;
            int endX = bounds.Right / map.TILESIZE;
            int startY = bounds.Top / map.TILESIZE;
            int endY = bounds.Bottom / map.TILESIZE;

            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    if (map.IsSolidTile(x, y))
                    {
                        Rectangle tileBounds = map.GetTileBounds(x, y);
                        if (bounds.Intersects(tileBounds))
                        {
                            Rectangle overlap = Rectangle.Intersect(bounds, tileBounds);

                            if (horizontal)
                            {
                                if (velocity.X > 0)
                                {
                                    bounds.X -= overlap.Width;
                                    velocity.X = 0;
                                }
                                else if (velocity.X < 0)
                                {
                                    bounds.X += overlap.Width;
                                    velocity.X = 0;
                                }
                            }
                            else
                            {
                                if (velocity.Y > 0)
                                {
                                    bounds.Y -= overlap.Height;
                                    velocity.Y = 0;
                                }
                                else if (velocity.Y < 0)
                                {
                                    bounds.Y += overlap.Height;
                                    velocity.Y = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
