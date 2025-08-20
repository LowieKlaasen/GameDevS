using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevS
{
    public class GameOverMenu : OverlayMenu
    {
        protected override Option[] options { get; set; }

        public GameOverMenu(GraphicsDevice graphicsDevice, GameScene gameScene, ContentManager contentManager)
            : base(graphicsDevice, gameScene, contentManager)
        {
            options = new Option[3];

            options[0] = new Option(optionFont, "Restart", new Color(64, 48, 22), Color.Gold);
            options[1] = new Option(optionFont, "Main Menu", new Color(64, 48, 22), Color.Gold);
            options[2] = new Option(optionFont, "Quit", new Color(64, 48, 22), Color.Gold);

            options[0].Selected = true;
            selectedOption = 0;
        }
        protected override void DrawOptions(SpriteBatch spriteBatch, int boardX, int boardY)
        {
            int spacer = 110;
            foreach (Option option in options)
            {
                float textWidth = option.Font.MeasureString(option.Text).X;
                spriteBatch.DrawString(option.Font, option.Text,
                    new Vector2(boardX + (woodenBoard.Width / 2 - textWidth / 2), boardY + spacer),
                    option.GetColor());
                spacer += 40;
            }
        }

        protected override void DrawTitle(SpriteBatch spriteBatch, int boardX, int boardY)
        {
            string title = "Game Over";
            float textWidth = titleFont.MeasureString(title).X;
            spriteBatch.DrawString(titleFont, title,
                new Vector2(boardX + (woodenBoard.Width / 2 - textWidth / 2), boardY + 30),
                Color.Gold);
        }

        protected override void OnOptionSelected(int selectedOption)
        {
            switch (selectedOption)
            {
                case 0:
                    gameScene.Restart();
                    options[selectedOption].Selected = false;
                    this.selectedOption = 0;
                    options[this.selectedOption].Selected = true;
                    break;
                case 1:
                    gameScene.SceneManager.RemoveScene();
                    break;
                case 2:
                    ServiceLocator.GameExitService.Exit();
                    break;
            }
        }
    }
}
