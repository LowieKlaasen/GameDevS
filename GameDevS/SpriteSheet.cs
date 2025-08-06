using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class SpriteSheet
    {
        public Texture2D Texture;
        public int NumberOfWidthSprites;
        public int NumberOfHeightSprites;

        public SpriteSheet(Texture2D texture, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            Texture = texture;
            NumberOfWidthSprites = numberOfWidthSprites;
            NumberOfHeightSprites = numberOfHeightSprites;
        }
    }
}
