using GameDevS.Animations;
using GameDevS.Entities;
using GameDevS.Entities.Collectibles.Factories;
using GameDevS.Entities.Enemies.Factories;
using GameDevS.Entities.PlayerMap;
using GameDevS.Graphics;
using GameDevS.Map;
using GameDevS.Movement.Controllers;
using GameDevS.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS.Scenes.Levels
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

                player = new Player(new Vector2(1 * TILESIZE, 5 * TILESIZE), 1f, 22, 21, 48, 53, new PlayerController());

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

            sprites.Add(enemyFactory.CreateStationaryEnemy(new Vector2(58, 3)));
            sprites.Add(enemyFactory.CreateStationaryEnemy(new Vector2(60, 3)));
            sprites.Add(enemyFactory.CreateStationaryEnemy(new Vector2(75, 5)));
            sprites.Add(enemyFactory.CreateStationaryEnemy(new Vector2(153, 10)));
            sprites.Add(enemyFactory.CreateStationaryEnemy(new Vector2(157, 4)));
            sprites.Add(enemyFactory.CreateStationaryEnemy(new Vector2(160, 10)));

            sprites.Add(enemyFactory.CreatePassivePatrolEnemy(new Vector2(54, 2)));
            sprites.Add(enemyFactory.CreatePassivePatrolEnemy(new Vector2(81, 6)));
            sprites.Add(enemyFactory.CreatePassivePatrolEnemy(new Vector2(84, 6)));
            sprites.Add(enemyFactory.CreatePassivePatrolEnemy(new Vector2(86, 6)));
            sprites.Add(enemyFactory.CreatePassivePatrolEnemy(new Vector2(156, 10)));

            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(14, 9), player));
            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(37, 2), player));
            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(81, 3), player));
            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(91, 3), player));
            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(110, 12), player));
            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(123, 11), player));
            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(148, 4), player));
            sprites.Add(enemyFactory.CreateActivePatrolEnemy(new Vector2(171, 2), player));

            foreach (var sprite in sprites)
            {
                collisionManager.Register(sprite);
            }
        }

        protected override void LoadCollectibles()
        {
            ICollectibleFactory collectibleFactory = new CollectibleFactory(ContentManager, TILESIZE);

            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(20, 9)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(22, 8)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(25, 7)));

            for (int x = 35; x <= 38; x++)
            {
                collectibles.Add(collectibleFactory.CreateCoin(new Vector2(x, 12)));
            }

            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(50, 1)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(51, 1)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(50, 2)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(51, 2)));

            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(64, 4)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(65, 3)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(66, 3)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(67, 4)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(68, 3)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(69, 3)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(70, 4)));

            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(96, 4)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(100, 4)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(98, 6)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(96, 8)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(100, 8)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(98, 10)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(96, 12)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(100, 12)));

            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(116, 12)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(117, 11)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(118, 10)));
            collectibles.Add(collectibleFactory.CreateCoin(new Vector2(119, 11)));

            for (int x = 129; x <= 133; x++)
            {
                collectibles.Add(collectibleFactory.CreateCoin(new Vector2(x, 9)));
            }

            for (int x = 139; x <= 141; x++)
            {
                collectibles.Add(collectibleFactory.CreateCoin(new Vector2(x, 4)));
            }

            for (int x = 176; x <= 178; x++)
            {
                collectibles.Add(collectibleFactory.CreateCoin(new Vector2(x, 4)));
            }

            for (int x = 181; x <= 185; x++)
            {
                collectibles.Add(collectibleFactory.CreateCoin(new Vector2(x, 8)));
            }

            for (int x = 183; x <= 185; x++)
            {
                collectibles.Add(collectibleFactory.CreateCoin(new Vector2(x, 5)));
            }
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

    }
}
