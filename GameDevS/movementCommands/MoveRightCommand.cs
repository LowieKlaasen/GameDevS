using Microsoft.Xna.Framework;

namespace GameDevS.movementCommands
{
    public class MoveRightCommand : ICommand
    {
        public void Execute(IMovable movable, float dt)
        {
            movable.Velocity = new Vector2(movable.Speed, movable.Velocity.Y);
        }
    }
}
