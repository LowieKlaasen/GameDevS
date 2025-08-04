using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    internal class Option
    {
        public SpriteFont Font;
        public bool Selected;
        public string Text;

        private Color unselectedColor;
        private Color selectedColor;

        public Option(SpriteFont font, string textValue, Color unselectedColor, Color selectedColor)
        {
            Font = font;
            Selected = false;
            Text = textValue;
            this.unselectedColor = unselectedColor;
            this.selectedColor = selectedColor;
        }

        public Color GetColor()
        {
            if (Selected == true)
                return selectedColor;

            return unselectedColor;
        }


    }
}
