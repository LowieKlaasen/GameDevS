using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameDevS
{
    public class StartScene : IScene
    {
        private ContentManager contentManager;
        private SceneManager sceneManager;
        private GraphicsDevice graphicsDevice;

        private Texture2D woodenBoard;

        private Texture2D[] parallax;

        private SpriteFont titleFont;

        private Option[] menuOptions;
        private int selectedOption;

        private bool selectionKeyLifted;

        public StartScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDevice graphicsDevice)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
            this.graphicsDevice = graphicsDevice;
        }

        public void Load()
        {
            woodenBoard = contentManager.Load<Texture2D>("woodenBoard");

            parallax = new Texture2D[5];

            for (int i = 0; i < 5; i++)
            {
                string fileName = "background/jungle/plx-" + (i + 1).ToString();

                parallax[i] = contentManager.Load<Texture2D>(fileName);
            }

            titleFont = contentManager.Load<SpriteFont>("fonts/UnifrakturCook");

            menuOptions = new Option[3];
            menuOptions[0] = new Option(contentManager.Load<SpriteFont>("fonts/Jacquard24"), "Level 1", new Color(64, 48, 22), Color.Gold);
            menuOptions[1] = new Option(contentManager.Load<SpriteFont>("fonts/Jacquard24"), "Level 2", new Color(64, 48, 22), Color.Gold);
            menuOptions[2] = new Option(contentManager.Load<SpriteFont>("fonts/Jacquard24"), "option 3", new Color(64, 48, 22), Color.Gold);

            menuOptions[0].Selected = true;
            selectedOption = 0;

            selectionKeyLifted = true;
        }

        public void Update(GameTime gameTime)
        {
            if (selectionKeyLifted && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                menuOptions[selectedOption].Selected = false;

                selectedOption++;
                if (selectedOption > menuOptions.Length - 1)
                {
                    selectedOption = 0;
                }
                
                menuOptions[selectedOption].Selected = true;

                selectionKeyLifted = false;
            }

            if (selectionKeyLifted && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                menuOptions[selectedOption].Selected = false;

                selectedOption--;
                if (selectedOption < 0)
                {
                    selectedOption = menuOptions.Length - 1;
                }

                menuOptions[selectedOption].Selected = true;

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
                        sceneManager.AddScene(new GameScene(contentManager, sceneManager, graphicsDevice));
                        break;
                    case 1:
                        // ToDo: Add redirection to level 2
                        throw new NotImplementedException();
                        break;
                    case 2:
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var bg in parallax)
            {
                spriteBatch.Draw(bg, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);
            }

            #region woodenBoard

            int startingpointX = (graphicsDevice.Viewport.Width - woodenBoard.Width) / 2;
            int startingpointY = (graphicsDevice.Viewport.Height - woodenBoard.Height) / 2;

            spriteBatch.Draw(woodenBoard, new Rectangle(startingpointX, startingpointY, woodenBoard.Width, woodenBoard.Height), Color.White);

            spriteBatch.DrawString(titleFont, "Ancient Escape", new Vector2((woodenBoard.Width / 2), startingpointY + 30), Color.Gold);

            int spacer = 110;
            foreach (var option in menuOptions)
            {
                spriteBatch.DrawString(option.Font, option.Text, new Vector2(woodenBoard.Width/2, startingpointY + spacer), option.GetColor());
                spacer += 40;
            }

            #endregion

            spriteBatch.End();
        }
    }
}
