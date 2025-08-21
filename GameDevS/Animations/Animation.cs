using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDevS.Animations
{
    internal class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames { get; set; }

        private int counter;
        private double secondCounter;

        public Animation()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            int fps = 15;

            if (secondCounter >= 1d/fps)
            {
                counter++;
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }

        public void GetFramesFromTextureProperties(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            int frameWidth = width / numberOfWidthSprites;
            int frameHeight = height / numberOfHeightSprites;

            for (int y = 0; y <= height - frameHeight ; y+= frameHeight)
            {
                for (int x = 0; x <= width - frameWidth ; x+= frameWidth)
                {
                    frames.Add(new AnimationFrame(new Rectangle(x, y, frameWidth, frameHeight)));
                }
            }
        }
    }
}
