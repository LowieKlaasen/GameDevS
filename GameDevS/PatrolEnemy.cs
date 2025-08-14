using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevS
{
    internal class PatrolEnemy : Enemy
    {
        public bool GoingLeft;

        public PatrolEnemy(Texture2D texture, Vector2 position, float scale, int hitboxStartX, int hitboxStartY, int hitboxWidth, int hitboxHeight, int numberOfWidthSprites, int numberOfHeightSprites) 
            : base(texture, position, scale, hitboxStartX, hitboxStartY, hitboxWidth, hitboxHeight, numberOfWidthSprites, numberOfHeightSprites)
        {
            GoingLeft = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
