using GameDevS.movementCommands;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDevS
{
    public class InputHandler
    {
        private Dictionary<Keys, ICommand> _keyBindings = new Dictionary<Keys, ICommand>();

        public InputHandler()
        {
            _keyBindings[Keys.Left] = new MoveLeftCommand();
            _keyBindings[Keys.Right] = new MoveRightCommand();
            _keyBindings[Keys.Up] = new JumpCommand();
        }

        public void HandleInput(IMovable movable, float dt)
        {
            Vector2 velocity = movable.Velocity;
            velocity.X = 0;
            movable.Velocity = velocity;

            KeyboardState state = Keyboard.GetState();

            foreach (var keyBinding in _keyBindings)
            {
                if (state.IsKeyDown(keyBinding.Key))
                {
                    keyBinding.Value.Execute(movable, dt);
                }
            }
        }
    }
}
