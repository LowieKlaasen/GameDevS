using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        public StartScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDevice graphicsDevice)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
            this.graphicsDevice = graphicsDevice;
        }

        public void Load()
        {
            woodenBoard = contentManager.Load<Texture2D>("StartScreen");

            parallax = new Texture2D[5];

            for (int i = 0; i < 5; i++)
            {
                string fileName = "background/jungle/plx-" + (i + 1).ToString();

                parallax[i] = contentManager.Load<Texture2D>(fileName);
            }
        }

        public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bg in parallax)
            {
                spriteBatch.Draw(bg, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);
            }

            //spriteBatch.Draw(woodenBoard, new Rectangle(100, 100, 500, 300), Color.White);
        }
    }
}
