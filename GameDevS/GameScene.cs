using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameDevS
{
    public class GameScene : IScene
    {
        private ContentManager contentManager;
        private SceneManager sceneManager;

        #region Demo

        private Texture2D texture;

        #endregion

        public GameScene(ContentManager contentManager, SceneManager sceneManager)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(10, 10, 400, 400), new Rectangle(0, 0, 90, 90), Color.White);
        }

        public void Load()
        {
            texture = contentManager.Load<Texture2D>("rogue_cropped");
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                sceneManager.AddScene(new ExitScene(contentManager));
            }
        }
    }
}
