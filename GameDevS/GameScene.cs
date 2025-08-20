using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameDevS
{
    public class GameScene : IScene
    {
        public ContentManager contentManager;
        public SceneManager sceneManager;

        private Texture2D _heroTextureIdle;
        private Texture2D _heroTextureRunning;
        private Texture2D _heroTextureJumping;
        private Texture2D _heroTextureDying;
        private Texture2D _heroTextureHurting;

        List<Sprite> sprites;
        Player player;

        private TileMap2 map;
        private Texture2D textureSwamp;

        private CollisionManager2 collisionManager;
        private MovementManager movementManager;

        public GraphicsDevice graphicsDevice;
        private Camera2D camera;

        //private PlayerController playerController;

        private AnimationUpdater animationUpdater;

        //private PassivePatrolController passivePatrolController;

        public bool IsPaused;
        private PauseMenu pauseMenu;

        public bool GameOver;
        private GameOverMenu gameOverMenu;

        private ScrollingBackground scrollingBackground;

        private List<ICollectible> collectibles;

        private GameOverlay hud;

        public GameScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDevice graphicsDevice)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;

            this.graphicsDevice = graphicsDevice;

            camera = new Camera2D(graphicsDevice.Viewport);

            //playerController = new PlayerController();

            //passivePatrolController = new PassivePatrolController();

            pauseMenu = new PauseMenu(graphicsDevice, this, contentManager);

            gameOverMenu = new GameOverMenu(graphicsDevice, this, contentManager);
        }

        public void Load()
        {
            collisionManager = new CollisionManager2();

            _heroTextureIdle = contentManager.Load<Texture2D>("RogueIdle_Cropped");
            _heroTextureRunning = contentManager.Load<Texture2D>("RogueRunning_Cropped");
            _heroTextureJumping = contentManager.Load<Texture2D>("RogieJump_Cropped");
            _heroTextureDying = contentManager.Load<Texture2D>("RogueDying_Cropped");
            _heroTextureHurting = contentManager.Load<Texture2D>("RogueHurt_Cropped");

            SpriteSheet idleSheet = new SpriteSheet(_heroTextureIdle, 17, 1);
            SpriteSheet runningSheet = new SpriteSheet(_heroTextureRunning, 4, 2);
            SpriteSheet jumpSheet = new SpriteSheet(_heroTextureJumping, 7, 1);
            SpriteSheet dieSheet = new SpriteSheet(_heroTextureDying, 5, 2);
            SpriteSheet hurtSheet = new SpriteSheet(_heroTextureHurting, 4, 1);

            sprites = new List<Sprite>();

            player = new Player(Vector2.Zero, 1f, sprites, 22, 21, 48, 53, new PlayerController());

            Animation2 idleAnimation = new Animation2(idleSheet);
            player.AddAnimation(AnimationState.IDLE, idleAnimation);

            Animation2 runningAnimation = new Animation2(runningSheet);
            player.AddAnimation(AnimationState.RUNNING, runningAnimation);

            Animation2 jumpAnimation = new Animation2(jumpSheet);
            player.AddAnimation(AnimationState.JUMPING, jumpAnimation);

            Animation2 dieAnimation = new Animation2(dieSheet);
            player.AddAnimation(AnimationState.DYING, dieAnimation);

            Animation2 hurtAnimation = new Animation2(hurtSheet);
            player.AddAnimation(AnimationState.HURTING, hurtAnimation);

            CreateStationaryEnemy(new Vector2(33 * 54, 3 * 54));

            CreatePassivePatrolEnemy(new Vector2(16 * 54, 7 * 54));

            CreatePassivePatrolEnemy(new Vector2(46 * 54, 9 * 54));
            CreatePassivePatrolEnemy(new Vector2(50 * 54, 9 * 54));
            CreatePassivePatrolEnemy(new Vector2(54 * 54, 9 * 54));
            CreatePassivePatrolEnemy(new Vector2(60 * 54, 9 * 54));

            CreateActivePatrolEnemy(new Vector2(22 * 54, 6 * 54), player);
            CreateActivePatrolEnemy(new Vector2(59 * 54, 3 * 54), player);
            CreateActivePatrolEnemy(new Vector2(88 * 54, 4 * 54), player);

            sprites.Add(player);

            textureSwamp = contentManager.Load<Texture2D>("map/swamp_tileset");

            //map = new TileMap("../../../Data/simple.csv", textureSwamp, 32);
            map = new TileMap2(textureSwamp, 54, 32, 10);
            //map.LoadMap("../../../Data/simple.csv");
            //map.LoadMap("../../../Data/Test_FullScreen.csv");
            //map.LoadMap("../../../Data/Test_MovingMap.csv");
            map.LoadMap("../../../Data/Level1_TempEnding.csv");


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

            Texture2D bgTexture = contentManager.Load<Texture2D>("background/jungle/plx-5");
            scrollingBackground = new ScrollingBackground(camera);

            float parallaxCounter = 0.1f;
            for (int i = 1; i < 6; i++) 
            { 
                string fileName = "background/jungle/plx-" + i.ToString();

                scrollingBackground.AddLayer(contentManager.Load<Texture2D>(fileName), parallaxCounter);
                parallaxCounter += 0.2f;
            }

            collectibles = new List<ICollectible>();

            CreateCoin(new Vector2(9 * 54, 6 * 54));
            CreateCoin(new Vector2(10 * 54, 6 * 54));
            CreateCoin(new Vector2(11 * 54, 6 * 54));

            CreateCoin(new Vector2(24 * 54, 6 * 54));
            CreateCoin(new Vector2(25 * 54, 6 * 54));
            CreateCoin(new Vector2(26 * 54, 6 * 54));

            CreateCoin(new Vector2(72 * 54, 1 * 54));
            CreateCoin(new Vector2(73 * 54, 1 * 54));
            CreateCoin(new Vector2(74 * 54, 1 * 54));
            CreateCoin(new Vector2(75 * 54, 1 * 54));
            CreateCoin(new Vector2(76 * 54, 1 * 54));

            SpriteFont hudFont = contentManager.Load<SpriteFont>("fonts/PixelEmulator");
            Texture2D coinTexture = contentManager.Load<Texture2D>("hud/Gold_2");
            Texture2D fullHeart = contentManager.Load<Texture2D>("hud/hearts/hearth_full");
            Texture2D emptyHeart = contentManager.Load<Texture2D>("hud/hearts/hearth_darkRed");

            hud = new GameOverlay(hudFont, player, coinTexture, fullHeart, emptyHeart);
        }

        public void Update(GameTime gameTime)
        {
            if (!IsPaused && Keyboard.GetState().IsKeyDown(Keys.P))
            {
                Debug.WriteLine("Pause pressed");
                IsPaused = true;
                Debug.WriteLine("IsPaused = " + IsPaused);
            }
            //if (!GameOver && Keyboard.GetState().IsKeyDown(Keys.G))
            //{
            //    Debug.WriteLine("Game over pressed");
            //    GameOver = true;
            //    Debug.WriteLine("GameOver = " + GameOver);
            //}

            //if (!player.Health.IsAlive)
            //{
            //    GameOver = true;
            //}
            player.OnDeathAnimationFinished += () =>
            {
                GameOver = true;
            };

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (player.IsDying)
            {
                dt /= 3;
            }

            if (dt > 1 && !player.Health.IsAlive)
            {
                GameOver = true;
            }

            if (IsPaused)
            {
                dt = 0;

                pauseMenu.Update(gameTime);
                return;
            }
            if (GameOver)
            {
                dt = 0;
                gameOverMenu.Update(gameTime);
                return;
            }

            camera.Follow(player.Position, 0, 200 * 54, 0, 15 * 54);

            foreach (var sprite in sprites)
            {
                sprite.Update(dt);

                //if (sprite is not Player)
                //{
                //    movementManager.Move(sprite, sprite.MovementController, dt);
                //}
                movementManager.Move(sprite, dt);
            }

            //KeyboardState keyboardState = Keyboard.GetState();

            //player.MovementController.UpdateKeyboard(keyboardState);

            //movementManager.Move(player, dt);

            //animationUpdater.UpdateAnimation(player);
            foreach (var sprite in sprites)
            {
                animationUpdater.UpdateAnimation(sprite);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                sceneManager.AddScene(new ExitScene(contentManager));
            }

            List<Sprite> toRemove = new List<Sprite>();
            foreach(var sprite in sprites)
            {
                if (sprite is Enemy enemy && !enemy.IsAlive)
                {
                    collisionManager.Remove(enemy);
                    //sprites.Remove(sprite);
                    toRemove.Add(sprite);
                }
            }
            foreach (var sprite in toRemove)
            {
                sprites.Remove(sprite);
            }

            foreach (ICollectible collectible in collectibles)
            {
                if (collectible is Coin coin && !coin.Collected)
                {
                    if (collectible is AnimatedEntity animatedEntity)
                    {
                        animatedEntity.Update(dt);
                    }

                    if (collectible.Bounds.Intersects(player.HitBox))
                    {
                        collectible.OnCollect(player);
                    }
                }
            }

            if (player.MovementController is PlayerController playerController && playerController.CheckDeathByFalling(player, camera, graphicsDevice.Viewport.Height))
            {
                GameOver = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            scrollingBackground.Draw(spriteBatch);

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

            foreach (ICollectible collectible in collectibles)
            {
                if (collectible is Coin coin && coin.Collected)
                {
                    continue;
                }

                if (collectible is AnimatedEntity animatedEntity) 
                {
                    animatedEntity.Draw(spriteBatch);
                }

                DebugDraw.DrawHollowRectangle(spriteBatch, collectible.Bounds, Color.Purple);
            }

            spriteBatch.End();

            hud.Draw(spriteBatch);

            if (IsPaused)
            {
                spriteBatch.Begin();
                pauseMenu.Draw(spriteBatch);
                spriteBatch.End();
            }

            if (GameOver) 
            {
                spriteBatch.Begin();
                gameOverMenu.Draw(spriteBatch);
                spriteBatch.End();
            }

        }

        public void Restart()
        {
            Load();

            IsPaused = false;
            GameOver = false;
        }

        #region Private Methods

        private void CreatePassivePatrolEnemy(Vector2 startPosition)
        {
            Texture2D walkTexture = contentManager.Load<Texture2D>("enemies/goblin/golem_walking");
            SpriteSheet walkSheet = new SpriteSheet(walkTexture, 8, 3);
            Animation2 walkAnimation = new Animation2(walkSheet);

            Texture2D idleTexture = contentManager.Load<Texture2D>("enemies/goblin/goblin_idle");
            SpriteSheet idleSheet = new SpriteSheet(idleTexture, 6, 3);
            Animation2 idleAnimation = new Animation2(idleSheet);

            Texture2D dyingTexture = contentManager.Load<Texture2D>("enemies/goblin/goblin_dying");
            SpriteSheet dyingSheet = new SpriteSheet(dyingTexture, 5, 3);
            Animation2 dyingAnimatoin = new Animation2(dyingSheet);

            PassivePatrolEnemy patrolEnemy = new PassivePatrolEnemy(startPosition, 0.1f, 23, 22, 41, 54, new PassivePatrolController());

            patrolEnemy.AddAnimation(AnimationState.IDLE, idleAnimation);
            patrolEnemy.AddAnimation(AnimationState.RUNNING, walkAnimation);
            patrolEnemy.AddAnimation(AnimationState.DYING, dyingAnimatoin);

            sprites.Add(patrolEnemy);
        }

        private void CreateActivePatrolEnemy(Vector2 startPosition, Player player)
        {
            Texture2D walkTexture = contentManager.Load<Texture2D>("enemies/ogre/ogre_walking");
            SpriteSheet walkSheet = new SpriteSheet(walkTexture, 8, 3);
            Animation2 walkAnimation = new Animation2(walkSheet);

            Texture2D idleTexture = contentManager.Load<Texture2D>("enemies/ogre/ogre_idle");
            SpriteSheet idleSheet = new SpriteSheet(idleTexture, 6, 3);
            Animation2 idleAnimation = new Animation2(idleSheet);

            Texture2D dyingTexture = contentManager.Load<Texture2D>("enemies/ogre/ogre_dying");
            SpriteSheet dyingSheet = new SpriteSheet(dyingTexture, 5, 3);
            Animation2 dyingAnimation = new Animation2(dyingSheet);

            Texture2D jumpTexture = contentManager.Load<Texture2D>("enemies/ogre/ogre_jumpSequene");
            SpriteSheet jumpSheet = new SpriteSheet(jumpTexture, 6, 2);
            Animation2 jumpAnimation = new Animation2(jumpSheet);

            PassivePatrolEnemy patrolEnemy = new PassivePatrolEnemy(startPosition, 0.1f, 23, 22, 41, 54, new ActivePatrolController(player, collisionManager));

            patrolEnemy.AddAnimation(AnimationState.IDLE, idleAnimation);
            patrolEnemy.AddAnimation(AnimationState.RUNNING, walkAnimation);
            patrolEnemy.AddAnimation(AnimationState.DYING, dyingAnimation);
            patrolEnemy.AddAnimation(AnimationState.JUMPING, jumpAnimation);

            sprites.Add(patrolEnemy);
        }

        private void CreateStationaryEnemy(Vector2 startPosition)
        {
            Texture2D walkTexture = contentManager.Load<Texture2D>("enemies/orc/orc_walking");
            SpriteSheet walkSheet = new SpriteSheet(walkTexture, 8, 3);
            Animation2 walkAnimation = new Animation2(walkSheet);

            Texture2D idleTexture = contentManager.Load<Texture2D>("enemies/orc/orc_idle");
            SpriteSheet idleSheet = new SpriteSheet(idleTexture, 6, 3);
            Animation2 idleAnimation = new Animation2(idleSheet);

            Texture2D dyingTexture = contentManager.Load<Texture2D>("enemies/orc/orc_dying");
            SpriteSheet dyingSheet = new SpriteSheet(dyingTexture, 5, 3);
            Animation2 dyingAnimation = new Animation2(dyingSheet);

            Texture2D chargingTexture = contentManager.Load<Texture2D>("enemies/orc/orc_running");
            SpriteSheet chargingSheet = new SpriteSheet(chargingTexture, 4, 3);
            Animation2 chargingAnimation = new Animation2(chargingSheet);

            StationaryEnemy stationaryEnemy = new StationaryEnemy(startPosition, 0.1f, 23, 22, 41, 54, new StationaryController());

            stationaryEnemy.AddAnimation(AnimationState.IDLE, idleAnimation);
            stationaryEnemy.AddAnimation(AnimationState.RUNNING, walkAnimation);
            stationaryEnemy.AddAnimation(AnimationState.DYING, dyingAnimation);

            sprites.Add(stationaryEnemy);
        }

        private void CreateCoin(Vector2 position)
        {
            Texture2D texture = contentManager.Load<Texture2D>("collectibles/goldenCoin_one");
            SpriteSheet spriteSheet = new SpriteSheet(texture, 5, 2);
            Animation2 animation = new Animation2(spriteSheet);

            Coin coin = new Coin(position, 0.06f, 36, 36);

            coin.AddAnimation(AnimationState.IDLE, animation);

            collectibles.Add(coin);
        }

        #endregion
    }
}
