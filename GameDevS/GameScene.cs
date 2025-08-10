using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GameDevS
{
    public class GameScene : IScene
    {
        private ContentManager contentManager;
        private SceneManager sceneManager;

        #region Game1

        private Texture2D _heroTexture;
        private Texture2D _enemyTexture;

        List<Sprite> sprites;
        Player player;

        #endregion

        //private Dictionary<Vector2, int> tilemap;
        private TileMap map;
        private List<Tile> textureStore;
        private Texture2D textureSwamp;

        private int TILESIZE = 48;
        private List<Rectangle> intersections;

        public GameScene(ContentManager contentManager, SceneManager sceneManager)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;

            intersections = new List<Rectangle>();
        }

        public void Load()
        {
            #region Game1

            _heroTexture = contentManager.Load<Texture2D>("RogueRunning_Cropped");
            _enemyTexture = contentManager.Load<Texture2D>("goblin_single");

            sprites = new List<Sprite>();

            sprites.Add(new Sprite(_enemyTexture, new Vector2(100, 100), 0.1f, 23, 22, 41, 54, 1, 1));
            sprites.Add(new Sprite(_enemyTexture, new Vector2(400, 200), 0.1f, 23, 22, 41, 54, 1, 1));
            sprites.Add(new Sprite(_enemyTexture, new Vector2(700, 300), 0.1f, 23, 22, 41, 54, 1, 1));

            player = new Player(_heroTexture, Vector2.Zero, 1f, sprites, 22, 21, 48, 53, 4, 2);

            sprites.Add(player);

            #endregion

            textureSwamp = contentManager.Load<Texture2D>("map/swamp_tileset");

            //tilemap = LoadMap("../../../Data/Try_Fixed.csv");
            //tilemap = LoadMap("../../../Data/simple.csv");
            //textureStore = loadTiles(32, 32, 10, 6);


            //textureStore = loadTiles(textureSwamp, 32, 32, 10, 6);

            map = new TileMap("../../../Data/simple.csv", textureSwamp, 32);
        }

        public void Update(GameTime gameTime)
        {
            #region Game1

            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);
            }

            #endregion

            //var horizontalTiles = GetIntersectingTilesHorizontal(player.Rectangle);
            //foreach (var index in horizontalTiles)
            //{
            //    if (tilemap.TryGetValue(index, out int _value))
            //    {
            //        Debug.WriteLine("intersecting horizontally");
            //    }
            //}

            //var verticalTiles = GetIntersectingTilesVertical(player.Rectangle);
            //foreach (var index in verticalTiles)
            //{
            //    if (tilemap.TryGetValue(index, out int _value))
            //    {
            //        Debug.WriteLine("intersecting vertically");
            //    }
            //}

            //List<Vector2> intersectingTiles = GetIntersectingTileIndices(player.Rectangle);
            //foreach (var index in intersectingTiles)
            //{
            //    if (tilemap.TryGetValue(index, out int _value))
            //    {
            //        Debug.WriteLine("intersecting");
            //    }
            //}

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                sceneManager.AddScene(new ExitScene(contentManager));
            }    
        }

        //public List<Vector2> GetIntersectingTileIndices(Rectangle target)
        //{
        //    List<Vector2> indices = new List<Vector2>();

        //    int startX = target.Left / TILESIZE;
        //    int endX = (target.Right - 1) / TILESIZE;
        //    int startY = target.Top / TILESIZE;
        //    int endY = (target.Bottom - 1) / TILESIZE;

        //    for (int x = startX; x <= endX; x++)
        //    {
        //        for (int y = startY; y <= endY; y++)
        //        {
        //            indices.Add(new Vector2(x, y));
        //        }
        //    }

        //    return indices;
        //}

        //public List<Vector2> GetIntersectingTilesHorizontal(Rectangle target)
        //{
        //    List<Vector2> indices = new List<Vector2>();
        //    int startX = target.Left / TILESIZE;
        //    int endX = (target.Right - 1) / TILESIZE;
        //    int topY = target.Top / TILESIZE;
        //    int bottomY = (target.Bottom - 1) / TILESIZE;

        //    // Top edge
        //    for (int x = startX; x <= endX; x++)
        //        indices.Add(new Vector2(x, topY));

        //    // Bottom edge
        //    if (bottomY != topY)
        //    {
        //        for (int x = startX; x <= endX; x++)
        //            indices.Add(new Vector2(x, bottomY));
        //    }

        //    return indices;
        //}

        //public List<Vector2> GetIntersectingTilesVertical(Rectangle target)
        //{
        //    List<Vector2> indices = new List<Vector2>();
        //    int leftX = target.Left / TILESIZE;
        //    int rightX = (target.Right - 1) / TILESIZE;
        //    int startY = target.Top / TILESIZE;
        //    int endY = (target.Bottom - 1) / TILESIZE;

        //    // Left edge
        //    for (int y = startY; y <= endY; y++)
        //        indices.Add(new Vector2(leftX, y));

        //    // Right edge
        //    if (rightX != leftX)
        //    {
        //        for (int y = startY; y <= endY; y++)
        //            indices.Add(new Vector2(rightX, y));
        //    }

        //    return indices;
        //}

        public void Draw(SpriteBatch spriteBatch)
        {

            #region Game1

            foreach (var sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }

            #endregion

            map.Draw(spriteBatch);

            //foreach (var tile in tilemap)
            //{
            //    Rectangle dest = new Rectangle(
            //        (int)tile.Key.X * TILESIZE,
            //        (int)tile.Key.Y * TILESIZE,
            //        TILESIZE,
            //        TILESIZE
            //    );

            //    Rectangle source = textureStore[tile.Value];

            //    spriteBatch.Draw(textureSwamp, dest, source, Color.White);
            //}

        }

        //private Dictionary<Vector2, int> LoadMap(string filepath)
        //{
        //    Dictionary<Vector2, int> result = new Dictionary<Vector2, int>();

        //    StreamReader reader = new StreamReader(filepath);

        //    int y = 0;
        //    string line;
        //    while ((line = reader.ReadLine()) != null)
        //    {
        //        string[] items = line.Split(',');

        //        for (int x = 0; x < items.Length; x++)
        //        {
        //            if (int.TryParse(items[x], out int value))
        //            {
        //                if (value >= 0)
        //                {
        //                    result[new Vector2(x, y)] = value;
        //                }
        //            }
        //        }

        //        y++;
        //    }

        //    return result;
        //}

        //private List<Rectangle> loadTiles(int tileWidth, int tileHeight, int amountWidth, int amountHeight)
        //{
        //    List<Rectangle> result = new List<Rectangle>();

        //    for (int y = 0; y < amountHeight; y++)
        //    {
        //        for (int x = 0; x < amountWidth; x++)
        //        {
        //            result.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
        //        }
        //    }

        //    return result;
        //}

        //private List<Tile> loadTiles(Texture2D texture, int tileWidth, int tileHeight, int amountWidth, int amountHeight)
        //{
        //    List<Tile> result = new List<Tile>();

        //    for (int y = 0; y < amountHeight; y++)
        //    {
        //        for (int x = 0; x < amountWidth; x++)
        //        {
        //            result.Add(new Tile(texture, new Rectangle(
        //                x * TILESIZE,
        //                y * TILESIZE,
        //                tileWidth,
        //                tileHeight
        //            )));
        //        }
        //    }

        //    return result;
        //}
    }
}
