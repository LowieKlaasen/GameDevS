using GameDevS.Entities.PlayerMap;
using Microsoft.Xna.Framework;

namespace GameDevS.Entities.Enemies.Factories
{
    public interface IEnemyFactory
    {
        Enemy CreateStationaryEnemy(Vector2 startCoordinates);
        Enemy CreatePassivePatrolEnemy(Vector2 startCoordinates);
        Enemy CreateActivePatrolEnemy(Vector2 startCoordinates, Player player);
    }
}
