using Microsoft.Xna.Framework;

namespace GameDevS.movementCommands
{
    public interface ICommand
    {
        void Execute(IMovable movable, float dt);
    }
}
