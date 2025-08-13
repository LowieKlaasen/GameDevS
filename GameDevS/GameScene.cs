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

        private Texture2D _heroTexture;
        private Texture2D _enemyTexture;

        private Texture2D _heroTextureIdle;
        private Texture2D _heroTextureRunning;
        private Texture2D _heroTextureJumping;


        List<Sprite> sprites;
        Player player;

        private TileMap2 map;
        private Texture2D textureSwamp;

        private CollisionManager2 collisionManager;
        private MovementManager movementManager;

        public GameScene(ContentManager contentManager, SceneManager sceneManager)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
        }

        public void Load()
        {
            #region Game1

            _heroTexture = contentManager.Load<Texture2D>("RogueRunning_Cropped");
            _enemyTexture = contentManager.Load<Texture2D>("goblin_single");

            _heroTextureIdle = contentManager.Load<Texture2D>("RogueIdle_Cropped");
            _heroTextureRunning = contentManager.Load<Texture2D>("RogueRunning_Cropped");
            _heroTextureJumping = contentManager.Load<Texture2D>("RogieJump_Cropped");

            SpriteSheet idleSheet = new SpriteSheet(_heroTextureIdle, 17, 1);
            SpriteSheet runningSheet = new SpriteSheet(_heroTextureRunning, 4, 2);
            SpriteSheet jumpSheet = new SpriteSheet(_heroTextureJumping, 7, 1);

            sprites = new List<Sprite>();

            //sprites.Add(new Sprite(_enemyTexture, new Vector2(100, 100), 0.1f, 23, 22, 41, 54, 1, 1));
            //sprites.Add(new Sprite(_enemyTexture, new Vector2(400, 200), 0.1f, 23, 22, 41, 54, 1, 1));
            //sprites.Add(new Sprite(_enemyTexture, new Vector2(700, 300), 0.1f, 23, 22, 41, 54, 1, 1));

            player = new Player(_heroTexture, Vector2.Zero, 1f, sprites, 22, 21, 48, 53, 4, 2);

            Animation2 idleAnimation = new Animation2(idleSheet);
            player.AddAnimation(AnimationState.IDLE, idleAnimation);

            Animation2 runningAnimation = new Animation2(runningSheet);
            player.AddAnimation(AnimationState.RUNNING, runningAnimation);

            Animation2 jumpAnimation = new Animation2(jumpSheet);
            player.AddAnimation(AnimationState.JUMPING, jumpAnimation);

            sprites.Add(player);

            #endregion

            textureSwamp = contentManager.Load<Texture2D>("map/swamp_tileset");

            //map = new TileMap("../../../Data/simple.csv", textureSwamp, 32);
            map = new TileMap2(textureSwamp, 48, 32, 10);
            //map.LoadMap("../../../Data/simple.csv");
            map.LoadMap("../../../Data/Test_FullScreen.csv");

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
            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);
            }

            KeyboardState keyboardState = Keyboard.GetState();
            movementManager.Move(player, player, gameTime, keyboardState);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                sceneManager.AddScene(new ExitScene(contentManager));
            }    
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var sprite in sprites)
            {
                sprite.Draw(spriteBatch);

                // ToDo: Remove draw hollow rectangle
                DebugDraw.DrawHollowRectangle(spriteBatch, sprite.HitBox, Color.Red);
            }

            map.Draw(spriteBatch);

            // ToDo: Remove draw hollow rectangle
            foreach (var tile in map.GetCollidables())
            {
                DebugDraw.DrawHollowRectangle(spriteBatch, tile.HitBox, Color.Green);
            }

        }
    }
}
