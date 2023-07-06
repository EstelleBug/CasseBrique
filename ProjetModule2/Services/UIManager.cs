using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BrickBreaker;

namespace Services
{
    internal class UIManager
    {
        private GameManager _gameManager;

        public UIManager(GameManager gameManager)
        {
            this._gameManager = gameManager;
        }

        public void Draw()
        {
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            SpriteFont GameFont = ServiceLocator.Get<IAssetsManager>().GetAsset<SpriteFont>("PixelFont");
            Texture2D lifeTexture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("balle");


            string scoreText = "Score: " + _gameManager.Score;
            string levelText = "Level: " + _gameManager.Level;

            sb.DrawString(GameFont, levelText, new Vector2(10, 5), Color.White);
            sb.DrawString(GameFont, scoreText, new Vector2(100, 5), Color.White);

            Vector2 lifeTexturePosition = new Vector2(10, Main.screenSize.Y - lifeTexture.Height - 5);
            float spacing = 5f;

            for (int i = 0; i < _gameManager.Lives; i++)
            {
                Vector2 offset = new Vector2(i * (lifeTexture.Width + spacing), 0);
                sb.Draw(lifeTexture, lifeTexturePosition + offset, Color.White);
            }

        }

    }
}
