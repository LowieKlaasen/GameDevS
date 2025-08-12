using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevS
{
    internal class MovementManager
    {
        //private readonly float speed; (speed is a property of a movable | If not, speed should be set via ctor)
        private readonly ICollisionService2 collisionService;

        public MovementManager(ICollisionService2 collisionService)
        {
            this.collisionService = collisionService;
        }

        public void Move(IMovable movable, ICollidable2 collidable, GameTime gameTime, KeyboardState keyboardState)
        {
            Vector2 movement = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                movement.X += 1;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                movement.X -= 1;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                movement.Y -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                movement.Y += 1;
            }


            //if (movement != Vector2.Zero)
            //{
            //    movement.Normalize();         (Blijkbaar niet nodig in platformer)
            //}

            Vector2 newPosition = movable.Position + movement * movable.Speed;

            if (!collisionService.WouldColide(collidable, newPosition))
            {
                movable.Position = newPosition;
            }
        }
    }
}
