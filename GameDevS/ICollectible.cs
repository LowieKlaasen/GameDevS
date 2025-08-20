using Microsoft.Xna.Framework;

namespace GameDevS
{
    public interface ICollectible
    {
            Rectangle Bounds { get; }
            void OnCollect(Player player);
    }
}
