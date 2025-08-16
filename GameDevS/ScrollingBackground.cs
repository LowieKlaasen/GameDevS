using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class ScrollingBackground
    {
        private Texture2D texture;
        private Camera2D camera;
        private float parallaxFactor;

        public ScrollingBackground(Texture2D texture, Camera2D camera, float parallaxFactor)
        {
            this.texture = texture;
            this.camera = camera;
            this.parallaxFactor = parallaxFactor;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int screenWidth = camera.Viewport.Width;
            int screenHeight = camera.Viewport.Height;

            float offsetX = (camera.Position.X * parallaxFactor) % screenWidth;
            if (offsetX < 0)
            {
                offsetX += screenWidth;
            }

            // First copy
            var destRect1 = new Rectangle(
                (int)(-offsetX),
                0,
                screenWidth,
                screenHeight
            );

            // Second copy (to the right)
            var destRect2 = new Rectangle(
                (int)(-offsetX + screenWidth),
                0,
                screenWidth,
                screenHeight
            );

            spriteBatch.Begin();
            spriteBatch.Draw(texture, destRect1, Color.White);
            spriteBatch.Draw(texture, destRect2, Color.White);
            spriteBatch.End();
        }
    }
}
