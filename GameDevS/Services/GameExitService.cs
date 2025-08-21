using Microsoft.Xna.Framework;

namespace GameDevS.Services
{
    public class GameExitService : IGameExitService
    {
        private readonly Game game;

        public GameExitService(Game game)
        {
            this.game = game;
        }

        public void Exit()
        {
            game.Exit();
        }
    }
}
