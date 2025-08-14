using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal class MovementManager
    {
        //private readonly float speed; (speed is a property of a movable | If not, speed should be set via ctor)
        private readonly ICollisionService2 collisionService;

        private readonly float gravity;

        public MovementManager(ICollisionService2 collisionService)
        {
            this.collisionService = collisionService;

            gravity = 2200f;
        }

        public void Move(IMovable movable, IMovementController movementController, GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 velocity = movable.Velocity;

            Vector2 desired = movementController.GetDesiredVelocity(movable, gameTime);

            velocity.X = desired.X;

            if (desired.Y != 0)
            { 
                velocity.Y = desired.Y; 
            }

            if (!movable.IsGrounded)
            { 
                velocity.Y += gravity * dt; 
            }

            Vector2 newPosition = movable.Position + velocity * dt;

            Vector2 verticalPosition = new Vector2(movable.Position.X, newPosition.Y);
            if (collisionService.WouldColide((ICollidable2)movable, verticalPosition))
            {
                if (velocity.Y > 0) // falling
                {
                    movable.IsGrounded = true;
                    velocity.Y = 0;
                }
                verticalPosition.Y = movable.Position.Y;
            }
            else
            {
                Vector2 onePixelDown = new Vector2(movable.Position.X, movable.Position.Y + 1);
                if (collisionService.WouldColide((ICollidable2)movable, onePixelDown))
                {
                    movable.IsGrounded = true;
                    velocity.Y = 0;
                }
                else
                {
                    movable.IsGrounded = false;
                }
            }

            Vector2 horizontalPosition = new Vector2(newPosition.X, verticalPosition.Y);
            if (collisionService.WouldColide((ICollidable2)movable, horizontalPosition))
            {
                velocity.X = 0;
                horizontalPosition.X = movable.Position.X;

                if (movementController is PatrolController patrolController)
                { 
                    patrolController.ReverseDirection(); 
                }
            }

            movable.Position = horizontalPosition;
            movable.Velocity = velocity;
        }

    }
}
