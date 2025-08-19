using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal interface ICollectible
    {
            Rectangle Bounds { get; }
            void OnCollect(Player player);
    }
}
