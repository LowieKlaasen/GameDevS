using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevS.Enemies.Factories
{
    public class JungleEnemyFactory : IEnemyFactory
    {
        private readonly ContentManager contentManager;
        private readonly CollisionManager2 collisionManager;
        private readonly int TILESIZE;

        public JungleEnemyFactory(ContentManager contentManager, CollisionManager2 collisionManager, int tileSize)
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

            return stationaryEnemy;
        }

        public Enemy CreatePassivePatrolEnemy(Vector2 startCoordinates)
        {
            Vector2 startPosition = new Vector2(
                startCoordinates.X * TILESIZE,
                startCoordinates.Y * TILESIZE
            );

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

            return patrolEnemy;
        }

        public Enemy CreateActivePatrolEnemy(Vector2 startCoordinates, Player player)
        {
            Vector2 startPosition = new Vector2(
                startCoordinates.X * TILESIZE,
                startCoordinates.Y * TILESIZE
            );

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

            return patrolEnemy;
        }
    }
}
