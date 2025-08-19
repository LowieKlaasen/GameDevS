using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace GameDevS
{
    internal class SettingsScene : IScene
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

        private bool enterKeyLifted;

        private Slider[] sliders;
        public SettingsScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDevice graphicsDevice)
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
            menuOptions[0] = new Option(contentManager.Load<SpriteFont>("fonts/Jacquard24"), "Music", new Color(64, 48, 22), Color.Gold);
            menuOptions[1] = new Option(contentManager.Load<SpriteFont>("fonts/Jacquard24"), "Sound Effects", new Color(64, 48, 22), Color.Gold);
            menuOptions[2] = new Option(contentManager.Load<SpriteFont>("fonts/Jacquard24"), "Back", new Color(64, 48, 22), Color.Gold);

            menuOptions[0].Selected = true;
            selectedOption = 0;

            selectionKeyLifted = true;

            sliders = new Slider[2];
            sliders[0] = new Slider(contentManager, new Vector2(woodenBoard.Width /2, 230), 300, 30, VolumeType.MUSIC);
            sliders[1] = new Slider(contentManager, new Vector2(woodenBoard.Width /2, 300), 300, 30, VolumeType.SOUNDEFFECT);

            sliders[0].Selected = true;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var slider in sliders)
            {
                slider.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                enterKeyLifted = true;
            }

            if (selectionKeyLifted && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                menuOptions[selectedOption].Selected = false;
                if (selectedOption < 2)
                    sliders[selectedOption].Selected = false;

                selectedOption++;
                if (selectedOption > menuOptions.Length - 1)
                {
                    selectedOption = 0;
                }

                menuOptions[selectedOption].Selected = true;
                if (selectedOption < 2)
                    sliders[selectedOption].Selected = true;

                selectionKeyLifted = false;
            }

            if (selectionKeyLifted && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                menuOptions[selectedOption].Selected = false;
                if (selectedOption < 2)
                    sliders[selectedOption].Selected = false;

                selectedOption--;
                if (selectedOption < 0)
                {
                    selectedOption = menuOptions.Length - 1;
                }

                menuOptions[selectedOption].Selected = true;
                if (selectedOption < 2)
                    sliders[selectedOption].Selected = true;

                selectionKeyLifted = false;
            }

            if (selectionKeyLifted && selectedOption < 2 && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (sliders[selectedOption].Value < 1)
                {
                    sliders[selectedOption].Value += 0.1f;
                }

                ServiceLocator.AudioService.SetVolume(sliders[selectedOption].VolumeType, sliders[selectedOption].Value);

                selectionKeyLifted = false;
            }

            if (selectionKeyLifted && selectedOption < 2 && Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (sliders[selectedOption].Value > 0)
                {
                    sliders[selectedOption].Value -= 0.1f;
                }

                ServiceLocator.AudioService.SetVolume(sliders[selectedOption].VolumeType, sliders[selectedOption].Value);
                
                selectionKeyLifted = false;
            }

            if (!selectionKeyLifted && Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Up)
                && Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                selectionKeyLifted = true;
            }

            if (enterKeyLifted && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                switch (selectedOption)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        sceneManager.RemoveScene();
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

            int startingpointX = (graphicsDevice.Viewport.Width - woodenBoard.Width) / 2;
            int startingpointY = (graphicsDevice.Viewport.Height - woodenBoard.Height) / 2;

            spriteBatch.Draw(woodenBoard, new Rectangle(startingpointX, startingpointY, woodenBoard.Width, woodenBoard.Height), Color.White);

            spriteBatch.DrawString(titleFont, "Volume Settings", new Vector2((woodenBoard.Width / 2), startingpointY + 30), Color.Gold);

            int spacer = 100;
            foreach (var option in menuOptions)
            {
                spriteBatch.DrawString(option.Font, option.Text, new Vector2(woodenBoard.Width / 2, startingpointY + spacer), option.GetColor());
                spacer += 70;
            }

            foreach (var slider in sliders)
            {
                slider.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
