using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal class GameOverlay
    {
        private SpriteFont font;
        private Player player;

        private Texture2D coinIcon;

        public GameOverlay(SpriteFont font, Player player, Texture2D coinIcon)
        {
            this.font = font;
            this.player = player;
            this.coinIcon = coinIcon;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            Rectangle coinDestRect = new Rectangle(
                20,
                20,
                20,
                20
            );

            Vector2 coinTextPosition = new Vector2(coinDestRect.X + coinDestRect.Width + 10, coinDestRect.Y);

            spriteBatch.Begin();
            spriteBatch.Draw(coinIcon, coinDestRect, Color.White);
            spriteBatch.DrawString(font, $"x {player.Score}", coinTextPosition, Color.Gold);
            spriteBatch.End();
        }
    }
}
