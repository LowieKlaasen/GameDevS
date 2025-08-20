using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevS
{
    public abstract class OverlayMenu
    {
        protected Texture2D overlay;
        protected Texture2D woodenBoard;

        protected GameScene gameScene;
        protected GraphicsDevice graphicsDevice;

        protected SpriteFont titleFont;
        protected SpriteFont optionFont;

        protected abstract Option[] options { get; set; }
        protected int selectedOption;

        protected bool selectionKeyLifted;

        protected bool enterKeyLifted;

        protected OverlayMenu(GraphicsDevice graphicsDevice, GameScene gameScene, ContentManager contentManager)
        {
            overlay = new Texture2D(graphicsDevice, 1, 1);
            overlay.SetData(new[] { Color.Black * 0.5f });

            this.gameScene = gameScene;
            this.graphicsDevice = graphicsDevice;

            woodenBoard = contentManager.Load<Texture2D>("woodenBoard");

            titleFont = contentManager.Load<SpriteFont>("fonts/UnifrakturCook");
            optionFont = contentManager.Load<SpriteFont>("fonts/Jacquard24");

            selectionKeyLifted = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (!enterKeyLifted && state.IsKeyUp(Keys.Enter))
            {
                enterKeyLifted = true;
            }

            if (selectionKeyLifted && state.IsKeyDown(Keys.Down))
            {
                options[selectedOption].Selected = false;

                selectedOption++;
                if (selectedOption > options.Length - 1)
                    selectedOption = 0;

                options[selectedOption].Selected = true;

                selectionKeyLifted = false;
            }

            if (selectionKeyLifted && state.IsKeyDown(Keys.Up))
            {
                options[selectedOption].Selected = false;

                selectedOption--;
                if (selectedOption < 0)
                    selectedOption = options.Length - 1;

                options[selectedOption].Selected = true;

                selectionKeyLifted = false;
            }

            if (!selectionKeyLifted && state.IsKeyUp(Keys.Down) && state.IsKeyUp(Keys.Up))
            {
                selectionKeyLifted = true;
            }

            if (enterKeyLifted && state.IsKeyDown(Keys.Enter))
            {
                OnOptionSelected(selectedOption);
            }
        }

        protected abstract void OnOptionSelected(int selectedOption);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(overlay, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);

            int boardX = (graphicsDevice.Viewport.Width - woodenBoard.Width) / 2;
            int boardY = (graphicsDevice.Viewport.Height - woodenBoard.Height) / 2;

            spriteBatch.Draw(woodenBoard, new Vector2(boardX, boardY), Color.White);

            DrawTitle(spriteBatch, boardX, boardY);

            DrawOptions(spriteBatch, boardX, boardY);
        }

        protected abstract void DrawTitle(SpriteBatch spriteBatch, int boardX, int boardY);
        protected abstract void DrawOptions(SpriteBatch spriteBatch, int boardX, int boardY);
    }
}
