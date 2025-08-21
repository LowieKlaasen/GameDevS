using GameDevS.Entities.PlayerMap;
using GameDevS.Services;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameDevS.Entities.Collectibles
{
    public class Coin : AnimatedEntity, ICollectible
    {
        private int width;
        private int height;

        public bool Collected;

        public Rectangle Bounds => new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            width, 
            height
        );

        public Coin(Vector2 position, float scale, int width, int height)
            : base(new Vector2(position.X + 8, position.Y + 8), scale)
        {
            this.width = width;
            this.height = height;

            Collected = false;
        }

        public void OnCollect(Player player)
        {
            // ToDo: Implement score
            System.Diagnostics.Debug.WriteLine("Coin collected");
            player.Score += 1;

            System.Diagnostics.Debug.WriteLine("Player score: " +  player.Score);

            ServiceLocator.AudioService.Play("coinCollected");
            Collected = true;
        }
    }
}
