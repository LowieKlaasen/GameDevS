using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class Tile2
    {
        public Texture2D Texture;
        public Rectangle SrcRect;
        public Rectangle DestRect;

        private int TILESIZE;
        // ToDo: Check if Solid property is used
        public bool Solid;

        public Tile2(Texture2D texture, Rectangle srcRect, Rectangle destRect, bool solid)
        {
            Texture = texture;
            SrcRect = srcRect;
            DestRect = destRect;
            Solid = solid;
        }
        public Tile2(Texture2D texture, Rectangle srcRect, Rectangle destRect)
        {
            Texture = texture;
            SrcRect = srcRect;
            DestRect = destRect;

            Solid = true;
        }
    }
}
