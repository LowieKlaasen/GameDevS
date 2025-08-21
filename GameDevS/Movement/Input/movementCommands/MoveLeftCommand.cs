using GameDevS.Movement;
using Microsoft.Xna.Framework;

namespace GameDevS.Movement.Input.movementCommands
{
    public class MoveLeftCommand : ICommand
    {
        public void Execute(IMovable movable, float dt)
        {
            movable.Velocity = new Vector2(-movable.Speed, movable.Velocity.Y);
        }
    }
}
