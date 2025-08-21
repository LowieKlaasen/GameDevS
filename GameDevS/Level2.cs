using GameDevS.Enemies.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class Level2 : GameScene
    {
        public Level2(ContentManager contentManager, SceneManager sceneManager, GraphicsDevice graphicsDevice) 
            : base(contentManager, sceneManager, graphicsDevice)
        { }

        public Level2(ContentManager contentManager, SceneManager sceneManager, GraphicsDevice graphicsDevice, Player player)
            : base(contentManager, sceneManager, graphicsDevice)
        {
            this.player = player;
        }

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

                //player = new Player(new Vector2(1 * TILESIZE, 5 * TILESIZE), 1f, sprites, 22, 21, 48, 53, new PlayerController());
                player = new Player(new Vector2(188 * TILESIZE, 7 * TILESIZE), 1f, sprites, 22, 21, 48, 53, new PlayerController());

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
            Texture2D textureExclusion = ContentManager.Load<Texture2D>("map/ExclusionZone");

            map = new TileMap2(textureExclusion, TILESIZE, 32, 9);
            map.LoadMap("../../../Data/Level2.csv");

            foreach (var tile in map.GetCollidables())
            {
                collisionManager.Register(tile);
            }

            goalZone = new GoalZone(new Vector2(194 * TILESIZE, 6 * TILESIZE), 2f, 3 * TILESIZE, 3 * TILESIZE);
            Texture2D goalTexture = ContentManager.Load<Texture2D>("map/goalZone/ddoor");
            SpriteSheet goalSheet = new SpriteSheet(goalTexture, 1, 1);
            Animation2 goalAnimation = new Animation2(goalSheet);
            goalZone.AddAnimation(AnimationState.IDLE, goalAnimation);
        }

        protected override void LoadEnemies()
        {
            IEnemyFactory enemyFactory = new TempleEnemyFactory(ContentManager, collisionManager, TILESIZE);

            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(13, 9), player));

            // ToDo: Add extra enemies

            //CreatePassivePatrolEnemy(new Vector2(54, 4));

            foreach (var sprite in sprites)
            {
                collisionManager.Register(sprite);
            }
        }

        protected override void LoadCollectibles()
        {
            // ToDo: add coins
            CreateCoin(new Vector2(20, 9));
            CreateCoin(new Vector2(22, 8));
            CreateCoin(new Vector2(25, 7));
        }

        protected override void LoadBackground()
        {
            scrollingBackground = new ScrollingBackground(camera);

            float parallaxCounter = 0.1f;
            for (int i = 1; i < 6; i++)
            {
                string fileName = "background/temple/plx-" + i.ToString();

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
            ServiceLocator.AudioService.PlayMusic("templeBG");
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
