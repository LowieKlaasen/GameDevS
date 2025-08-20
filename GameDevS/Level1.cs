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

            sprites.Add(player);
        }

        protected override void LoadMap()
        {
            Texture2D textureSwamp = ContentManager.Load<Texture2D>("map/swamp_tileset");

            map = new TileMap2(textureSwamp, TILESIZE, 32, 10);
            map.LoadMap("../../../Data/Level1_TempEnding.csv");

            foreach (var tile in map.GetCollidables())
            {
                collisionManager.Register(tile);
            }
        }

        protected override void LoadEnemies()
        {
            CreateStationaryEnemy(new Vector2(33, 3));

            CreatePassivePatrolEnemy(new Vector2(16, 7));

            CreatePassivePatrolEnemy(new Vector2(46, 9));
            CreatePassivePatrolEnemy(new Vector2(50, 9));
            CreatePassivePatrolEnemy(new Vector2(54, 9));
            CreatePassivePatrolEnemy(new Vector2(60, 9));

            CreateActivePatrolEnemy(new Vector2(22, 6), player);
            CreateActivePatrolEnemy(new Vector2(59, 3), player);
            CreateActivePatrolEnemy(new Vector2(88, 4), player);

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

            CreateCoin(new Vector2(72, 1));
            CreateCoin(new Vector2(73, 1));
            CreateCoin(new Vector2(74, 1));
            CreateCoin(new Vector2(75, 1));
            CreateCoin(new Vector2(76, 1));
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


        #region Private Methods

        private void CreatePassivePatrolEnemy(Vector2 startCoordinates)
        {
            Vector2 startPosition = new Vector2(
                startCoordinates.X * TILESIZE,
                startCoordinates.Y * TILESIZE
            );

            Texture2D walkTexture = ContentManager.Load<Texture2D>("enemies/goblin/golem_walking");
            SpriteSheet walkSheet = new SpriteSheet(walkTexture, 8, 3);
            Animation2 walkAnimation = new Animation2(walkSheet);

            Texture2D idleTexture = ContentManager.Load<Texture2D>("enemies/goblin/goblin_idle");
            SpriteSheet idleSheet = new SpriteSheet(idleTexture, 6, 3);
            Animation2 idleAnimation = new Animation2(idleSheet);

            Texture2D dyingTexture = ContentManager.Load<Texture2D>("enemies/goblin/goblin_dying");
            SpriteSheet dyingSheet = new SpriteSheet(dyingTexture, 5, 3);
            Animation2 dyingAnimatoin = new Animation2(dyingSheet);

            PassivePatrolEnemy patrolEnemy = new PassivePatrolEnemy(startPosition, 0.1f, 23, 22, 41, 54, new PassivePatrolController());

            patrolEnemy.AddAnimation(AnimationState.IDLE, idleAnimation);
            patrolEnemy.AddAnimation(AnimationState.RUNNING, walkAnimation);
            patrolEnemy.AddAnimation(AnimationState.DYING, dyingAnimatoin);

            sprites.Add(patrolEnemy);
        }

        private void CreateActivePatrolEnemy(Vector2 startCoordinates, Player player)
        {
            Vector2 startPosition = new Vector2(
                startCoordinates.X * TILESIZE,
                startCoordinates.Y * TILESIZE
            );

            Texture2D walkTexture = ContentManager.Load<Texture2D>("enemies/ogre/ogre_walking");
            SpriteSheet walkSheet = new SpriteSheet(walkTexture, 8, 3);
            Animation2 walkAnimation = new Animation2(walkSheet);

            Texture2D idleTexture = ContentManager.Load<Texture2D>("enemies/ogre/ogre_idle");
            SpriteSheet idleSheet = new SpriteSheet(idleTexture, 6, 3);
            Animation2 idleAnimation = new Animation2(idleSheet);

            Texture2D dyingTexture = ContentManager.Load<Texture2D>("enemies/ogre/ogre_dying");
            SpriteSheet dyingSheet = new SpriteSheet(dyingTexture, 5, 3);
            Animation2 dyingAnimation = new Animation2(dyingSheet);

            Texture2D jumpTexture = ContentManager.Load<Texture2D>("enemies/ogre/ogre_jumpSequene");
            SpriteSheet jumpSheet = new SpriteSheet(jumpTexture, 6, 2);
            Animation2 jumpAnimation = new Animation2(jumpSheet);

            PassivePatrolEnemy patrolEnemy = new PassivePatrolEnemy(startPosition, 0.1f, 23, 22, 41, 54, new ActivePatrolController(player, collisionManager));

            patrolEnemy.AddAnimation(AnimationState.IDLE, idleAnimation);
            patrolEnemy.AddAnimation(AnimationState.RUNNING, walkAnimation);
            patrolEnemy.AddAnimation(AnimationState.DYING, dyingAnimation);
            patrolEnemy.AddAnimation(AnimationState.JUMPING, jumpAnimation);

            sprites.Add(patrolEnemy);
        }

        private void CreateStationaryEnemy(Vector2 startCoordinates)
        {
            Vector2 startPosition = new Vector2(
                startCoordinates.X * TILESIZE,
                startCoordinates.Y * TILESIZE
            );

            Texture2D walkTexture = ContentManager.Load<Texture2D>("enemies/orc/orc_walking");
            SpriteSheet walkSheet = new SpriteSheet(walkTexture, 8, 3);
            Animation2 walkAnimation = new Animation2(walkSheet);

            Texture2D idleTexture = ContentManager.Load<Texture2D>("enemies/orc/orc_idle");
            SpriteSheet idleSheet = new SpriteSheet(idleTexture, 6, 3);
            Animation2 idleAnimation = new Animation2(idleSheet);

            Texture2D dyingTexture = ContentManager.Load<Texture2D>("enemies/orc/orc_dying");
            SpriteSheet dyingSheet = new SpriteSheet(dyingTexture, 5, 3);
            Animation2 dyingAnimation = new Animation2(dyingSheet);

            Texture2D chargingTexture = ContentManager.Load<Texture2D>("enemies/orc/orc_running");
            SpriteSheet chargingSheet = new SpriteSheet(chargingTexture, 4, 3);
            Animation2 chargingAnimation = new Animation2(chargingSheet);

            StationaryEnemy stationaryEnemy = new StationaryEnemy(startPosition, 0.1f, 23, 22, 41, 54, new StationaryController());

            stationaryEnemy.AddAnimation(AnimationState.IDLE, idleAnimation);
            stationaryEnemy.AddAnimation(AnimationState.RUNNING, walkAnimation);
            stationaryEnemy.AddAnimation(AnimationState.DYING, dyingAnimation);

            sprites.Add(stationaryEnemy);
        }

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
