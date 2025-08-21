using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameDevS.Collectibles.Factories
{
    public class CollectibleFactory : FactoryBase, ICollectibleFactory
    {
        public CollectibleFactory(ContentManager contentManager, int tileSize)
            : base (contentManager, tileSize)
        { }

        public ICollectible CreateCoin(Vector2 coordinates)
        {
            Vector2 position = ConvertCoordinates(coordinates);

            Texture2D texture = contentManager.Load<Texture2D>("collectibles/goldenCoin_one");
            SpriteSheet spriteSheet = new SpriteSheet(texture, 5, 2);
            Animation2 animation = new Animation2(spriteSheet);

            Coin coin = new Coin(position, 0.06f, 36, 36);

            coin.AddAnimation(AnimationState.IDLE, animation);

            return coin;
        }
    }
}
