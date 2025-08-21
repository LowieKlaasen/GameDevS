using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS.Enemies.Factories
{
    public class TempleEnemyFactory : IEnemyFactory
    {
        private readonly ContentManager contentManager;
        private readonly CollisionManager2 collisionManager;
        private readonly int TILESIZE;

        public TempleEnemyFactory(ContentManager contentManager, CollisionManager2 collisionManager, int tileSize)
        {
            this.contentManager = contentManager;
            this.collisionManager = collisionManager;
            TILESIZE = tileSize;
        }

        public Enemy CreateStationaryEnemy(Vector2 startCoordinates)
        {
            Vector2 startPosition = new Vector2(
                startCoordinates.X * TILESIZE,
                startCoordinates.Y * TILESIZE
            );

            Texture2D idleTexture = contentManager.Load<Texture2D>("enemies/golems/golem1_idle");
            SpriteSheet idleSheet = new SpriteSheet(idleTexture, 6, 3);
            Animation2 idleAnimation = new Animation2(idleSheet);

            Texture2D dyingTexture = contentManager.Load<Texture2D>("enemies/golems/golem1_dying");
            SpriteSheet dyingSheet = new SpriteSheet(dyingTexture, 5, 3);
            Animation2 dyingAnimation = new Animation2(dyingSheet);

            StationaryEnemy stationaryEnemy = new StationaryEnemy(startPosition, 0.1f, 23, 22, 41, 54, new StationaryController());

            stationaryEnemy.AddAnimation(AnimationState.IDLE, idleAnimation);
            stationaryEnemy.AddAnimation(AnimationState.DYING, dyingAnimation);

            return stationaryEnemy;
        }

        public Enemy CreatePassivePatrolEnemy(Vector2 startCoordinates)
        {
            Vector2 startPosition = new Vector2(
                startCoordinates.X * TILESIZE,
                startCoordinates.Y * TILESIZE
            );

            Texture2D walkTexture = contentManager.Load<Texture2D>("enemies/golems/golem2_walking");
            SpriteSheet walkSheet = new SpriteSheet(walkTexture, 6, 4);
            Animation2 walkAnimation = new Animation2(walkSheet);

            Texture2D idleTexture = contentManager.Load<Texture2D>("enemies/golems/golem2_idle");
            SpriteSheet idleSheet = new SpriteSheet(idleTexture, 6, 3);
            Animation2 idleAnimation = new Animation2(idleSheet);

            Texture2D dyingTexture = contentManager.Load<Texture2D>("enemies/golems/golem2_dying");
            SpriteSheet dyingSheet = new SpriteSheet(dyingTexture, 5, 3);
            Animation2 dyingAnimatoin = new Animation2(dyingSheet);

            PassivePatrolEnemy patrolEnemy = new PassivePatrolEnemy(startPosition, 0.1f, 23, 22, 41, 54, new PassivePatrolController());

            patrolEnemy.AddAnimation(AnimationState.IDLE, idleAnimation);
            patrolEnemy.AddAnimation(AnimationState.RUNNING, walkAnimation);
            patrolEnemy.AddAnimation(AnimationState.DYING, dyingAnimatoin);

            return patrolEnemy;
        }

        public Enemy CreateActivePatrolEnemy(Vector2 startCoordinates, Player player)
        {
            Vector2 startPosition = new Vector2(
                startCoordinates.X * TILESIZE,
                startCoordinates.Y * TILESIZE
            );

            Texture2D walkTexture = contentManager.Load<Texture2D>("enemies/golems/golem3_walking");
            SpriteSheet walkSheet = new SpriteSheet(walkTexture, 6, 4);
            Animation2 walkAnimation = new Animation2(walkSheet);

            Texture2D idleTexture = contentManager.Load<Texture2D>("enemies/golems/golem3_idle");
            SpriteSheet idleSheet = new SpriteSheet(idleTexture, 6, 3);
            Animation2 idleAnimation = new Animation2(idleSheet);

            Texture2D dyingTexture = contentManager.Load<Texture2D>("enemies/golems/golem3_dying");
            SpriteSheet dyingSheet = new SpriteSheet(dyingTexture, 5, 3);
            Animation2 dyingAnimation = new Animation2(dyingSheet);

            Texture2D jumpTexture = contentManager.Load<Texture2D>("enemies/golems/golem3_jumping");
            SpriteSheet jumpSheet = new SpriteSheet(jumpTexture, 4, 3);
            Animation2 jumpAnimation = new Animation2(jumpSheet);

            PassivePatrolEnemy patrolEnemy = new PassivePatrolEnemy(startPosition, 0.1f, 23, 22, 41, 54, new ActivePatrolController(player, collisionManager));

            patrolEnemy.AddAnimation(AnimationState.IDLE, idleAnimation);
            patrolEnemy.AddAnimation(AnimationState.RUNNING, walkAnimation);
            patrolEnemy.AddAnimation(AnimationState.DYING, dyingAnimation);
            patrolEnemy.AddAnimation(AnimationState.JUMPING, jumpAnimation);

            return patrolEnemy;
        }


    }
}
