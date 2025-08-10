using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace GameDevS
{
    internal class TileMap
    {
        public int[,] Tiles;
        public int TILESIZE = 48;
        private Texture2D tileset;
        private int tilesetColumns;
        private int textureTileSize;

        public TileMap(string filePath, Texture2D tileset, int textureTileSize)
        {
            this.tileset = tileset;
            this.textureTileSize = textureTileSize;

            string[] lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int cols = lines[0].Split(',').Length;
            Tiles = new int[rows, cols];

            for (int y = 0; y < rows; y++)
            {
                string[] numbers = lines[y].Split(',');
                for (int x = 0; x < cols; x++)
                {
                    Tiles[y, x] = int.Parse(numbers[x]);
                }
            }

            tilesetColumns = tileset.Width / textureTileSize;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Tiles.GetLength(0); y++)
            {
                for (int x = 0; x < Tiles.GetLength(1); x++)
                {
                    int tileID = Tiles[y, x];
                    if (tileID < 0)
                    {
                        continue;
                    }

                    int srcX = (tileID % tilesetColumns) * textureTileSize;
                    int srcY = (tileID / tilesetColumns) * textureTileSize;

                    Rectangle sourceRect = new Rectangle(
                        srcX,
                        srcY,
                        textureTileSize,
                        textureTileSize
                    );
                    Rectangle destRect = new Rectangle(
                        x * TILESIZE,
                        y * TILESIZE,
                        TILESIZE,
                        TILESIZE
                    );

                    spriteBatch.Draw(
                        tileset,
                        destRect,
                        sourceRect,
                        Color.White
                    );
                }
            }
        }
    }
}
