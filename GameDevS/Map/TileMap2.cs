using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace GameDevS.Map
{
    public class TileMap2
    {
        private List<Tile2> map;
        private int displayTileSize;
        private Texture2D texture;
        private int textureTileSize;
        private int amountOfWidthTiles;

        public TileMap2(Texture2D texture, int displayTileSize, int textureTileSize, int amountOfWidthTiles)
        {
            map = new List<Tile2>();
            this.displayTileSize = displayTileSize;
            this.texture = texture;
            this.textureTileSize = textureTileSize;
            this.amountOfWidthTiles = amountOfWidthTiles;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            foreach (Tile2 tile in map)
            {
                spriteBatch.Draw(tile.Texture, tile.DestRect, tile.SrcRect, Color.White);
            }
        }

        public List<Tile2> GetCollidables()
        {
            return map;
        }

        public void LoadMap(string filePath) 
        {
            using (var reader = new StreamReader(filePath))
            {
                int y = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    var numbers = line.Split(',');

                    int x = 0;
                    foreach (var number in numbers)
                    {
                        if (int.TryParse(number, out int result))
                        {
                            if (result >= 0)
                            {

                                Rectangle srcRect = new Rectangle(
                                    result % amountOfWidthTiles * textureTileSize,
                                    result / amountOfWidthTiles * textureTileSize,
                                    textureTileSize,
                                    textureTileSize
                                );

                                Rectangle destRect = new Rectangle(
                                    x * displayTileSize,
                                    y * displayTileSize,
                                    displayTileSize,
                                    displayTileSize
                                );

                                Tile2 tile = new Tile2(texture, srcRect, destRect);

                                map.Add(tile);
                            }
                        }
                        x++;
                    }
                    y++;
                }
            }
        }
    }
}
