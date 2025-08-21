using GameDevS.Enemies.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class Level1 : GameScene
    {
        public Level1(ContentManager contentManager, SceneManager sceneManager, GraphicsDevice graphicsDevice) 
            : base(contentManager, sceneManager, graphicsDevice)
        { }

        protected override void LoadPlayer()
        {
            Texture2D _heroTextureIdle = ContentManager.Load<Texture2D>("RogueIdle_Cropped");
            Texture2D _heroTextureRunning = ContentManager.Load<Texture2D>("RogueRunning_Cropped");
            Texture2D _heroTextureJumping = ContentManager.Load<Texture2D>("RogieJump_Cropped");
            Texture2D _heroTextureDying = ContentManager.Load<Texture2D>("RogueDying_Cropped");
            Texture2D _heroTextureHurting = ContentManager.Load<Texture2D>("RogueHurt_Cropped");

            SpriteSheet idleSheet = new SpriteSheet(_heroTextureIdle, 17, 1);
            SpriteSheet runningSheet = new SpriteSheet(_heroTextureRunning, 4, 2);
            SpriteSheet jumpSheet = new SpriteSheet(_heroTextureJumping, 7, 1);
            SpriteSheet dieSheet = new SpriteSheet(_heroTextureDying, 5, 2);
            SpriteSheet hurtSheet = new SpriteSheet(_heroTextureHurting, 4, 1);

            //player = new Player(Vector2.Zero, 1f, sprites, 22, 21, 48, 53, new PlayerController());
            player = new Player(new Vector2(183 * TILESIZE, 6 * TILESIZE), 1f, sprites, 22, 21, 48, 53, new PlayerController());

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

            sprites.Add(player);
        }

        protected override void LoadMap()
        {
            Texture2D textureSwamp = ContentManager.Load<Texture2D>("map/swamp_tileset");

            map = new TileMap2(textureSwamp, TILESIZE, 32, 10);
            //map.LoadMap("../../../Data/Level1_TempEnding.csv");
            map.LoadMap("../../../Data/Level1_v2.csv");

            foreach (var tile in map.GetCollidables())
            {
                collisionManager.Register(tile);
            }

            goalZone = new GoalZone(new Vector2(194 * TILESIZE, 4 * TILESIZE), 1f, 5 * TILESIZE, 5 * TILESIZE);
            Texture2D goalTexture = ContentManager.Load<Texture2D>("map/goalZone/Ancient_Bas_Relief");
            SpriteSheet goalSheet = new SpriteSheet(goalTexture, 1, 1);
            Animation2 goalAnimation = new Animation2(goalSheet);
            goalZone.AddAnimation(AnimationState.IDLE, goalAnimation);
        }

        protected override void LoadEnemies()
        {
            IEnemyFactory enemyFactory = new JungleEnemyFactory(ContentManager, collisionManager, TILESIZE);

            sprites.Add(enemyFactory.CreateStationaryEnemy(new Vector2(33, 3)));

            sprites.Add(enemyFactory.CreatePassivePatrolEnemy(new Vector2(16, 7)));

            sprites.Add(enemyFactory.CreatePassivePatrolEnemy(new Vector2(46, 9)));
            sprites.Add(enemyFactory.CreatePassivePatrolEnemy(new Vector2(50, 9)));
            sprites.Add(enemyFactory.CreatePassivePatrolEnemy(new Vector2(54, 9)));
            sprites.Add(enemyFactory.CreatePassivePatrolEnemy(new Vector2(60, 9)));

            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(22, 6), player));
            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(59, 3), player));
            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(88, 4), player));

            foreach (var sprite in sprites)
            {
                collisionManager.Register(sprite);
            }
        }

        protected override void LoadCollectibles()
        {
            CreateCoin(new Vector2(9, 6));
            CreateCoin(new Vector2(10, 6));
            CreateCoin(new Vector2(11, 6));

            CreateCoin(new Vector2(24, 6));
            CreateCoin(new Vector2(25, 6));
            CreateCoin(new Vector2(26, 6));

            for (int x = 72; x <= 76; x++)
            {
                CreateCoin(new Vector2(x, 1));
            }

            for (int y = 11; y <= 12; y++)
            {
                for (int x = 137; x <= 151; x++)
                {
                    CreateCoin(new Vector2(x, y));
                }
            }

            CreateCoin(new Vector2(163, 0));
            CreateCoin(new Vector2(165, 0));
            CreateCoin(new Vector2(162, 1));
            CreateCoin(new Vector2(164, 1));
            CreateCoin(new Vector2(166, 1));
        }

        protected override void LoadBackground()
        {
            scrollingBackground = new ScrollingBackground(camera);

            float parallaxCounter = 0.1f;
            for (int i = 1; i < 6; i++)
            {
                string fileName = "background/jungle/plx-" + i.ToString();

                scrollingBackground.AddLayer(ContentManager.Load<Texture2D>(fileName), parallaxCounter);
                parallaxCounter += 0.2f;
            }
        }

        protected override void LoadHUD()
        {
            SpriteFont hudFont = ContentManager.Load<SpriteFont>("fonts/PixelEmulator");
            Texture2D coinTexture = ContentManager.Load<Texture2D>("hud/Gold_2");
            Texture2D fullHeart = ContentManager.Load<Texture2D>("hud/hearts/hearth_full");
            Texture2D emptyHeart = ContentManager.Load<Texture2D>("hud/hearts/hearth_darkRed");

            hud = new GameOverlay(hudFont, player, coinTexture, fullHeart, emptyHeart);
        }

        protected override void LoadMusic()
        {
            ServiceLocator.AudioService.PlayMusic("jungleBG");
        }


        #region Private Methods

        private void CreateCoin(Vector2 coordinates)
        {
            Vector2 position = new Vector2(
                coordinates.X * TILESIZE,
                coordinates.Y * TILESIZE
            );

            Texture2D texture = ContentManager.Load<Texture2D>("collectibles/goldenCoin_one");
            SpriteSheet spriteSheet = new SpriteSheet(texture, 5, 2);
            Animation2 animation = new Animation2(spriteSheet);

            Coin coin = new Coin(position, 0.06f, 36, 36);

            coin.AddAnimation(AnimationState.IDLE, animation);

            collectibles.Add(coin);
        }

        #endregion
    }
}
