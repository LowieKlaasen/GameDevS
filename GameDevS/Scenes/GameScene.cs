using GameDevS.Animations;
using GameDevS.Collision;
using GameDevS.Debug;
using GameDevS.Entities;
using GameDevS.Entities.Collectibles;
using GameDevS.Entities.Enemies;
using GameDevS.Entities.PlayerMap;
using GameDevS.Graphics;
using GameDevS.Map;
using GameDevS.Menus;
using GameDevS.Movement;
using GameDevS.Movement.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameDevS.Scenes
{
    public abstract class GameScene : IScene
    {
        public ContentManager ContentManager;
        public SceneManager SceneManager;
        public GraphicsDevice GraphicsDevice;

        protected Camera2D camera;
        protected CollisionManager2 collisionManager;
        protected MovementManager movementManager;
        protected AnimationUpdater animationUpdater;

        protected List<Sprite> sprites;
        protected Player player;
        protected TileMap2 map;
        protected GameOverlay hud;

        public bool IsPaused;
        protected PauseMenu pauseMenu;

        public bool GameOver;
        protected GameOverMenu gameOverMenu;

        public bool Won;
        protected WinMenu winMenu;

        protected List<ICollectible> collectibles;
        protected ScrollingBackground scrollingBackground;

        protected int TILESIZE = 54;

        //protected Rectangle goalZone;
        protected GoalZone goalZone;

        public GameScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDevice graphicsDevice)
        {
            ContentManager = contentManager;
            SceneManager = sceneManager;
            GraphicsDevice = graphicsDevice;

            camera = new Camera2D(graphicsDevice.Viewport);

            pauseMenu = new PauseMenu(GraphicsDevice, this, contentManager);
            gameOverMenu = new GameOverMenu(GraphicsDevice, this, contentManager);
            winMenu = new WinMenu(GraphicsDevice, this, contentManager);
        }

        public virtual void Load()
        {
            collisionManager = new CollisionManager2();
            movementManager = new MovementManager(collisionManager);
            animationUpdater = new AnimationUpdater();

            sprites = new List<Sprite>();
            collectibles = new List<ICollectible>();

            LoadPlayer();
            LoadMap();
            LoadEnemies();
            LoadCollectibles();
            LoadBackground();
            LoadHUD();
            LoadMusic();
        }

        protected abstract void LoadPlayer();
        protected abstract void LoadMap();
        protected abstract void LoadEnemies();
        protected abstract void LoadCollectibles();
        protected abstract void LoadBackground();
        protected abstract void LoadHUD();
        protected abstract void LoadMusic();

        public virtual void Update(GameTime gameTime)
        {
            if (!IsPaused && Keyboard.GetState().IsKeyDown(Keys.P))
            {
                IsPaused = true;
            }
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
            if (Won)
            {
                dt = 0;

                winMenu.Update(gameTime);
                return;
            }

            camera.Follow(player.Position, 0, 200 * TILESIZE, 0, 15 * TILESIZE);

            foreach (var sprite in sprites)
            {
                sprite.Update(dt);
                movementManager.Move(sprite, dt);
            }
            foreach (var sprite in sprites)
            {
                animationUpdater.UpdateAnimation(sprite);
            }

            List<Sprite> toRemove = new List<Sprite>();
            foreach (var sprite in sprites)
            {
                if (sprite is Enemy enemy && !enemy.IsAlive)
                {
                    collisionManager.Remove(enemy);
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

            goalZone.Update(dt);

            if (player.MovementController is PlayerController playerController && playerController.CheckDeathByFalling(player, camera, GraphicsDevice.Viewport.Height))
            {
                GameOver = true;
            }

            CheckGoalReached();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            scrollingBackground.Draw(spriteBatch);

            spriteBatch.Begin(transformMatrix: camera.Transform);

            goalZone.Draw(spriteBatch);

            map.Draw(spriteBatch);

            // ToDo: Remove draw hollow rectangle
            foreach (var tile in map.GetCollidables())
            {
                DebugDraw.DrawHollowRectangle(spriteBatch, tile.HitBox, Color.Green);
            }

            foreach (var sprite in sprites)
            {
                sprite.Draw(spriteBatch);

                // ToDo: Remove draw hollow rectangle
                DebugDraw.DrawHollowRectangle(spriteBatch, sprite.HitBox, Color.Red);
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

            DebugDraw.DrawHollowRectangle(spriteBatch, goalZone.Rectangle, Color.HotPink);

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
            if (Won)
            {
                spriteBatch.Begin();
                winMenu.Draw(spriteBatch);
                spriteBatch.End();
            }
        }

        public void Restart()
        {
            Load();

            IsPaused = false;
            GameOver = false;
            Won = false;
        }

        protected void CheckGoalReached()
        {
            if (player != null && goalZone.Rectangle.Contains(player.HitBox))
            {
                OnGoalReached();
            }
        }

        protected virtual void OnGoalReached()
        {
            System.Diagnostics.Debug.WriteLine("GoalZone reached");
            Won = true;
        }
        
    }
}
