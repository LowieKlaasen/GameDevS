using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal class GameOverlay
    {
        private SpriteFont font;
        private Player player;

        private Texture2D coinIcon;

        private Texture2D heartIcon_full;
        private Texture2D heartIcon_empty;

        public GameOverlay(SpriteFont font, Player player, Texture2D coinIcon, Texture2D fullHeart, Texture2D emptyHeart)
        {
            this.font = font;
            this.player = player;
            this.coinIcon = coinIcon;

            heartIcon_full = fullHeart;
            heartIcon_empty = emptyHeart;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            Rectangle coinDestRect = new Rectangle(
                20,
                60,
                20,
                20
            );

            Vector2 coinTextPosition = new Vector2(coinDestRect.X + coinDestRect.Width + 10, coinDestRect.Y);

            int heartSize = 22;
            int startX = 20;
            int startY = 20;

            spriteBatch.Begin();

            for (int i = 0; i < player.Health.Max; i++)
            {
                Texture2D heartIcon = heartIcon_full;
                if (i >= player.Health.Current)
                {
                    heartIcon = heartIcon_empty;
                }

                Rectangle heartDestRect = new Rectangle(
                    startX + i * (heartSize +5),
                    startY,
                    heartSize,
                    heartSize
                );

                spriteBatch.Draw(heartIcon, heartDestRect, Color.White);
            }

            spriteBatch.Draw(coinIcon, coinDestRect, Color.White);
            spriteBatch.DrawString(font, $"x {player.Score:000}", coinTextPosition, Color.Gold);

            spriteBatch.End();
        }
    }
}
