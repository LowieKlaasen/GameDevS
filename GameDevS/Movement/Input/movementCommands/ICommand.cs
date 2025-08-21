using GameDevS.Movement;
using Microsoft.Xna.Framework;

namespace GameDevS.Movement.Input.movementCommands
{
    public interface ICommand
    {
        void Execute(IMovable movable, float dt);
    }
}
