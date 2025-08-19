using Microsoft.Xna.Framework;
using System.Diagnostics;
using static GameDevS.CollisionManager2;

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

        //public void Move(IMovable movable, IMovementController movementController, float dt)
        public void Move(IMovable movable, float dt)
        {
            //float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float groundSnap = 2f;

            var collidableMovable = (ICollidable2)movable;

            int offsetX, offsetY;
            if (movable is Sprite s)
            {
                offsetX = s.hitboxStartX;
                offsetY = s.hitboxStartY;
            }
            else
            {
                offsetX = collidableMovable.HitBox.Left - (int)movable.Position.X;
                offsetY = collidableMovable.HitBox.Top - (int)movable.Position.Y;
            }

            Vector2 velocity = movable.Velocity;
            Vector2 desired = movable.MovementController.GetDesiredVelocity(movable, dt);

            velocity.X = desired.X;
            if (desired.Y != 0) velocity.Y = desired.Y;

            if (!movable.IsGrounded)
                velocity.Y += gravity * dt;

            // =========
            // VERTICAL
            // =========
            Vector2 startVertical = movable.Position;
            Vector2 endVertical = new Vector2(movable.Position.X, movable.Position.Y + velocity.Y * dt);

            var verticalResult = collisionService.CheckCollision(collidableMovable, startVertical, endVertical);

            if (verticalResult.Direction == CollisionDirection.BOTTOM && velocity.Y >= 0)
            {
                movable.IsGrounded = true;

                endVertical.Y = verticalResult.Collidable.HitBox.Top - offsetY - collidableMovable.HitBox.Height;

                if (verticalResult.Collidable is Enemy enemy)
                {
                    enemy.Die();
                    velocity.Y = -movable.JumpSpeed / 2f;
                }
                else
                {
                    velocity.Y = 0f;
                }
            }
            else if (verticalResult.Direction == CollisionDirection.TOP && velocity.Y < 0)
            {
                endVertical.Y = verticalResult.Collidable.HitBox.Bottom - offsetY;
                velocity.Y = 0f;
                movable.IsGrounded = false;
            }
            else
            {
                Vector2 snapEnd = new Vector2(movable.Position.X, movable.Position.Y + groundSnap);
                var snapResult = collisionService.CheckCollision(collidableMovable, movable.Position, snapEnd);

                if (snapResult.Direction == CollisionDirection.BOTTOM && velocity.Y >= 0)
                {
                    movable.IsGrounded = true;
                    velocity.Y = 0f;
                    endVertical.Y = snapResult.Collidable.HitBox.Top - offsetY - collidableMovable.HitBox.Height;
                }
                else
                {
                    movable.IsGrounded = false;
                }
            }

            movable.Position = endVertical;

            // ===========
            // HORIZONTAL
            // ===========
            Vector2 startHorizontal = movable.Position;
            Vector2 endHorizontal = new Vector2(movable.Position.X + velocity.X * dt, movable.Position.Y);

            var horizontalResult = collisionService.CheckCollision(collidableMovable, startHorizontal, endHorizontal);

            if (horizontalResult.Direction == CollisionDirection.LEFT ||
                horizontalResult.Direction == CollisionDirection.RIGHT)
            {
                if (horizontalResult.Direction == CollisionDirection.LEFT)
                {
                    endHorizontal.X = horizontalResult.Collidable.HitBox.Right - offsetX;
                }
                else
                {
                    endHorizontal.X = horizontalResult.Collidable.HitBox.Left - offsetX - collidableMovable.HitBox.Width;
                }

                velocity.X = 0f;

                if (horizontalResult.Collidable is Enemy enemy)
                {
                    // ToDo: player damage
                    if (movable is Player player)
                    {
                        Debug.WriteLine($"{movable.GetType()} took side damage");

                        player.TakeDamage(1);
                    }
                }

                //if (movementController is PassivePatrolController patrolController) 
                //{
                //    if (horizontalResult.Collidable is Player player)
                //    {
                //        Debug.WriteLine($"Player took damage from patrolenemy");

                //        player.TakeDamage(1);
                //    }

                //    patrolController.ReverseDirection(movable);
                //}

                if (movable is PassivePatrolEnemy passivePatrolEnemy)
                {
                    if (passivePatrolEnemy.MovementController is PassivePatrolController controller)
                    {
                        controller.ReverseDirection(movable);
                    }
                }
            }

            movable.Position = endHorizontal;
            movable.Velocity = velocity;
        }


    }
}
