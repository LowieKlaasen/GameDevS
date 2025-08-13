using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class MovementManager
    {
        //private readonly float speed; (speed is a property of a movable | If not, speed should be set via ctor)
        private readonly ICollisionService2 collisionService;

        private readonly float gravity;

        private Vector2 velocity;

        public MovementManager(ICollisionService2 collisionService)
        {
            this.collisionService = collisionService;

            gravity = 1;
        }

        //public void Move(IMovable movable, ICollidable2 collidable, GameTime gameTime, KeyboardState keyboardState)
        //{
        //    Vector2 movement = Vector2.Zero;

        //    if (keyboardState.IsKeyDown(Keys.Right))
        //    {
        //        movement.X += 1;
        //    }
        //    if (keyboardState.IsKeyDown(Keys.Left))
        //    {
        //        movement.X -= 1;
        //    }

        //    if (keyboardState.IsKeyDown(Keys.Up))
        //    {
        //        movement.Y -= 1;
        //    }
        //    if (keyboardState.IsKeyDown(Keys.Down))
        //    {
        //        movement.Y += 1;
        //    }


        //    //if (movement != Vector2.Zero)
        //    //{
        //    //    movement.Normalize(); /*(Blijkbaar niet nodig in platformer)*/
        //    //}

        //    Vector2 newPosition = movable.Position + movement * movable.Speed;

        //    if (!collisionService.WouldColide(collidable, newPosition))
        //    {
        //        movable.Position = newPosition;
        //    }
        //}

        public void Move(IMovable movable, ICollidable2 collidable, GameTime gameTime, KeyboardState keyboardState)
        {
            //float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Horizontal input
            velocity.X = 0;
            if (keyboardState.IsKeyDown(Keys.Right)) 
            { 
                velocity.X = movable.Speed;

                if (movable is Sprite sprite)
                {
                    sprite.effect = SpriteEffects.None;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Left)) 
            { 
                velocity.X = -movable.Speed;

                if (movable is Sprite sprite)
                {
                    sprite.effect = SpriteEffects.FlipHorizontally;
                }
            }

            // Jump
            if (movable.IsGrounded && keyboardState.IsKeyDown(Keys.Up))
            {
                velocity.Y = -movable.JumpSpeed;
                movable.IsGrounded = false;
            }

            // Apply gravity
            //velocity.Y += gravity * dt;
            velocity.Y += gravity;

            // Calculate new position
            //Vector2 newPosition = movable.Position + velocity * dt;
            Vector2 newPosition = movable.Position + velocity;

            // Check vertical collisions first
            Vector2 verticalPosition = new Vector2(movable.Position.X, newPosition.Y);
            if (collisionService.WouldColide((ICollidable2)movable, verticalPosition))
            {
                if (velocity.Y > 0) // Falling down
                    movable.IsGrounded = true;

                velocity.Y = 0; // Stop vertical movement
                verticalPosition.Y = movable.Position.Y; // Reset to old Y
            }

            // Check horizontal collisions
            Vector2 horizontalPosition = new Vector2(newPosition.X, verticalPosition.Y);
            if (collisionService.WouldColide((ICollidable2)movable, horizontalPosition))
            {
                velocity.X = 0; // Stop horizontal movement
                horizontalPosition.X = movable.Position.X; // Reset to old X
            }

            movable.Position = horizontalPosition;

            if (movable is Sprite s)
            {
                if (!s.IsGrounded)
                {
                    s.SetAnimation(AnimationState.JUMPING);
                }
                else if (velocity.X != 0)
                {
                    s.SetAnimation(AnimationState.RUNNING);
                }
                else
                {
                    s.SetAnimation(AnimationState.IDLE);
                }
            }
        }

    }
}
