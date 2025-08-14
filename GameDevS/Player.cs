using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameDevS
{
    internal class Player : Sprite, ICollidable2
    {
        // ToDo: Check if this is necessery (& remove)
        private List<Sprite> collisionGroup;

        public Player(Texture2D texture, Vector2 position, float scale, List<Sprite> collisionGroup, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites) : base(texture, position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, numberOfWidthSprites, numberOfHeightSprites) 
        {
            this.collisionGroup = collisionGroup;

            speed = 240f;
            jumpSpeed = 680f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
