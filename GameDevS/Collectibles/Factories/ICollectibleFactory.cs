using Microsoft.Xna.Framework;

namespace GameDevS.Collectibles.Factories
{
    internal interface ICollectibleFactory
    {
        ICollectible CreateCoin(Vector2 coordinates);
    }
}
