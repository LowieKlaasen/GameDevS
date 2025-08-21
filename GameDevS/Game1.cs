using GameDevS.Debug;
using GameDevS.Scenes;
using GameDevS.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevS
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SceneManager sceneManager;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            sceneManager = new SceneManager();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            DebugDraw.Initialize(GraphicsDevice);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ServiceLocator.AudioService = new AudioService(Content);
            ServiceLocator.GameExitService = new GameExitService(this);

            sceneManager.AddScene(new StartScene(Content, sceneManager, GraphicsDevice));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            sceneManager.GetCurrentScene().Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            sceneManager.GetCurrentScene().Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
