using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class Camera2D
    {
        public Matrix Transform { get; private set; }
        public Vector2 Position { get; private set; }

        private Viewport _viewport;

        public Camera2D(Viewport viewport)
        {
            _viewport = viewport;
        }

        public void Follow(Vector2 target, 
            float leftBoundary, float rightBoundary,
            float topBoundary, float bottomBoundary)
        {
            float cameraX = target.X - _viewport.Width / 3f;
            float cameraY = target.Y - _viewport.Height / 1.5f;

            cameraX = MathHelper.Clamp(cameraX, leftBoundary, rightBoundary - _viewport.Width);
            cameraY = MathHelper.Clamp(cameraY, topBoundary, bottomBoundary - _viewport.Height);

            Position = new Vector2(cameraX, cameraY);

            Transform = Matrix.CreateTranslation(new Vector3(-Position, 0));
        }
    }
}
