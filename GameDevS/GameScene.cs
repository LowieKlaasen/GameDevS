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

        private TileMap2 map;
        //private TileMap map;
        private Texture2D textureSwamp;

        private CollisionManager2 collisionManager;
        private MovementManager movementManager;

        public GameScene(ContentManager contentManager, SceneManager sceneManager)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;

            //collisionManager = new CollisionManager(); (Moved to Load)
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

            //map = new TileMap("../../../Data/simple.csv", textureSwamp, 32);
            map = new TileMap2(textureSwamp, 48, 32, 10);
            map.LoadMap("../../../Data/simple.csv");

            collisionManager = new CollisionManager2();

            foreach (var tile in map.GetCollidables())
            {
                collisionManager.Register(tile);
            }
            foreach (var sprite in sprites)
            {
                collisionManager.Register(sprite);
            }

            movementManager = new MovementManager(collisionManager);
        }

        public void Update(GameTime gameTime)
        {
            #region Game1

            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);
            }

            #endregion

            //collisionManager.ResolvePlayerTileCollision(player, map);

            KeyboardState keyboardState = Keyboard.GetState();
            movementManager.Move(player, player, gameTime, keyboardState);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                sceneManager.AddScene(new ExitScene(contentManager));
            }    
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            #region Game1

            foreach (var sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }

            #endregion

            map.Draw(spriteBatch);

        }
    }
}
