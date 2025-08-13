using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDevS
{
    internal class Animation2
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames { get; set; }

        private int counter;
        private double secondCounter;

        public SpriteSheet SpriteSheet;

        public Animation2(SpriteSheet spriteSheet)
        {
            frames = new List<AnimationFrame>();

            SpriteSheet = spriteSheet;

            GetFramesFromSpriteSheet();
        }

        //private void AddFrame(AnimationFrame frame)
        //{
        //    frames.Add(frame);
        //    CurrentFrame = frames[0];
        //}

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            int fps = 15;

            if (secondCounter >= 1d / fps)
            {
                counter++;
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }

        private void GetFramesFromSpriteSheet()
        {
            int frameWidth = SpriteSheet.Texture.Width / SpriteSheet.NumberOfWidthSprites;
            int frameHeight = SpriteSheet.Texture.Height / SpriteSheet.NumberOfHeightSprites;

            for (int y = 0; y <= SpriteSheet.Texture.Height - frameHeight; y += frameHeight)
            {
                for (int x = 0; x <= SpriteSheet.Texture.Width - frameWidth; x += frameWidth)
                {
                    frames.Add(new AnimationFrame(new Rectangle(x, y, frameWidth, frameHeight)));
                }
            }
        }
    }
}
