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

            // ToDo: Check if sceneManager initialization should be here or in Initialize()-method
            sceneManager = new SceneManager();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            DebugDraw.Initialize(GraphicsDevice);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ServiceLocator.AudioService = new AudioService(Content);

            // TODO: use this.Content to load your game content here

            // ToDo: Check wheter AddScene needs to be here or in Initialize()?
            //sceneManager.AddScene(new GameScene(Content, sceneManager));
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

            // TODO: Add your drawing code here
            //_spriteBatch.Begin();

            sceneManager.GetCurrentScene().Draw(_spriteBatch);

            //_spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
