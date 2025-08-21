using GameDevS.Entities.PlayerMap;
using Microsoft.Xna.Framework;

namespace GameDevS.Entities.Collectibles
{
    public interface ICollectible
    {
            Rectangle Bounds { get; }
            void OnCollect(Player player);
    }
}
