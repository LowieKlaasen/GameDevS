using Microsoft.Xna.Framework;

namespace GameDevS
{
    public class GoalZone : AnimatedEntity
    {
        public Rectangle Rectangle { get; set; }

        public GoalZone(Vector2 position, float scale, int width, int height) 
            : base(position, scale)
        {
            Rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }
    }
}
