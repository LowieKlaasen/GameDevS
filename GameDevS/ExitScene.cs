using GameDevS.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    public class ExitScene : IScene
    {
        private ContentManager contentManager;

        public ExitScene(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void Load()
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
