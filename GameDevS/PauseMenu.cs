using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameDevS
{
    internal class PauseMenu
    {
        private Texture2D overlay;
        private Texture2D woodenBoard;

        private GameScene gameScene;
        private GraphicsDevice graphicsDevice;

        private SpriteFont titleFont;

        private Option[] options;
        private int selectedOption;

        private bool selectionKeyLifted;

        public PauseMenu(GraphicsDevice graphicsDevice, GameScene gameScene, ContentManager contentManager)
        {
            overlay = new Texture2D(graphicsDevice, 1, 1);
            overlay.SetData(new[] { Color.Black * 0.5f });

            this.gameScene = gameScene;
            this.graphicsDevice = graphicsDevice;

            woodenBoard = contentManager.Load<Texture2D>("woodenBoard");

            titleFont = contentManager.Load<SpriteFont>("fonts/UnifrakturCook");

            options = new Option[3];

            options[0] = new Option(contentManager.Load<SpriteFont>("fonts/Jacquard24"), "Continue", new Color(64, 48, 22), Color.Gold);
            options[1] = new Option(contentManager.Load<SpriteFont>("fonts/Jacquard24"), "Restart", new Color(64, 48, 22), Color.Gold);
            options[2] = new Option(contentManager.Load<SpriteFont>("fonts/Jacquard24"), "Quit", new Color(64, 48, 22), Color.Gold);

            options[0].Selected = true;
            selectedOption = 0;

            selectionKeyLifted = true;
        }

        public void Update(GameTime gameTime)
        {
            if (selectionKeyLifted && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                options[selectedOption].Selected = false;

                selectedOption++;
                if (selectedOption > options.Length - 1)
                {
                    selectedOption = 0;
                }

                options[selectedOption].Selected = true;

                selectionKeyLifted = false;
            }

            if (selectionKeyLifted && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                options[selectedOption].Selected = false;

                selectedOption--;
                if (selectedOption < 0)
                {
                    selectedOption = options.Length - 1;
                }

                options[selectedOption].Selected = true;

                selectionKeyLifted = false;
            }

            if (!selectionKeyLifted && Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                selectionKeyLifted = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                switch (selectedOption)
                {
                    case 0:
                        gameScene.IsPaused = false;
                        break;
                    case 1:
                        gameScene.Restart();
                        options[selectedOption].Selected = false;
                        selectedOption = 0;
                        options[selectedOption].Selected = true;
                        break;
                    case 2:
                        ServiceLocator.GameExitService.Exit();
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(overlay, new Rectangle(0, 0, 1280, 720), Color.White);

            int startingpointX = (graphicsDevice.Viewport.Width - woodenBoard.Width) / 2;
            int startingpointY = (graphicsDevice.Viewport.Height - woodenBoard.Height) / 2;

            spriteBatch.Draw(woodenBoard, new Rectangle(startingpointX, startingpointY, woodenBoard.Width, woodenBoard.Height), Color.White);

            float textWidth = titleFont.MeasureString("Paused").X;
            spriteBatch.DrawString(titleFont, "Paused", new Vector2(startingpointX + (woodenBoard.Width / 2 - textWidth / 2), startingpointY + 30), Color.Gold);

            int spacer = 110;
            foreach (Option option in options)
            {
                spriteBatch.DrawString(option.Font, option.Text, new Vector2(woodenBoard.Width / 2, startingpointY + spacer), option.GetColor());
                spacer += 40;
            }
        }
    }
}
