using GameDevS.Collision;
using GameDevS.Entities;
using GameDevS.Entities.Enemies;
using GameDevS.Entities.PlayerMap;
using GameDevS.Movement.Controllers;
using Microsoft.Xna.Framework;
using System;

namespace GameDevS.Movement
{
    public class MovementManager
    {
        private readonly ICollisionService2 collisionService;

        private readonly float gravity;

        public MovementManager(ICollisionService2 collisionService)
        {
            this.collisionService = collisionService;

            gravity = 2200f;
        }

        public void Move(IMovable movable, float dt)
        {
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

            #region Acceleration & Deceleration

            if (Math.Abs(desired.X) > 0.01f)
            {
                velocity.X += desired.X * (movable is Player player ? player.Acceleration : 200f) * dt;

                float maxSpeed = (movable is Player player2 ? player2.MaxSpeed : 75f);
                velocity.X = Math.Clamp(velocity.X, -maxSpeed, maxSpeed);
            }
            else
            {
                float deceleration = (movable is Player player3 ? player3.Deceleration : 250f) * dt;
                if (velocity.X > 0)
                {
                    velocity.X = Math.Max(0, velocity.X - deceleration);
                }
                else if (velocity.X < 0)
                {
                    velocity.X = Math.Min(0, velocity.X + deceleration);
                }
            }

            #endregion

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
                    if (movable is Player player)
                    {
                        player.TakeDamage(1, horizontalResult.Direction);
                    }
                }

                if (movable.MovementController is PassivePatrolController passiveController)
                {
                    if (horizontalResult.Collidable is Player player)
                    {
                        player.TakeDamage(1, horizontalResult.Direction);
                    }

                    passiveController.ReverseDirection(movable);
                }
                if (movable.MovementController is ActivePatrolController activeController)
                {
                    if (horizontalResult.Collidable is Player player)
                    {
                        player.TakeDamage(1, horizontalResult.Direction);
                    }

                    activeController.ReverseDirection(movable);
                }
            }

            movable.Position = endHorizontal;
            movable.Velocity = velocity;
        }


    }
}
