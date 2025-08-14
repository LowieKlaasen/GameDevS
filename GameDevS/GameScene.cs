using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

        private GraphicsDevice graphicsDevice;
        private Camera2D camera;

        private PlayerController playerController;

        private AnimationUpdater animationUpdater;

        private PassivePatrolController passivePatrolController;

        public GameScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDevice graphicsDevice)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;

            this.graphicsDevice = graphicsDevice;

            camera = new Camera2D(graphicsDevice.Viewport);

            playerController = new PlayerController();

            passivePatrolController = new PassivePatrolController();
        }

        public void Load()
        {
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

            CreatePassivePatrolEnemy(new Vector2(16 * 54, 7 * 54));

            player = new Player(_heroTexture, Vector2.Zero, 1f, sprites, 22, 21, 48, 53, 4, 2);

            Animation2 idleAnimation = new Animation2(idleSheet);
            player.AddAnimation(AnimationState.IDLE, idleAnimation);

            Animation2 runningAnimation = new Animation2(runningSheet);
            player.AddAnimation(AnimationState.RUNNING, runningAnimation);

            Animation2 jumpAnimation = new Animation2(jumpSheet);
            player.AddAnimation(AnimationState.JUMPING, jumpAnimation);

            sprites.Add(player);

            textureSwamp = contentManager.Load<Texture2D>("map/swamp_tileset");

            //map = new TileMap("../../../Data/simple.csv", textureSwamp, 32);
            map = new TileMap2(textureSwamp, 54, 32, 10);
            //map.LoadMap("../../../Data/simple.csv");
            //map.LoadMap("../../../Data/Test_FullScreen.csv");
            //map.LoadMap("../../../Data/Test_MovingMap.csv");
            map.LoadMap("../../../Data/Level1_TempEnding.csv");

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

            animationUpdater = new AnimationUpdater();

            //ServiceLocator.AudioService.PlayMusic("jungleBG");
        }

        public void Update(GameTime gameTime)
        {
            //camera.Follow(player.Position, 0, 30 * 54, 0, 15 * 54);
            camera.Follow(player.Position, 0, 200 * 54, 0, 15 * 54);

            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);

                if (sprite is not Player)
                {
                    movementManager.Move(sprite, passivePatrolController, gameTime);
                }
            }

            KeyboardState keyboardState = Keyboard.GetState();

            playerController.UpdateKeyboard(keyboardState);

            movementManager.Move(player, playerController, gameTime);

            //animationUpdater.UpdateAnimation(player);
            foreach (var sprite in sprites)
            {
                animationUpdater.UpdateAnimation(sprite);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                sceneManager.AddScene(new ExitScene(contentManager));
            }    
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: camera.Transform);

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

            spriteBatch.End();

        }

        #region Private Methods

        private void CreatePassivePatrolEnemy(Vector2 startPosition)
        {
            Texture2D walkTexture = contentManager.Load<Texture2D>("enemies/goblin/golem_walking");
            SpriteSheet walkSprite = new SpriteSheet(walkTexture, 8, 3);
            Animation2 walkAnimation = new Animation2(walkSprite);

            Texture2D idleTexture = contentManager.Load<Texture2D>("enemies/goblin/goblin_idle");
            SpriteSheet idleSheet = new SpriteSheet(idleTexture, 6, 3);
            Animation2 idleAnimation = new Animation2(idleSheet);

            PassivePatrolEnemy patrolEnemy = new PassivePatrolEnemy(idleTexture, startPosition, 0.1f, 23, 22, 41, 54, 6, 3);

            patrolEnemy.AddAnimation(AnimationState.IDLE, idleAnimation);
            patrolEnemy.AddAnimation(AnimationState.RUNNING, walkAnimation);

            sprites.Add(patrolEnemy);
        }

        #endregion
    }
}
