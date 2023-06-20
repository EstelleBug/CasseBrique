using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BrickBreaker;

namespace Services
{
    internal class UIManager
    {
        private GameManager gameManager;

        public UIManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Draw()
        {
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            SpriteFont GameFont = ServiceLocator.Get<IAssetsManager>().GetAsset<SpriteFont>("PixelFont");
            Texture2D lifeTexture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("balle");


            string scoreText = "Score: " + gameManager.Score;
            //string livesText = "Lives: " + gameManager.Lives;
            string levelText = "Level: " + gameManager.Level;

            sb.DrawString(GameFont, levelText, new Vector2(10, 5), Color.White);
            //sb.DrawString(GameFont, livesText, new Vector2(100, 5), Color.White);
            sb.DrawString(GameFont, scoreText, new Vector2(100, 5), Color.White);

            Vector2 lifeTexturePosition = new Vector2(10, Main._screenSize.Y - lifeTexture.Height - 5);
            float spacing = 5f;

            for (int i = 0; i < gameManager.Lives; i++)
            {
                Vector2 offset = new Vector2(i * (lifeTexture.Width + spacing), 0);
                sb.Draw(lifeTexture, lifeTexturePosition + offset, Color.White);
            }

        }

    }
}
