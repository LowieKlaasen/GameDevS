using GameDevS.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameDevS
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _heroTexture;
        private Texture2D _enemyTexture;

        List<Sprite> sprites;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            sprites = new List<Sprite>();

            sprites.Add(new Sprite(_enemyTexture, new Vector2(100, 100), 0.1f));
            sprites.Add(new Sprite(_enemyTexture, new Vector2(400, 200), 0.1f));
            sprites.Add(new Sprite(_enemyTexture, new Vector2(700, 300), 0.1f));

            sprites.Add(new Player(_heroTexture, Vector2.Zero, 1f));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _heroTexture = Content.Load<Texture2D>("rogue_cropped");
            _enemyTexture = Content.Load<Texture2D>("goblin_single");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //if (!space_pressed && Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //    space_pressed = true;
            //    Debug.WriteLine("Space key pressed");
            //}

            //if (Keyboard.GetState().IsKeyUp(Keys.Space))
            //{
            //    space_pressed = false;
            //}

            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            foreach (var sprite in sprites)
            {
                sprite.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
