using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS.Graphics
{
    public class Camera2D
    {
        public Matrix Transform { get; private set; }
        public Vector2 Position { get; private set; }

        public readonly Viewport Viewport;

        public Camera2D(Viewport viewport)
        {
            Viewport = viewport;
        }

        public void Follow(Vector2 target, 
            float leftBoundary, float rightBoundary,
            float topBoundary, float bottomBoundary)
        {
            float cameraX = target.X - Viewport.Width / 3f;
            //float cameraY = target.Y - _viewport.Height / 1.5f;
            float cameraY = target.Y - Viewport.Height / 2f;

            cameraX = MathHelper.Clamp(cameraX, leftBoundary, rightBoundary - Viewport.Width);
            cameraY = MathHelper.Clamp(cameraY, topBoundary, bottomBoundary - Viewport.Height);

            Position = new Vector2(cameraX, cameraY);

            Transform = Matrix.CreateTranslation(new Vector3(-Position, 0));
        }
    }
}
