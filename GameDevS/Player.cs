using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameDevS
{
    internal class Player : Sprite, ICollidable2
    {
        private const float JumpVelocity = -12f;

        public Vector2 velocity;
        public Vector2 Velocity 
        { 
            get { return velocity; } 
            set { velocity = value; } 
        }

        private List<Sprite> collisionGroup;

        private float gravity = 1f;
        private bool isGrounded = false;

        public Player(Texture2D texture, Vector2 position, float scale, List<Sprite> collisionGroup, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites) : base(texture, position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, numberOfWidthSprites, numberOfHeightSprites) 
        {
            this.collisionGroup = collisionGroup;

            velocity = Vector2.Zero;
            speed = 4;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
