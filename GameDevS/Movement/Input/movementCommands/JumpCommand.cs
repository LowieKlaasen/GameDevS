using GameDevS.Entities.PlayerMap;
using GameDevS.Services;
using Microsoft.Xna.Framework;

namespace GameDevS.Movement.Input.movementCommands
{
    public class JumpCommand : ICommand
    {
        public void Execute(IMovable movable, float dt)
        {
            if (movable.IsGrounded)
            {
                movable.Velocity = new Vector2(movable.Velocity.X, -movable.JumpSpeed);

                if (movable is Player)
                {
                    ServiceLocator.AudioService.Play("jump");
                }
            }
        }
    }
}
