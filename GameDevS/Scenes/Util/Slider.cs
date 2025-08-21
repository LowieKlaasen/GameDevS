using GameDevS.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS.Scenes.Util
{
    public enum VolumeType { MUSIC, SOUNDEFFECT}

    internal class Slider
    {
        private Texture2D defaultTrack;
        private Texture2D defaultCircle;

        private Texture2D selectedTrack;
        private Texture2D selectedCircle;

        private Rectangle destRectTrack;
        private Rectangle destRectCircle;

        private int trackLength;
        private int radius;
        public float Value { get; set; }

        public bool Selected;

        public VolumeType VolumeType;

        private Vector2 position;

        public Slider(ContentManager contentManager, Vector2 position, int width, int radius, VolumeType volumeType)
        {
            defaultTrack = contentManager.Load<Texture2D>("volumeMenu/track_default");
            defaultCircle = contentManager.Load<Texture2D>("volumeMenu/circle_default");
            selectedTrack = contentManager.Load<Texture2D>("volumeMenu/track_selected");
            selectedCircle = contentManager.Load<Texture2D>("volumeMenu/circle_selected");

            int height = (int)(defaultTrack.Height / (float)defaultTrack.Width * width / 3 * 2);

            trackLength = width;
            VolumeType = volumeType;
            this.position = position;
            this.radius = radius;

            Value = ServiceLocator.AudioService.GetVolume(volumeType);

            destRectTrack = new Rectangle(
                (int)position.X,
                (int)position.Y,
                width,
                height
            );

            destRectCircle = new Rectangle(
                (int)(position.X + Value * trackLength - radius/2),
                (int)(position.Y - radius/5),
                radius,
                radius
            );
        }

        public void Update(GameTime gameTime) 
        {
            Value = ServiceLocator.AudioService.GetVolume(VolumeType);

            destRectCircle = new Rectangle(
                (int)(position.X + Value * trackLength - radius / 2),
                (int)(position.Y - radius / 5),
                radius,
                radius
            );
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D track = defaultTrack;
            Texture2D circle = defaultCircle;

            if (Selected) 
            {
                track = selectedTrack;
                circle = selectedCircle;
            }

            spriteBatch.Draw(track, destRectTrack, Color.White);
            spriteBatch.Draw(circle, destRectCircle, Color.White);
        }


    }
}
