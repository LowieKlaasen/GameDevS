using Microsoft.Xna.Framework;

namespace GameDevS.Entities.Collectibles.Factories
{
    internal interface ICollectibleFactory
    {
        ICollectible CreateCoin(Vector2 coordinates);
    }
}
