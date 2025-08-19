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

        public GameOverlay(SpriteFont font, Player player)
        {
            this.font = font;
            this.player = player;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, $"Coins: {player.Score}", new Vector2(20, 20), Color.Gold);
            spriteBatch.End();
        }
    }
}
