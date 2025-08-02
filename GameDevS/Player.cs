using GameDevS.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevS
{
    internal class Player : Sprite
    {

        public Player(Texture2D texture, Vector2 position, float scale) : base(texture, position, scale) {   }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position.X += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position.Y -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position.Y += 2;
            }
        }
    }
}
