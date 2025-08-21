using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameDevS
{
    public abstract class FactoryBase
    {
        protected readonly ContentManager contentManager;
        protected readonly int TILESIZE;

        protected FactoryBase(ContentManager contentManager, int tileSize)
        {
            this.contentManager = contentManager;
            TILESIZE = tileSize;
        }

        protected Vector2 ConvertCoordinates(Vector2 coordinates)
        {
            return new Vector2(
                coordinates.X * TILESIZE,
                coordinates.Y * TILESIZE
            );
        }
    }
}
