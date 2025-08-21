using GameDevS.Entities.PlayerMap;
using GameDevS.Graphics;
using GameDevS.Movement.Input;
using Microsoft.Xna.Framework;

namespace GameDevS.Movement.Controllers
{
    internal class PlayerController : IMovementController
    {
        private InputHandler inputHandler;

        public PlayerController() 
        {
            inputHandler = new InputHandler();
        }
        public PlayerController(InputHandler inputHandler)
        {
            this.inputHandler = inputHandler;
        }

        public Vector2 GetDesiredVelocity(IMovable movable, float dt)
        {
            if (movable is Player player)
            {
                if (!player.IsKnockBackActive)
                {
                    inputHandler.HandleInput(movable, dt);

                    return new Vector2(
                        (movable.Velocity.X < 0 ? -1 : (movable.Velocity.X > 0 ? 1 : 0)),
                        movable.Velocity.Y
                    );
                }
                return player.KnockbackVelocity;
            }

            return Vector2.Zero;
        }

        public bool CheckDeathByFalling(Player player, Camera2D camera, int viewportHeight)
        {
            if (player.Position.Y > camera.Position.Y + viewportHeight)
            {
                System.Diagnostics.Debug.WriteLine("player fell under the map");
                return true;
            }
            return false;
        }
    }
}
