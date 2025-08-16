using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevS
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
