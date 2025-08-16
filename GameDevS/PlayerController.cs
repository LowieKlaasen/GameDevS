using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace GameDevS
{
    internal class PlayerController : IMovementController
    {
        private KeyboardState _keyboardState;
        public void UpdateKeyboard(KeyboardState state)
        {
            _keyboardState = state;
        }

        public Vector2 GetDesiredVelocity(IMovable movable, float dt)
        {
            Vector2 velocity = Vector2.Zero;

            if (_keyboardState.IsKeyDown(Keys.Right))
            { 
                velocity.X = movable.Speed; 
            }
            else if (_keyboardState.IsKeyDown(Keys.Left))
            { 
                velocity.X = -movable.Speed; 
            }

            if (movable.IsGrounded && _keyboardState.IsKeyDown(Keys.Up))
            {
                velocity.Y = -movable.JumpSpeed;
                ServiceLocator.AudioService.Play("jump");
            }

            return velocity;
        }

        public bool CheckDeathByFalling(Player player, Camera2D camera, int viewportHeight)
        {
            if (player.Position.Y > camera.Position.Y + viewportHeight)
            {
                Debug.WriteLine("player fell under the map");
                return true;
            }
            return false;
        }
    }
}
