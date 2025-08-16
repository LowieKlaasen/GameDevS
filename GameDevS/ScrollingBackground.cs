using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameDevS
{
    internal class ScrollingBackground
    {
        private Camera2D camera;
        private readonly List<BackgroundLayer> backgroundLayers = new List<BackgroundLayer>();

        public ScrollingBackground(Camera2D camera)
        {
            this.camera = camera;
        }

        public void AddLayer(Texture2D texture, float parallaxFactor) 
        { 
            backgroundLayers.Add(new BackgroundLayer(texture, parallaxFactor));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int screenWidth = camera.Viewport.Width;
            int screenHeight = camera.Viewport.Height;

            spriteBatch.Begin();
            foreach (var backgroundLayer in backgroundLayers)
            {
                float offsetX = (camera.Position.X * backgroundLayer.ParallaxFactor) % screenWidth;
                if (offsetX < 0)
                {
                    offsetX += screenWidth;
                }

                var destRect1 = new Rectangle(
                    (int)(-offsetX),
                    0,
                    screenWidth,
                    screenHeight
                );

                var destRect2 = new Rectangle(
                    (int)(-offsetX + screenWidth),
                    0,
                    screenWidth,
                    screenHeight
                );

                spriteBatch.Draw(backgroundLayer.Texture, destRect1, Color.White);
                spriteBatch.Draw(backgroundLayer.Texture, destRect2, Color.White);
            }
            spriteBatch.End();
        }
    }


    class BackgroundLayer
    {
        public Texture2D Texture { get; }
        public float ParallaxFactor { get; }

        public BackgroundLayer(Texture2D texture, float parallaxFactor)
        {
            Texture = texture;
            ParallaxFactor = parallaxFactor;
        }
    }
}
